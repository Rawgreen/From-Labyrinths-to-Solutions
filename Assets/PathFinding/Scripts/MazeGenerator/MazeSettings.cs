using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Manages the maze settings and available maze generator algorithms.
    /// </summary>
    public class MazeSettings : MonoBehaviour
    {
        [SerializeField] private MazeSetting[] settings;                 // Array of maze settings
        [SerializeField] private MazeGeneratorInterface[] generators;    // Array of maze generator algorithms

        /// <summary>
        /// Initializes the maze generator algorithms by adding them as components to the GameObject.
        /// </summary>
        private void Awake()
        {
            generators = new MazeGeneratorInterface[6];

            // Algorithms
            generators[0] = gameObject.AddComponent<RandomWalls>();
            generators[1] = gameObject.AddComponent<BinaryTreeMaze>();
            generators[2] = gameObject.AddComponent<Prim>();
            generators[3] = gameObject.AddComponent<RecursiveBacktracking>();
            generators[4] = gameObject.AddComponent<Kruskal>();
            generators[5] = gameObject.AddComponent<RecursiveDivision>();
        }

        /// <summary>
        /// Returns the array of maze settings.
        /// </summary>
        /// <returns>The array of maze settings.</returns>
        public MazeSetting[] GetSettings()
        {
            return settings;
        }

        /// <summary>
        /// Returns the array of maze generator algorithms.
        /// </summary>
        /// <returns>The array of maze generator algorithms.</returns>
        public MazeGeneratorInterface[] GetGenerators()
        {
            return generators;
        }
    }

    /// <summary>
    /// Represents the settings for generating a maze.
    /// </summary>
    [System.Serializable]
    public class MazeSetting
    {
        public int xSize;                // Width of the maze in number of cells
        public int ySize;                // Height of the maze in number of cells
        public float cellSize;           // Size of each individual cell
        public Vector3 fieldPos;         // Position of the maze field
        public float gapBetweenCells;    // Gap between cells
    }
}
