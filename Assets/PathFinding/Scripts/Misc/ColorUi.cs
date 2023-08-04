using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PathFinding
{
    /// <summary>
    /// Represents a color UI element used in the ColorChooser class for color selection and visualization.
    /// </summary>
    public class ColorUi : MonoBehaviour
    {
        [SerializeField] private Image colorImage;
        public Color color;

        /// <summary>
        /// Sets up the color UI element with the specified color.
        /// </summary>
        /// <param name="c">The color to set.</param>
        public void Setup(Color c)
        {
            color = c;
            colorImage.color = c;
        }
    }
}