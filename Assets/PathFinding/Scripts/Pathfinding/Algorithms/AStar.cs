using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// A* algorithm implementation for pathfinding.
    /// </summary>
    public class AStar : MonoBehaviour, PathfinderInterface
    {
        int stepCount;
        int[,] manhattanDistance;

        /// <summary>
        /// Finds the shortest path from the start cell to the target cell using the A* algorithm.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="target">The target cell.</param>
        public void FindPath(CellInterface[,] cells, CellInterface start, CellInterface target)
        {
            stepCount = 0;
            manhattanDistance = new int[cells.GetLength(0), cells.GetLength(1)];

            // Initialization phase
            foreach (CellInterface c in cells)
            {
                c.SetParentCell(null);
                c.SetIsVisited(false);
                c.SetHelperNum(99999); // Minimum distance from start
                manhattanDistance[c.GetXValue(), c.GetYValue()] = 2 * Mathf.Abs(target.GetXValue() - c.GetXValue()) + Mathf.Abs(target.GetYValue() - c.GetYValue());

                if (c.GetCellType() == CellTool.CellType.EMPTY)
                    c.SetColor(Color.white, true);
            }

            start.SetHelperNum(0);

            StartCoroutine(DelayedPathing(cells, start, target));
        }

        /// <summary>
        /// Performs the pathfinding algorithm with a delay between steps to visualize the process.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="target">The target cell.</param>
        IEnumerator DelayedPathing(CellInterface[,] cells, CellInterface start, CellInterface target)
        {
            List<CellInterface> list = new List<CellInterface>();
            list.Add(start);

            while (list.Count != 0)
            {
                GameManager._Instance.ChangeStepCount(++stepCount);

                CellInterface c = GetCellWithSmallestF(list);
                c.SetIsVisited(true);

                list.Remove(c);

                if (c != start && c != target)
                    PathfindingTools.SetCellColorByDistance(c, c.GetHelperNum());

                if (c == target) break;

                List<CellInterface> neighbours = PathfindingTools.GetVisitedNeighbours(c, cells, false);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    CellInterface neighbour = neighbours[i];

                    if (neighbour.GetCellType() == CellTool.CellType.WALL) continue;

                    neighbour.SetIsVisited(true);
                    neighbour.SetParentCell(c);
                    neighbour.SetHelperNum(c.GetHelperNum() + 1); // HelperNum measures the distance from start
                    list.Add(neighbour);
                }

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            // Highlight the shortest path
            yield return StartCoroutine(PathfindingTools.HighlightPathWithParents(target, start));

            GameManager._Instance.SetShortestPath(PathfindingTools.GetPath(target, start).Count);
            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Returns the cell with the lowest F score (Manhattan distance + distance from start) from a list of cells.
        /// </summary>
        /// <param name="cells">The list of cells to search.</param>
        /// <returns>The cell with the lowest F score.</returns>
        private CellInterface GetCellWithSmallestF(List<CellInterface> cells)
        {
            int min = 999999;
            CellInterface minCellInterface = null;

            foreach (CellInterface c in cells)
            {
                int m = manhattanDistance[c.GetXValue(), c.GetYValue()];
                if (c.GetHelperNum() + m < min)
                {
                    min = c.GetHelperNum() + m;
                    minCellInterface = c;
                }
            }

            return minCellInterface;
        }

        /// <summary>
        /// Returns the name of the A* algorithm.
        /// </summary>
        /// <returns>The name of the algorithm.</returns>
        public string GetName()
        {
            return "A*";
        }
    }
}
