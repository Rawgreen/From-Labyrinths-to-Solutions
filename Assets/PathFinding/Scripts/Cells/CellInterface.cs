using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Interface representing the properties and methods of a grid-based cell.
    /// </summary>
    public interface CellInterface
    {
        /// <summary>
        /// Sets the grid position of the cell.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell in the grid.</param>
        /// <param name="y">The y-coordinate of the cell in the grid.</param>
        void SetGridPosition(int x, int y);

        /// <summary>
        /// Gets the x-coordinate of the cell in the grid.
        /// </summary>
        /// <returns>The x-coordinate of the cell.</returns>
        int GetXValue();

        /// <summary>
        /// Gets the y-coordinate of the cell in the grid.
        /// </summary>
        /// <returns>The y-coordinate of the cell.</returns>
        int GetYValue();

        /// <summary>
        /// Sets the type of the cell and updates its appearance.
        /// </summary>
        /// <param name="type">The new type of the cell.</param>
        /// <param name="forceChange">If true, the color change is instant without animation.</param>
        void SetCellType(CellTool.CellType type, bool forceChange);

        /// <summary>
        /// Sets the color of the cell.
        /// </summary>
        /// <param name="color">The new color of the cell.</param>
        /// <param name="forceChange">If true, the color change is instant without animation.</param>
        void SetColor(Color color, bool forceChange);

        /// <summary>
        /// Gets the type of the cell.
        /// </summary>
        /// <returns>The type of the cell.</returns>
        CellTool.CellType GetCellType();

        /// <summary>
        /// Gets the helper number associated with the cell.
        /// </summary>
        /// <returns>The helper number.</returns>
        int GetHelperNum();

        /// <summary>
        /// Sets the helper number associated with the cell.
        /// </summary>
        /// <param name="n">The new helper number.</param>
        void SetHelperNum(int n);

        /// <summary>
        /// Checks if the cell has been visited during pathfinding.
        /// </summary>
        /// <returns>True if the cell has been visited, false otherwise.</returns>
        bool GetIsVisited();

        /// <summary>
        /// Sets the visited status of the cell.
        /// </summary>
        /// <param name="b">The new visited status.</param>
        void SetIsVisited(bool b);

        /// <summary>
        /// Gets the parent cell interface used for path reconstruction.
        /// </summary>
        /// <returns>The parent cell interface.</returns>
        CellInterface GetParentCell();

        /// <summary>
        /// Sets the parent cell interface used for path reconstruction.
        /// </summary>
        /// <param name="ic">The parent cell interface.</param>
        void SetParentCell(CellInterface ic);
    }
}
