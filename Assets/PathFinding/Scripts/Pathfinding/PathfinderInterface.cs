using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// The PathfinderInterface defines the contract that pathfinding algorithms must adhere to.
    /// </summary>
    public interface PathfinderInterface
    {
        /// <summary>
        /// Finds the shortest path from the start cell to the target cell.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="target">The target cell.</param>
        void FindPath(CellInterface[,] cells, CellInterface start, CellInterface target);

        /// <summary>
        /// Returns the name of the pathfinding algorithm.
        /// </summary>
        /// <returns>The name of the algorithm.</returns>
        string GetName();
    }
}