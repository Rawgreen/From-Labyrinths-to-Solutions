using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// The GameManager class controls the game flow and user interactions.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager _Instance { get { return _instance; } }
        private bool isInteractable = true;

        [SerializeField] private FieldGenerator cellGenerator;
        private CellInterface targetCell, startCell;

        [SerializeField] private MazeSettings mazeSettings;

        [SerializeField] private CellTypeProps[] cellTypeProps;

        [SerializeField] private UIManager uIManager;

        public static float delay = 0.2f;


        private void Awake()
        {
            _instance = this;
        }

        /// <summary>
        /// Initializes the game by setting the interactability and generating the field.
        /// </summary>
        void Start()
        {
            SetIsInteractable(true);
            GenerateField(mazeSettings.GetSettings()[0]);
        }

        /// <summary>
        /// Handles user input and performs corresponding actions.
        /// </summary>
        void Update()
        {
            HandleInput();
        }

        #region Input
        private void HandleInput()
        {
            if (!isInteractable) return;
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider == null) return;

            CellInterface cell = hit.collider.GetComponent<CellInterface>();
            if (cell == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseButtonDown(cell);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightMouseButtonDown(cell);
            }
            else if (Input.GetMouseButton(0))
            {
                LeftMouseButtonDown(cell);
            }
        }

        internal void SetDelay(float value)
        {
            delay = 1 - value;
        }

        private void LeftMouseButtonDown(CellInterface cell)
        {
            if (!isInteractable) return;

            if (Input.GetKey(KeyCode.LeftControl))
                cell.SetCellType(CellTool.CellType.EMPTY, false);
            else cell.SetCellType(CellTool.CellType.WALL, false);
        }

        private void RightMouseButtonDown(CellInterface cell)
        {
            if (!isInteractable) return;

            if (Input.GetKey(KeyCode.LeftControl))
                SetTargetCell(cell);
            else SetStartCell(cell);
        }

        /// <summary>
        /// Changes the number of steps displayed in the UI.
        /// </summary>
        public void ChangeStepCount(int steps)
        {
            uIManager.SetSteps(steps);
        }

        /// <summary>
        /// Sets the shortest path in the UI.
        /// </summary>
        public void SetShortestPath(int steps)
        {
            uIManager.SetShortestPath(steps);
        }

        /// <summary>
        /// Sets the provided cell as the start cell.
        /// </summary>
        public CellInterface SetStartCell(CellInterface cell)
        {
            if (startCell != null) startCell.SetCellType(CellTool.CellType.EMPTY, false);

            startCell = cell;

            if (cell != null)
                cell.SetCellType(CellTool.CellType.START, false);

            return startCell;
        }

        /// <summary>
        /// Sets the provided cell as the target cell.
        /// </summary>
        public CellInterface SetTargetCell(CellInterface cell)
        {
            if (targetCell != null) targetCell.SetCellType(CellTool.CellType.EMPTY, false);

            targetCell = cell;

            if (cell != null)
                cell.SetCellType(CellTool.CellType.TARGET, false);

            return targetCell;
        }
        #endregion

        /// <summary>
        /// Generates the field based on the provided maze settings.
        /// </summary>
        public void GenerateField(MazeSetting setting)
        {
            if (!isInteractable) return;

            targetCell = null;
            startCell = null;

            cellGenerator.ClearCells();

            cellGenerator.GenerateMap(setting);
        }

        /// <summary>
        /// Generates a maze using the provided maze generator.
        /// </summary>
        public void GenerateMaze(MazeGeneratorInterface generator)
        {
            if (!isInteractable) return;

            targetCell = null;
            startCell = null;

            SetIsInteractable(false);
            generator.Generate(cellGenerator.GetCells());
        }

        /// <summary>
        /// Finds the path using the provided pathfinder.
        /// </summary>
        public void FindPath(PathfinderInterface pathfinder)
        {
            if (!isInteractable) return;

            CellInterface[,] cells = cellGenerator.GetCells();

            if (startCell == null)
            {
                startCell = SetStartCell(cells[UnityEngine.Random.Range(0, cells.GetLength(0)), UnityEngine.Random.Range(0, cells.GetLength(1))]);
            }

            if (targetCell == null)
            {
                targetCell = SetTargetCell(cells[UnityEngine.Random.Range(0, cells.GetLength(0)), UnityEngine.Random.Range(0, cells.GetLength(1))]);
            }

            SetIsInteractable(false);

            pathfinder.FindPath(cells, startCell, targetCell);
        }

        /// <summary>
        /// Sets the interactability of the game.
        /// </summary>
        public void SetIsInteractable(bool b)
        {
            isInteractable = b;
        }

        /// <summary>
        /// Retrieves the properties of a cell type.
        /// </summary>
        public CellTypeProps GetCellTypeProps(int n)
        {
            return cellTypeProps[n];
        }
    }
}
