using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Generates and manages the grid of cells for the maze.
    /// </summary>
    public class FieldGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;  // Prefab for the individual cells in the maze
        [SerializeField] private MazeSettings mazeSettings;  // Settings for the maze generation

        private CellInterface[,] cells;  // 2D array to store the grid of cells

        /// <summary>
        /// Generates the maze grid based on the provided maze settings.
        /// </summary>
        /// <param name="setting">The maze settings specifying the size and appearance of the maze.</param>
        public void GenerateMap(MazeSetting setting)
        {
            GenerateMap(setting.xSize, setting.ySize, setting.cellSize, setting.gapBetweenCells);
            transform.position = setting.fieldPos;
        }

        /// <summary>
        /// Generates the maze grid based on the specified parameters.
        /// </summary>
        /// <param name="xSize">The number of cells in the x-axis.</param>
        /// <param name="ySize">The number of cells in the y-axis.</param>
        /// <param name="scale">The scale of each cell.</param>
        /// <param name="gap">The gap between cells.</param>
        private void GenerateMap(int xSize, int ySize, float scale, float gap)
        {
            cells = new Cell[xSize, ySize];

            float offsetX = -xSize / 2f * scale;
            float offsetY = -ySize / 2f * scale;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    // Instantiate a cell object based on the prefab
                    GameObject cellObject = Instantiate(cellPrefab);
                    CellInterface ic = cellObject.GetComponent<CellInterface>();

                    // Set the name and parent of the cell object
                    cellObject.gameObject.name = i + "_" + j;
                    cellObject.transform.SetParent(transform);

                    // Set the position and scale of the cell object
                    cellObject.transform.localPosition = new Vector3(j * scale + j * gap + offsetX, i * scale + i * gap + offsetY, 0);
                    cellObject.transform.localScale = Vector3.one * scale;

                    // Set the grid position of the cell
                    ic.SetGridPosition(i, j);

                    // Store the cell in the cells array
                    cells[i, j] = ic;
                }
            }
        }

        /// <summary>
        /// Clears the generated cells in the maze.
        /// </summary>
        public void ClearCells()
        {
            if (cells != null)
            {
                foreach (CellInterface ic in cells)
                {
                    MonoBehaviour mb = ic as MonoBehaviour;
                    Destroy(mb.gameObject);
                }

                cells = null;
            }
        }

        /// <summary>
        /// Returns the grid of cells in the maze.
        /// </summary>
        /// <returns>The 2D array of CellInterface representing the maze grid.</returns>
        public CellInterface[,] GetCells()
        {
            return cells;
        }
    }
}
