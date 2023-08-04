using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Utility class for cell-related operations and properties.
    /// </summary>
    public class CellTool
    {
        /// <summary>
        /// Checks if two cells are neighboring each other.
        /// </summary>
        /// <param name="cell1">The first cell.</param>
        /// <param name="cell2">The second cell.</param>
        /// <returns>True if the cells are neighbors, false otherwise.</returns>
        public static bool IsNeighbouring(CellInterface cell1, CellInterface cell2)
        {
            int x = Mathf.Abs(cell1.GetXValue() - cell2.GetXValue());
            int y = Mathf.Abs(cell1.GetYValue() - cell2.GetYValue());

            if (x <= 1 && y <= 1 && (x == 0 || y == 0))
                return true;

            return false;
        }

        /// <summary>
        /// Enumeration representing the different types of cells.
        /// </summary>
        public enum CellType { EMPTY, WALL, TARGET, START }
    }

    /// <summary>
    /// Represents the properties of a cell type.
    /// </summary>
    [System.Serializable]
    public class CellTypeProps
    {
        [SerializeField]
        private Color cellColor;
        [SerializeField]
        private Sprite sprite;

        /// <summary>
        /// Gets the color associated with the cell type.
        /// </summary>
        /// <returns>The color of the cell type.</returns>
        public Color GetColor() => cellColor;

        /// <summary>
        /// Gets the sprite associated with the cell type.
        /// </summary>
        /// <returns>The sprite of the cell type.</returns>
        public Sprite GetSprite() => sprite;
    }
}