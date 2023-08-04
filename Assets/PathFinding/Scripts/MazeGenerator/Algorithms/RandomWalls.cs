using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class RandomWalls : MonoBehaviour, MazeGeneratorInterface
    {
        /// <summary>
        /// Generates a maze with random walls.
        /// </summary>
        /// <param name="cells">The 2D array of CellInterface objects representing the maze grid.</param>
        public void Generate(CellInterface[,] cells)
        {
            // Set all cells in the maze to empty
            MazeTools.SetAllCellTypes(CellTool.CellType.EMPTY, cells);

            // Iterate through each cell in the maze
            foreach (CellInterface ic in cells)
            {
                // Randomly decide whether to set the cell as a wall
                if (Random.Range(0, 100) < 40)
                {
                    ic.SetCellType(CellTool.CellType.WALL, false);
                }
            }

            // Set the maze generator to be interactable
            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Gets the name of the maze generator.
        /// </summary>
        /// <returns>The name of the maze generator.</returns>
        public string GetName()
        {
            return "Random Walls";
        }
    }
}