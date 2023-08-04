using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Provides utility methods for managing maze-related operations.
    /// </summary>
    public class MazeTools
    {
        /// <summary>
        /// Sets the cell type of all cells in the provided array.
        /// </summary>
        /// <param name="type">The cell type to set.</param>
        /// <param name="cells">The 2D array of cells.</param>
        public static void SetAllCellTypes(CellTool.CellType type, CellInterface[,] cells)
        {
            foreach (CellInterface ic in cells)
            {
                ic.SetCellType(type, true);
            }
        }

        /// <summary>
        /// Returns a list of neighboring cells of the given cell.
        /// </summary>
        /// <param name="c">The cell to find neighbors for.</param>
        /// <param name="cells">The 2D array of cells.</param>
        /// <returns>A list of neighboring cells.</returns>
        public static List<CellInterface> GetNeighbours(CellInterface c, CellInterface[,] cells)
        {
            List<CellInterface> neighbours = new List<CellInterface>();
            int x = c.GetXValue(), y = c.GetYValue();

            if (x >= 2)
            {
                neighbours.Add(cells[x - 2, y]);
            }

            if (x < cells.GetLength(0) - 2)
            {
                neighbours.Add(cells[x + 2, y]);
            }

            if (y >= 2)
            {
                neighbours.Add(cells[x, y - 2]);
            }

            if (y < cells.GetLength(1) - 2)
            {
                neighbours.Add(cells[x, y + 2]);
            }

            return neighbours;
        }

        /// <summary>
        /// Returns a list of neighboring cells of the given cell with a specific cell type.
        /// </summary>
        /// <param name="c">The cell to find neighbors for.</param>
        /// <param name="cells">The 2D array of cells.</param>
        /// <param name="type">The cell type to filter by.</param>
        /// <returns>A list of neighboring cells with the specified cell type.</returns>
        public static List<CellInterface> GetNeighboursWithType(CellInterface c, CellInterface[,] cells, CellTool.CellType type)
        {
            List<CellInterface> neighbours = new List<CellInterface>();
            int x = c.GetXValue(), y = c.GetYValue();

            if (x >= 2)
            {
                CellInterface cellInterface = cells[x - 2, y];
                if (cellInterface.GetCellType() == type)
                    neighbours.Add(cellInterface);
            }

            if (x < cells.GetLength(0) - 2)
            {
                CellInterface cellInterface = cells[x + 2, y];
                if (cellInterface.GetCellType() == type)
                    neighbours.Add(cellInterface);
            }

            if (y >= 2)
            {
                CellInterface cellInterface = cells[x, y - 2];
                if (cellInterface.GetCellType() == type)
                    neighbours.Add(cellInterface);
            }

            if (y < cells.GetLength(1) - 2)
            {
                CellInterface cellInterface = cells[x, y + 2];
                if (cellInterface.GetCellType() == type)
                    neighbours.Add(cellInterface);
            }

            return neighbours;
        }
    }
}
