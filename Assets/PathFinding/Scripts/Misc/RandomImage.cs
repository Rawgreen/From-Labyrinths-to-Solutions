using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PathFinding
{
    /// <summary>
    /// Randomly selects and sets a sprite image from a collection of images onto the attached Image component.
    /// </summary>
    public class RandomImage : MonoBehaviour
    {
        [SerializeField] Sprite[] images;

        /// <summary>
        /// Sets a random sprite image from the collection onto the attached Image component.
        /// </summary>
        void Start()
        {
            GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
        }
    }
}