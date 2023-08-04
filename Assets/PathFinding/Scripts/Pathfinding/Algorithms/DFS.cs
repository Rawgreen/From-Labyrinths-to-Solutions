using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Depth-First Search (DFS) algorithm implementation for pathfinding.
    /// </summary>
    public class DFS : MonoBehaviour, PathfinderInterface
    {
        bool coroutineBool;
        int stepCount;

        /// <summary>
        /// Finds the shortest path from the start cell to the target cell using the DFS algorithm.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="target">The target cell.</param>
        public void FindPath(CellInterface[,] cells, CellInterface start, CellInterface target)
        {
            stepCount = 0;

            foreach (CellInterface c in cells)
            {
                c.SetParentCell(null);
                c.SetIsVisited(false);
                c.SetHelperNum(0);

                if (c.GetCellType() == CellTool.CellType.EMPTY)
                    c.SetColor(Color.white, true);
            }

            StartCoroutine(DelayedPathing(cells, start, target));
        }

        /// <summary>
        /// Performs the DFS algorithm with a delay between steps to visualize the process.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="start">The starting cell.</param>
        /// <param name="target">The target cell.</param>
        IEnumerator DelayedPathing(CellInterface[,] cells, CellInterface start, CellInterface target)
        {
            yield return (RecursiveDFS(cells, start, target));

            yield return (PathfindingTools.HighlightPathWithParents(target, start));

            GameManager._Instance.SetShortestPath(PathfindingTools.GetPath(target, start).Count);
            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Recursively performs the DFS algorithm to find the path from the current cell to the target cell.
        /// </summary>
        /// <param name="cells">The grid of cells to navigate.</param>
        /// <param name="current">The current cell being explored.</param>
        /// <param name="target">The target cell.</param>
        IEnumerator RecursiveDFS(CellInterface[,] cells, CellInterface current, CellInterface target)
        {
            if (current == target)
            {
                coroutineBool = true;
                yield break;
            }

            current.SetIsVisited(true);
            PathfindingTools.SetCellColorByDistance(current, current.GetHelperNum());

            GameManager._Instance.ChangeStepCount(++stepCount);

            if (GameManager.delay != 0)
                yield return new WaitForSeconds(GameManager.delay);

            foreach (CellInterface c in PathfindingTools.GetVisitedNeighbours(current, cells, false))
            {
                if (c.GetCellType() == CellTool.CellType.WALL) continue;
                if (c.GetIsVisited()) continue;

                c.SetParentCell(current);
                c.SetHelperNum(current.GetHelperNum() + 1);

                yield return StartCoroutine(RecursiveDFS(cells, c, target));

                if (coroutineBool)
                {
                    yield break;
                }
            }

            coroutineBool = false;
            yield break;
        }

        /// <summary>
        /// Returns the name of the DFS algorithm.
        /// </summary>
        /// <returns>The name of the algorithm.</returns>
        public string GetName()
        {
            return "Depth-First Search";
        }
    }
}
