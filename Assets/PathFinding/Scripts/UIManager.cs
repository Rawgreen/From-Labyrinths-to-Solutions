using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PathFinding
{
    /// <summary>
    /// Manages the UI elements and user interactions in the game.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MazeSettings mazeSettings;
        [SerializeField] private PathfinderSettings pathfinderSettings;

        [SerializeField] private TMP_Dropdown mazeDropdown;
        [SerializeField] private TMP_Dropdown mazeGeneratorDropdown;
        [SerializeField] private TMP_Dropdown pathfinderDropdown;

        [SerializeField] private Slider delaySlider;

        [Space(10)]
        [Header("Stats of Pathfinding")]
        [SerializeField] private TMP_Text stepCountText;
        [SerializeField] private TMP_Text shortestPathText;
        [SerializeField] private GameObject statObject;

        // Start is called before the first frame update
        void Start()
        {
            SetMazeDropDown();
            SetMazeGeneratorDropDown();
            SetPathfinderDropDown();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Sets the options for the maze generator dropdown based on available maze generators.
        /// </summary>
        private void SetMazeGeneratorDropDown()
        {
            mazeGeneratorDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            optionDatas.Add(new TMP_Dropdown.OptionData("Choose"));

            foreach (MazeGeneratorInterface generator in mazeSettings.GetGenerators())
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = generator.GetName();

                optionDatas.Add(option);
            }

            mazeGeneratorDropdown.AddOptions(optionDatas);
        }

        /// <summary>
        /// Sets the options for the maze dropdown based on available maze settings.
        /// </summary>
        private void SetMazeDropDown()
        {
            mazeDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            optionDatas.Add(new TMP_Dropdown.OptionData("Choose"));

            foreach (MazeSetting mazeSetting in mazeSettings.GetSettings())
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = mazeSetting.xSize + " X " + mazeSetting.ySize;

                optionDatas.Add(option);
            }

            mazeDropdown.AddOptions(optionDatas);
        }

        /// <summary>
        /// Sets the options for the pathfinder dropdown based on available pathfinders.
        /// </summary>
        private void SetPathfinderDropDown()
        {
            pathfinderDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
            optionDatas.Add(new TMP_Dropdown.OptionData("Choose"));

            foreach (PathfinderInterface pathfinder in pathfinderSettings.GetPathfinders())
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = pathfinder.GetName();

                optionDatas.Add(option);
            }

            pathfinderDropdown.AddOptions(optionDatas);
        }

        /// <summary>
        /// Sets the delay for pathfinding based on the value of the delay slider.
        /// </summary>
        public void SetDelaySlider()
        {
            GameManager._Instance.SetDelay(delaySlider.value);
        }

        /// <summary>
        /// Handles the button click event for field generation.
        /// </summary>
        public void OnClickFieldGeneration()
        {
            if (mazeDropdown.value == 0) return;

            statObject.SetActive(false);

            GameManager._Instance.GenerateField(mazeSettings.GetSettings()[mazeDropdown.value - 1]);
            mazeDropdown.value = 0;
        }

        /// <summary>
        /// Handles the button click event for maze generation.
        /// </summary>
        public void OnClickMazeGeneration()
        {
            if (mazeGeneratorDropdown.value == 0) return;

            statObject.SetActive(false);

            GameManager._Instance.GenerateMaze(mazeSettings.GetGenerators()[mazeGeneratorDropdown.value - 1]);
            mazeGeneratorDropdown.value = 0;
        }

        /// <summary>
        /// Handles the button click event for pathfinding.
        /// </summary>
        public void OnClickPathfinding()
        {
            if (pathfinderDropdown.value == 0) return;

            statObject.SetActive(true);
            shortestPathText.text = "-";

            GameManager._Instance.FindPath(pathfinderSettings.GetPathfinders()[pathfinderDropdown.value - 1]);
            pathfinderDropdown.value = 0;
        }

        /// <summary>
        /// Sets the step count in the UI.
        /// </summary>
        /// <param name="stepCount">The number of steps taken in the pathfinding algorithm.</param>
        public void SetSteps(int stepCount)
        {
            stepCountText.text = "" + stepCount;
        }

        /// <summary>
        /// Sets the shortest path length in the UI.
        /// </summary>
        /// <param name="stepCount">The length of the shortest path found by the pathfinding algorithm.</param>
        public void SetShortestPath(int stepCount)
        {
            shortestPathText.text = "" + stepCount;
        }
    }
}
