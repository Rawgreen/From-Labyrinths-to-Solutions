using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Interface for maze generator algorithms.
    /// </summary>
    public interface MazeGeneratorInterface
    {
        /// <summary>
        /// Gets the name of the maze generator.
        /// </summary>
        /// <returns>The name of the maze generator.</returns>
        string GetName();

        /// <summary>
        /// Generates a maze using the specified grid of cells.
        /// </summary>
        /// <param name="cells">The 2D array of CellInterface representing the grid of cells.</param>
        void Generate(CellInterface[,] cells);
    }
}