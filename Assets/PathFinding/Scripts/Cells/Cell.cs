using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    /// <summary>
    /// Represents a cell in a grid-based pathfinding system.
    /// </summary>
    public class Cell : MonoBehaviour, CellInterface
    {
        // Private variables
        private int x;                          // The x-coordinate of the cell in the grid
        private int y;                          // The y-coordinate of the cell in the grid
        private int helperNumber;               // A helper number associated with the cell
        private bool isVisited;                  // Flag indicating if the cell has been visited during pathfinding
        private CellInterface _parentCellInterface;   // Reference to the parent cell used for path reconstruction

        [SerializeField] private CellTool.CellType type;   // The type of the cell

        private SpriteRenderer spriteRenderer;    // Reference to the sprite renderer component attached to the cell

        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetCellType(CellTool.CellType.EMPTY, true);
        }

        /// <summary>
        /// Sets the grid position of the cell.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell in the grid.</param>
        /// <param name="y">The y-coordinate of the cell in the grid.</param>
        public void SetGridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Gets the x-coordinate of the cell in the grid.
        /// </summary>
        /// <returns>The x-coordinate of the cell.</returns>
        public int GetXValue()
        {
            return x;
        }

        /// <summary>
        /// Gets the y-coordinate of the cell in the grid.
        /// </summary>
        /// <returns>The y-coordinate of the cell.</returns>
        public int GetYValue()
        {
            return y;
        }

        /// <summary>
        /// Performs a color change animation on the cell.
        /// </summary>
        /// <param name="newColor">The target color of the cell.</param>
        /// <param name="time">The duration of the animation in seconds.</param>
        /// <returns>An enumerator for the animation coroutine.</returns>
        private IEnumerator ColorChangeAnimation(Color newColor, float time)
        {
            Color prevColor = spriteRenderer.color;

            float t = 0;
            while (t < time)
            {
                t += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(prevColor, newColor, t / time);

                yield return null;
            }

            spriteRenderer.color = newColor;
        }

        /// <summary>
        /// Sets the type of the cell and updates its appearance.
        /// </summary>
        /// <param name="type">The new type of the cell.</param>
        /// <param name="forceChange">If true, the color change is instant without animation.</param>
        public void SetCellType(CellTool.CellType type, bool forceChange)
        {
            this.type = type;

            CellTypeProps cellType = GameManager._Instance.GetCellTypeProps((int)type);
            Color newColor = cellType.GetColor();
            spriteRenderer.sprite = cellType.GetSprite();

            SetColor(newColor, forceChange);
        }

        /// <summary>
        /// Sets the color of the cell.
        /// </summary>
        /// <param name="color">The new color of the cell.</param>
        /// <param name="forceChange">If true, the color change is instant without animation.</param>
        public void SetColor(Color color, bool forceChange)
        {
            StopAllCoroutines();

            if (!forceChange)
                StartCoroutine(ColorChangeAnimation(color, 0.5f));
            else
            {
                StartCoroutine(ColorChangeAnimation(color, 0.0f));
            }
        }

        /// <summary>
        /// Gets the type of the cell.
        /// </summary>
        /// <returns>The type of the cell.</returns>
        public CellTool.CellType GetCellType()
        {
            return type;
        }

        /// <summary>
        /// Gets the helper number associated with the cell.
        /// </summary>
        /// <returns>The helper number.</returns>
        public int GetHelperNum()
        {
            return helperNumber;
        }

        /// <summary>
        /// Sets the helper number associated with the cell.
        /// </summary>
        /// <param name="n">The new helper number.</param>
        public void SetHelperNum(int n)
        {
            helperNumber = n;
        }

        /// <summary>
        /// Checks if the cell has been visited during pathfinding.
        /// </summary>
        /// <returns>True if the cell has been visited, false otherwise.</returns>
        public bool GetIsVisited()
        {
            return isVisited;
        }

        /// <summary>
        /// Sets the visited status of the cell.
        /// </summary>
        /// <param name="b">The new visited status.</param>
        public void SetIsVisited(bool b)
        {
            isVisited = b;
        }

        /// <summary>
        /// Gets the parent cell interface used for path reconstruction.
        /// </summary>
        /// <returns>The parent cell interface.</returns>
        public CellInterface GetParentCell()
        {
            return _parentCellInterface;
        }

        /// <summary>
        /// Sets the parent cell interface used for path reconstruction.
        /// </summary>
        /// <param name="ic">The parent cell interface.</param>
        public void SetParentCell(CellInterface ic)
        {
            _parentCellInterface = ic;
        }
    }
}
