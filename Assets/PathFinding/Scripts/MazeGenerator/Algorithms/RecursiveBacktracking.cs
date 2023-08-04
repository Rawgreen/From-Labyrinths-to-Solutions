using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// A maze generator that uses the Recursive Backtracking algorithm.
    /// </summary>
    public class RecursiveBacktracking : MonoBehaviour, MazeGeneratorInterface
    {
        /// <summary>
        /// Generates a maze using the Recursive Backtracking algorithm.
        /// </summary>
        /// <param name="cells">The 2D array of CellInterface objects representing the maze grid.</param>
        public void Generate(CellInterface[,] cells)
        {
            // Set all cells in the maze to walls
            MazeTools.SetAllCellTypes(CellTool.CellType.WALL, cells);

            // Randomly select a starting cell in the maze
            CellInterface startCellInterface = cells[Random.Range(0, cells.GetLength(0)), Random.Range(0, cells.GetLength(1))];
            startCellInterface.SetCellType(CellTool.CellType.EMPTY, false);

            // Create a list to track visited cells during the generation process
            List<CellInterface> visitedCells = new List<CellInterface>();
            visitedCells.Add(startCellInterface);

            // Start the maze generation process
            StartCoroutine(StartGeneration(cells, visitedCells, startCellInterface));
        }

        /// <summary>
        /// Coroutine for starting the maze generation process.
        /// </summary>
        IEnumerator StartGeneration(CellInterface[,] cells, List<CellInterface> visitedCells, CellInterface currentCellInterface)
        {
            yield return DoMaze(cells, visitedCells, currentCellInterface);

            // Set the maze generator to be interactable
            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Recursive method that performs the maze generation.
        /// </summary>
        IEnumerator DoMaze(CellInterface[,] cells, List<CellInterface> visitedCells, CellInterface currentCellInterface)
        {
            int x = currentCellInterface.GetXValue();
            int y = currentCellInterface.GetYValue();

            int[] a = { 0, 1, 0, -1 };
            int[] b = { 1, 0, -1, 0 };

            int[] intArray = { 0, 1, 2, 3 };
            List<int> intList = new List<int>(intArray);
            intList = intList.OrderBy(item => Random.Range(0, 1000f)).ToList();

            for (int i = 0; i < 4; i++)
            {
                int nx = a[intList[i]];
                int ny = b[intList[i]];

                if (nx + ny > 0)
                {
                    if (y + ny * 2 < cells.GetLength(1) && x + nx * 2 < cells.GetLength(0) && !visitedCells.Contains(cells[x + nx, y + ny]) && !visitedCells.Contains(cells[x + nx * 2, y + ny * 2]))
                    {
                        // Carve a path between the current cell and the next cell
                        HandleCarving(cells[x + nx, y + ny], cells[x + nx * 2, y + ny * 2], visitedCells);

                        if (GameManager.delay != 0)
                            yield return new WaitForSeconds(GameManager.delay);

                        // Recursively continue the maze generation from the next cell
                        yield return StartCoroutine(DoMaze(cells, visitedCells, cells[x + nx * 2, y + ny * 2]));
                    }
                }
                else
                {
                    if (y + ny * 2 >= 0 && x + nx * 2 >= 0 && !visitedCells.Contains(cells[x + nx, y + ny]) && !visitedCells.Contains(cells[x + nx * 2, y + ny * 2]))
                    {
                        // Carve a path between the current cell and the next cell
                        HandleCarving(cells[x + nx, y + ny], cells[x + nx * 2, y + ny * 2], visitedCells);

                        if (GameManager.delay != 0)
                            yield return new WaitForSeconds(GameManager.delay);

                        // Recursively continue the maze generation from the next cell
                        yield return StartCoroutine(DoMaze(cells, visitedCells, cells[x + nx * 2, y + ny * 2]));
                    }
                }
            }
        }

        /// <summary>
        /// Carves a path between two cells and marks them as visited.
        /// </summary>
        /// <param name="c1">The first cell to carve a path from.</param>
        /// <param name="c2">The second cell to carve a path to.</param>
        /// <param name="visitedCells">The list of visited cells.</param>
        public static void HandleCarving(CellInterface c1, CellInterface c2, List<CellInterface> visitedCells)
        {
            c1.SetCellType(CellTool.CellType.EMPTY, false);
            c2.SetCellType(CellTool.CellType.EMPTY, false);

            visitedCells.Add(c1);
            visitedCells.Add(c2);
        }

        /// <summary>
        /// Gets the name of the maze generator.
        /// </summary>
        /// <returns>The name of the maze generator.</returns>
        public string GetName()
        {
            return "Recursive Backtracking";
        }
    }
}
