using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PathFinding
{
    /// <summary>
    /// Manages the color selection and manipulation for the maze visualization.
    /// </summary>
    public class ColorChooser : MonoBehaviour
    {
        private List<ColorUi> colors;
        [SerializeField] private Transform parent;
        [SerializeField] private ColorUi colorPrefab;

        [SerializeField] private Slider redSlider, blueSlider, greenSlider;
        [SerializeField] private TMP_Text redText, blueText, greenText;

        [SerializeField] private Image addImage;

        // Start is called before the first frame update
        void Start()
        {
            colors = new List<ColorUi>();

            // Instantiate and setup color UI elements for each predefined color
            foreach (Color c in PathfindingTools.GetColors())
            {
                var newColor = Instantiate(colorPrefab);
                newColor.transform.SetParent(parent);

                newColor.Setup(c);
                colors.Add(newColor);

                newColor.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Adds a new color based on the current values of the RGB sliders.
        /// </summary>
        public void AddColor()
        {
            var newColor = Instantiate(colorPrefab);
            newColor.transform.SetParent(parent);

            newColor.Setup(new Color(redSlider.value, greenSlider.value, blueSlider.value));
            colors.Add(newColor);

            newColor.gameObject.SetActive(true);

            SetColors();
        }

        /// <summary>
        /// Removes the specified color from the list of colors.
        /// </summary>
        /// <param name="c">The color UI element to remove.</param>
        public void RemoveColor(ColorUi c)
        {
            if (colors.Count == 1) return;

            colors.Remove(c);
            Destroy(c.gameObject);
        }

        private void SetColors()
        {
            Color[] colorArray = new Color[colors.Count];

            for (int i = 0; i < colorArray.Length; i++)
                colorArray[i] = colors[i].color;

            PathfindingTools.SetColors(colorArray);
        }

        /// <summary>
        /// Updates the text value for the red slider and updates the add image color.
        /// </summary>
        public void SetRedSlider()
        {
            redText.text = (int)(255 * redSlider.value) + "";
            SetAddImage();
        }

        /// <summary>
        /// Updates the text value for the blue slider and updates the add image color.
        /// </summary>
        public void SetBlueSlider()
        {
            blueText.text = (int)(255 * blueSlider.value) + "";
            SetAddImage();
        }

        /// <summary>
        /// Updates the text value for the green slider and updates the add image color.
        /// </summary>
        public void SetGreenSlider()
        {
            greenText.text = (int)(255 * greenSlider.value) + "";
            SetAddImage();
        }

        /// <summary>
        /// Increases the sibling index of the specified color UI element.
        /// </summary>
        /// <param name="c">The color UI element to move.</param>
        public void IncreaseTransformChildPosition(ColorUi c)
        {
            c.transform.SetSiblingIndex(c.transform.GetSiblingIndex() + 1);
        }

        private void SetAddImage()
        {
            addImage.color = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        }
    }
}
