using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Maze generator that implements the Kruskal's algorithm.
    /// </summary>
    public class Kruskal : MonoBehaviour, MazeGeneratorInterface
    {
        List<List<CellInterface>> sets;

        /// <summary>
        /// Generates a maze using Kruskal's algorithm.
        /// </summary>
        /// <param name="cells">The 2D array of cells representing the maze grid.</param>
        public void Generate(CellInterface[,] cells)
        {
            StartCoroutine(DelayedGeneration(cells));
        }

        /// <summary>
        /// Generates the maze with a delay between each step.
        /// </summary>
        /// <param name="cells">The 2D array of cells representing the maze grid.</param>
        /// <returns>An IEnumerator for the coroutine.</returns>
        IEnumerator DelayedGeneration(CellInterface[,] cells)
        {
            MazeTools.SetAllCellTypes(CellTool.CellType.EMPTY, cells);

            List<Tuple<CellInterface, int>> edges = new List<Tuple<CellInterface, int>>();
            Dictionary<int, List<CellInterface>> sets = new Dictionary<int, List<CellInterface>>();

            int setNum = 0;
            for (int i = 1; i < cells.GetLength(0) - 1; i += 2)
            {
                for (int j = 1; j < cells.GetLength(1) - 1; j += 2)
                {
                    CellInterface c2 = cells[i, j];

                    edges.Add(new Tuple<CellInterface, int>(c2, 0));
                    edges.Add(new Tuple<CellInterface, int>(c2, 1));

                    c2.SetHelperNum(setNum);

                    List<CellInterface> list = new List<CellInterface>();
                    list.Add(c2);

                    sets.Add(setNum++, list);
                }
            }

            edges = edges.OrderBy(item => UnityEngine.Random.Range(0, 1000f)).ToList();

            while (edges.Count != 0)
            {
                CellInterface c = edges[0].Item1;
                int orientation = edges[0].Item2;

                edges.RemoveAt(0);

                if (c.GetCellType() == CellTool.CellType.WALL)
                {
                    continue;
                }

                if (orientation == 0)
                { //vertical
                    int x = c.GetXValue(), y = c.GetYValue();
                    if (x >= 0 && x < cells.GetLength(0) - 2)
                    {
                        CellInterface up = cells[x + 2, y];

                        if (up.GetHelperNum() == c.GetHelperNum()) continue;

                        UnifySets(c.GetHelperNum(), up.GetHelperNum(), sets);

                        up.SetCellType(CellTool.CellType.WALL, false);
                        cells[x + 1, y].SetCellType(CellTool.CellType.WALL, false);
                        cells[x, y].SetCellType(CellTool.CellType.WALL, false);
                    }
                }
                else
                { //horizontal
                    int x = c.GetXValue(), y = c.GetYValue();
                    if (y >= 0 && y < cells.GetLength(1) - 2)
                    {
                        CellInterface right = cells[x, y + 2];

                        if (right.GetHelperNum() == c.GetHelperNum()) continue;

                        UnifySets(c.GetHelperNum(), right.GetHelperNum(), sets);

                        right.SetCellType(CellTool.CellType.WALL, false);
                        cells[x, y + 1].SetCellType(CellTool.CellType.WALL, false);
                        cells[x, y].SetCellType(CellTool.CellType.WALL, false);
                    }
                }

                if (GameManager.delay != 0)
                    yield return new WaitForSeconds(GameManager.delay);
            }

            GameManager._Instance.SetIsInteractable(true);
        }

        /// <summary>
        /// Unifies two sets by merging them into a single set.
        /// </summary>
        /// <param name="s1Num">The number of the first set.</param>
        /// <param name="s2Num">The number of the second set.</param>
        /// <param name="set">A dictionary that maps set numbers to lists of cells.</param>
        void UnifySets(int s1Num, int s2Num, Dictionary<int, List<CellInterface>> set)
        {
            if (s1Num == s2Num) return;

            foreach (CellInterface c in set[s2Num])
            {
                set[s1Num].Add(c);
                c.SetHelperNum(s1Num);
            }

            set[s2Num].Clear();
        }

        /// <summary>
        /// Gets the name of the maze generator.
        /// </summary>
        /// <returns>The name of the maze generator.</returns>
        public string GetName()
        {
            return "Kruskal";
        }
    }
}
