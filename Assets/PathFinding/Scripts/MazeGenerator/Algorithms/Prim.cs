using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class Prim : MonoBehaviour, MazeGeneratorInterface
    {
        /// <summary>
        /// Generates a maze using Prim's algorithm.
        /// </summary>
        /// <param name="cells">The 2D array of CellInterface objects representing the maze grid.</param>
        public void Generate(CellInterface[,] cells)
        {
            StartCoroutine(GenerationWithDelay(cells));
        }

        /// <summary>
        /// Coroutine for generating the maze with a delay between each step.
        /// </summary>
        /// <param name="cells">The 2D array of CellInterface objects representing the maze grid.</param>
        IEnumerator GenerationWithDelay(CellInterface[,] cells)
        {
            List<CellInterface> list = new List<CellInterface>();
            MazeTools.SetAllCellTypes(CellTool.CellType.EMPTY, cells);

            // Choose a random starting cell and mark it as a wall
            CellInterface startCellInterface = cells[Random.Range(0, cells.GetLength(0)), Random.Range(0, cells.GetLength(1))];
            startCellInterface.SetCellType(CellTool.CellType.WALL, false);

            // Get the initial neighbors of the starting cell
            list = MazeTools.GetNeighbours(startCellInterface, cells);

            while (list.Count != 0)
            {
                // Pick a random cell from the list of available cells
                CellInterface currentCellInterface = list[Random.Range(0, list.Count)];
                list.Remove(currentCellInterface);

                // Skip if the current cell is already a wall
                if (currentCellInterface.GetCellType() == CellTool.CellType.WALL) continue;

                // Set the current cell as a wall
                currentCellInterface.SetCellType(CellTool.CellType.WALL, false);

                // Get the neighboring cells that are walls
                List<CellInterface> neighbours = MazeTools.GetNeighboursWithType(currentCellInterface, cells, CellTool.CellType.WALL);

                if (neighbours.Count != 0)
                {
                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);

                    // Pick a random neighbor
                    CellInterface c = neighbours[Random.Range(0, neighbours.Count)];
                    neighbours.Remove(c);

                    // Set the neighbor and the wall between them as walls
                    c.SetCellType(CellTool.CellType.WALL, false);
                    cells[c.GetXValue() - (c.GetXValue() - currentCellInterface.GetXValue()) / 2, c.GetYValue() - (c.GetYValue() - currentCellInterface.GetYValue()) / 2].SetCellType(CellTool.CellType.WALL, false);

                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }

                // Get the empty neighbors of the current cell and add them to the list
                List<CellInterface> emptyNeighbours = MazeTools.GetNeighboursWithType(currentCellInterface, cells, CellTool.CellType.EMPTY);
                list.AddRange(emptyNeighbours);

                foreach (CellInterface c in emptyNeighbours)
                {
                    c.SetColor(Color.cyan, false);
                }

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Gets the name of the maze generator.
        /// </summary>
        /// <returns>The name of the maze generator.</returns>
        public string GetName()
        {
            return "Prim";
        }
    }
}
