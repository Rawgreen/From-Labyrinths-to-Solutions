using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// The PathfindingTools class provides utility methods for pathfinding and cell color manipulation.
    /// </summary>
    public class PathfindingTools : MonoBehaviour
    {
        private static int distancePerColor = 25;
        private static Color[] blendColors = { Color.magenta, new Color(138 / 255f, 43 / 255f, 226 / 255f), Color.cyan, Color.green, Color.yellow };

        /// <summary>
        /// Returns a list of visited neighbors of a given cell.
        /// </summary>
        /// <param name="c">The cell for which to find the visited neighbors.</param>
        /// <param name="cells">The 2D array of cells representing the grid.</param>
        /// <param name="isVisited">Flag indicating whether to retrieve visited or unvisited neighbors.</param>
        /// <returns>A list of CellInterface objects representing the visited neighbors.</returns>
        public static List<CellInterface> GetVisitedNeighbours(CellInterface c, CellInterface[,] cells, bool isVisited)
        {
            List<CellInterface> neighbours = new List<CellInterface>();
            int x = c.GetXValue(), y = c.GetYValue();

            if (x >= 1)
            {
                CellInterface cellInterface = cells[x - 1, y];
                if (cellInterface.GetIsVisited() == isVisited)
                    neighbours.Add(cellInterface);
            }

            if (x < cells.GetLength(0) - 1)
            {
                CellInterface cellInterface = cells[x + 1, y];
                if (cellInterface.GetIsVisited() == isVisited)
                    neighbours.Add(cellInterface);
            }

            if (y >= 1)
            {
                CellInterface cellInterface = cells[x, y - 1];
                if (cellInterface.GetIsVisited() == isVisited)
                    neighbours.Add(cellInterface);
            }

            if (y < cells.GetLength(1) - 1)
            {
                CellInterface cellInterface = cells[x, y + 1];
                if (cellInterface.GetIsVisited() == isVisited)
                    neighbours.Add(cellInterface);
            }

            return neighbours;
        }

        /// <summary>
        /// Sets the color of a cell based on its distance from the start cell.
        /// </summary>
        /// <param name="c">The cell to set the color for.</param>
        /// <param name="dist">The distance of the cell from the start cell.</param>
        public static void SetCellColorByDistance(CellInterface c, int dist)
        {
            int idx = (dist / distancePerColor) % blendColors.Length;
            int idx2 = (idx + 1) % blendColors.Length;

            Color color = Color.Lerp(blendColors[idx], blendColors[idx2], (dist % distancePerColor) / (float)distancePerColor);
            c.SetColor(color, false);
        }

        /// <summary>
        /// Highlights the path from the target cell to the start cell by changing the color of the cells along the path.
        /// </summary>
        /// <param name="c">The target cell.</param>
        /// <param name="start">The start cell.</param>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        public static IEnumerator HighlightPathWithParents(CellInterface c, CellInterface start)
        {
            while (c.GetParentCell() != null)
            {
                c = c.GetParentCell();

                if (c != start)
                    c.SetColor(new Color(255 / 255f, 165 / 255f, 0 / 255f), false);

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }
        }

        /// <summary>
        /// Retrieves the path from the target cell to the start cell.
        /// </summary>
        /// <param name="target">The target cell.</param>
        /// <param name="start">The start cell.</param>
        /// <returns>A list of CellInterface objects representing the path.</returns>
        public static List<CellInterface> GetPath(CellInterface target, CellInterface start)
        {
            List<CellInterface> cells = new List<CellInterface>();

            while (target.GetParentCell() != null)
            {
                cells.Add(target);
                target = target.GetParentCell();
            }

            return cells;
        }

        /// <summary>
        /// Returns the array of blend colors used for cell coloring.
        /// </summary>
        /// <returns>An array of Color objects representing the blend colors.</returns>
        public static Color[] GetColors() => blendColors;

        /// <summary>
        /// Sets the array of blend colors used for cell coloring.
        /// </summary>
        /// <param name="c">An array of Color objects representing the blend colors.</param>
        public static void SetColors(Color[] c)
        {
            blendColors = c;
        }
    }
}
