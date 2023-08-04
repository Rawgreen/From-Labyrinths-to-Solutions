using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Maze generator that implements the binary tree algorithm.
    /// </summary>
    public class BinaryTreeMaze : MonoBehaviour, MazeGeneratorInterface
    {
        /// <summary>
        /// Generates a maze using the binary tree algorithm.
        /// </summary>
        /// <param name="cells">The 2D array of cells representing the maze grid.</param>
        public void Generate(CellInterface[,] cells)
        {
            MazeTools.SetAllCellTypes(CellTool.CellType.WALL, cells);

            StartCoroutine(GenerationWithDelay(cells));
        }

        /// <summary>
        /// Generates the maze with a delay between each step.
        /// </summary>
        /// <param name="cells">The 2D array of cells representing the maze grid.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        IEnumerator GenerationWithDelay(CellInterface[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i += 2)
            {
                for (int j = 0; j < cells.GetLength(1); j += 2)
                {
                    CellInterface c = cells[i, j];
                    c.SetCellType(CellTool.CellType.EMPTY, false);

                    List<CellInterface> emptyNeighbours = new List<CellInterface>();

                    if (i != 0 && cells[i - 2, j].GetCellType() == CellTool.CellType.EMPTY)
                        emptyNeighbours.Add(cells[i - 1, j]);

                    if (j != 0 && cells[i, j - 2].GetCellType() == CellTool.CellType.EMPTY)
                        emptyNeighbours.Add(cells[i, j - 1]);

                    if (emptyNeighbours.Count != 0)
                    {
                        emptyNeighbours[Random.Range(0, emptyNeighbours.Count)].SetCellType(CellTool.CellType.EMPTY, false);
                    }

                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Gets the name of the maze generator.
        /// </summary>
        /// <returns>The name of the maze generator.</returns>
        public string GetName()
        {
            return "Binary Tree";
        }
    }
}
