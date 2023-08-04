using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// A maze generator that uses the recursive division algorithm.
    /// </summary>
    public class RecursiveDivision : MonoBehaviour, MazeGeneratorInterface
    {
        bool yieldBool;

        /// <summary>
        /// Generates a maze using the recursive division algorithm.
        /// </summary>
        /// <param name="cells">The grid of cells representing the maze.</param>
        public void Generate(CellInterface[,] cells)
        {
            // Set all cells to be empty by default
            foreach (CellInterface c in cells)
            {
                c.SetCellType(CellTool.CellType.EMPTY, true);

                // Set the boundary cells as walls
                if (c.GetXValue() == 0 || c.GetYValue() == 0 || c.GetXValue() == cells.GetLength(0) - 1 || c.GetYValue() == cells.GetLength(1) - 1)
                    c.SetCellType(CellTool.CellType.WALL, true);
            }

            // Start the recursive pathing algorithm
            StartCoroutine(StartPathing(0, cells.GetLength(0) - 1, 0, cells.GetLength(1) - 1, cells));
        }

        /// <summary>
        /// Coroutine method for starting the recursive pathing algorithm.
        /// </summary>
        IEnumerator StartPathing(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, CellInterface[,] cells)
        {
            // Call the recursive pathing method
            yield return RecursivePathing(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells);

            // Enable interactivity when the maze generation is complete
            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Recursive method for generating the maze.
        /// </summary>
        IEnumerator RecursivePathing(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, CellInterface[,] cells)
        {
            // Base case: if the boundaries are too close, exit the method
            if (lowerBoundaryX >= upperBoundaryX - 2 || lowerBoundaryY >= upperBoundaryY - 2)
                yield break;

            // Delay between maze generation steps
            if (GameManager.delay != 0)
                yield return new WaitForSeconds(GameManager.delay);

            // Randomly choose whether to divide vertically or horizontally
            if (Random.Range(0, 2) == 0)
            {
                // Divide vertically
                yield return StartCoroutine(Vertical(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells));

                // If the vertical division did not occur, perform a horizontal division
                if (!yieldBool)
                {
                    Horizontal(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells);
                }
            }
            else
            {
                // Divide horizontally
                yield return StartCoroutine(Horizontal(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells));

                // If the horizontal division did not occur, perform a vertical division
                if (!yieldBool)
                {
                    Vertical(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells);
                }
            }
        }

        /// <summary>
        /// Coroutine method for performing the vertical division.
        /// </summary>
        IEnumerator Vertical(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, CellInterface[,] cells)
        {
            // Check if the division space is too small
            if (upperBoundaryX - lowerBoundaryX - 3 <= 0)
            {
                yieldBool = false;
                yield break;
            }
            else
            {
                // Randomly choose a position for the division wall and the space
                int idx = Random.Range(0, upperBoundaryX - lowerBoundaryX - 3) + lowerBoundaryX + 2;
                int wallSpaceidx = Random.Range(0, upperBoundaryY - lowerBoundaryY - 1) + lowerBoundaryY + 1;

                // Create the division wall
                for (int i = lowerBoundaryY + 1; i < upperBoundaryY; i++)
                {
                    cells[idx, i].SetCellType(CellTool.CellType.WALL, false);
                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }

                // Create the empty space in the division wall
                cells[idx, wallSpaceidx].SetCellType(CellTool.CellType.EMPTY, false);

                // Recursively divide the spaces on both sides of the division wall
                yield return StartCoroutine(RecursivePathing(lowerBoundaryX, idx, lowerBoundaryY, upperBoundaryY, cells));
                yield return StartCoroutine(RecursivePathing(idx, upperBoundaryX, lowerBoundaryY, upperBoundaryY, cells));
            }

            yieldBool = true;
        }

        /// <summary>
        /// Coroutine method for performing the horizontal division.
        /// </summary>
        IEnumerator Horizontal(int lowerBoundaryX, int upperBoundaryX, int lowerBoundaryY, int upperBoundaryY, CellInterface[,] cells)
        {
            // Check if the division space is too small
            if (upperBoundaryY - lowerBoundaryY - 3 <= 0)
            {
                yieldBool = false;
                yield break;
            }

            // Randomly choose a position for the division wall and the space
            int idx = Random.Range(0, upperBoundaryY - lowerBoundaryY - 3) + lowerBoundaryY + 2;
            int wallSpaceidx = Random.Range(0, upperBoundaryX - lowerBoundaryX - 1) + lowerBoundaryX + 1;

            // Create the division wall
            for (int i = lowerBoundaryX + 1; i < upperBoundaryX; i++)
            {
                cells[i, idx].SetCellType(CellTool.CellType.WALL, false);
                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            // Create the empty space in the division wall
            cells[wallSpaceidx, idx].SetCellType(CellTool.CellType.EMPTY, false);

            // Recursively divide the spaces on both sides of the division wall
            yield return StartCoroutine(RecursivePathing(lowerBoundaryX, upperBoundaryX, lowerBoundaryY, idx, cells));
            yield return StartCoroutine(RecursivePathing(lowerBoundaryX, upperBoundaryX, idx, upperBoundaryY, cells));

            yieldBool = true;
        }

        /// <summary>
        /// Returns the name of the maze generation algorithm.
        /// </summary>
        /// <returns>The name of the algorithm.</returns>
        public string GetName()
        {
            return "Recursive Division";
        }
    }
}
