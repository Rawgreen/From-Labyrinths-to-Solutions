using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// The PathfinderSettings class manages the collection of available pathfinding algorithms.
    /// </summary>
    public class PathfinderSettings : MonoBehaviour
    {
        [SerializeField] private PathfinderInterface[] pathfinders;

        /// <summary>
        /// Initializes the pathfinders array by adding instances of the supported pathfinding algorithms.
        /// </summary>
        private void Awake()
        {
            pathfinders = new PathfinderInterface[3];
            pathfinders[0] = gameObject.AddComponent<BFS>();
            pathfinders[1] = gameObject.AddComponent<DFS>();
            pathfinders[2] = gameObject.AddComponent<AStar>();
        }

        /// <summary>
        /// Returns an array of available pathfinding algorithms.
        /// </summary>
        /// <returns>An array of PathfinderInterface representing the available pathfinding algorithms.</returns>
        public PathfinderInterface[] GetPathfinders()
        {
            return pathfinders;
        }
    }
}