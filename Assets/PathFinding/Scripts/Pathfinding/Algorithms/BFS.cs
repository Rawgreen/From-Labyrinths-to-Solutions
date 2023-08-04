using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Breath-First Search (BFS) algorithm implementation for pathfinding.
    /// </summary>
    public class BFS : MonoBehaviour, PathfinderInterface
    {
        int stepCount;

        /// <summary>
        /// Finds the shortest path from the start cell to the target cell using the BFS algorithm.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="target">The target cell.</param>
        public void FindPath(CellInterface[,] cells, CellInterface start, CellInterface target)
        {
            stepCount = 0;

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
            // Initialization phase
            foreach (CellInterface c in cells)
            {
                c.SetParentCell(null);
                c.SetIsVisited(false);
                c.SetHelperNum(0);

                if (c.GetCellType() == CellTool.CellType.EMPTY)
                {
                    c.SetColor(Color.white, true);
                }
            }

            Queue<CellInterface> queue = new Queue<CellInterface>();
            queue.Enqueue(start);

            int currentDistance = 0;
            while (queue.Count != 0)
            {
                CellInterface c = queue.Dequeue();
                c.SetIsVisited(true);

                GameManager._Instance.ChangeStepCount(++stepCount);

                if (c == target) break;

                List<CellInterface> neighbours = PathfindingTools.GetVisitedNeighbours(c, cells, false);

                if (neighbours.Count == 0) continue;

                for (int i = 0; i < neighbours.Count; i++)
                {
                    CellInterface neighbour = neighbours[i];

                    if (neighbour.GetCellType() == CellTool.CellType.WALL) continue;

                    neighbour.SetIsVisited(true);
                    neighbour.SetParentCell(c);
                    neighbour.SetHelperNum(c.GetHelperNum() + 1); // HelperNum measures the distance from start
                    queue.Enqueue(neighbour);

                    if (target != neighbour)
                        PathfindingTools.SetCellColorByDistance(neighbour, neighbour.GetHelperNum());
                }

                if (currentDistance < c.GetHelperNum())
                {
                    currentDistance++;

                    if (GameManager.delay != 0)
                        yield return new WaitForSeconds(GameManager.delay);
                }
            }

            // Highlight the shortest path
            yield return StartCoroutine(PathfindingTools.HighlightPathWithParents(target, start));

            GameManager._Instance.SetShortestPath(PathfindingTools.GetPath(target, start).Count);
            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Returns the name of the BFS algorithm.
        /// </summary>
        /// <returns>The name of the algorithm.</returns>
        public string GetName()
        {
            return "Breath-First Search";
        }
    }
}
