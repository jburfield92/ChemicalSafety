using UnityEngine;

public class HelpMenus : MonoBehaviour 
{
	public GameObject MouseSensHelpMenu;
	public GameObject FontSizeHelpMenu;
	public GameObject BrightnessHelpMenu;
	public GameObject GameVolumeHelpMenu;

	public GameObject ControlsHelpMenu;
	public GameObject DialogueHelpMenu;

    public GameObject ProgresssHelpMenu;
    public GameObject TestResultsHelpMenu;

    public GameObject ProgressPanel;
    public GameObject TestResultsPanel;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	/// <summary>
	/// Activates the mouse sens help menu.
	/// </summary>
	public void ActivateMouseSensHelp()
	{
		MouseSensHelpMenu.SetActive (!MouseSensHelpMenu.activeSelf);
		FontSizeHelpMenu.SetActive (false);
		BrightnessHelpMenu.SetActive (false);
		GameVolumeHelpMenu.SetActive (false);
	}

	/// <summary>
	/// Activates the font size help menu.
	/// </summary>
	public void ActivateFontSizeMenu()
	{
		MouseSensHelpMenu.SetActive (false);
		FontSizeHelpMenu.SetActive (!FontSizeHelpMenu.activeSelf);
		BrightnessHelpMenu.SetActive (false);
		GameVolumeHelpMenu.SetActive (false);
	}

	/// <summary>
	/// Activates the brightness help menu.
	/// </summary>
	public void ActivateBrightnessMenu()
	{
		MouseSensHelpMenu.SetActive (false);
		FontSizeHelpMenu.SetActive (false);
		BrightnessHelpMenu.SetActive (!BrightnessHelpMenu.activeSelf);
		GameVolumeHelpMenu.SetActive (false);
	}

	/// <summary>
	/// Activates the game volume help menu.
	/// </summary>
	public void ActivateGameVolumeHelp()
	{
		MouseSensHelpMenu.SetActive (false);
		FontSizeHelpMenu.SetActive (false);
		BrightnessHelpMenu.SetActive (false);
		GameVolumeHelpMenu.SetActive (!GameVolumeHelpMenu.activeSelf);
	}

	/// <summary>
	/// Deactivates all setting help menus.
	/// </summary>
	public void DeactivateAllSettingHelps()
	{
		MouseSensHelpMenu.SetActive (false);
		FontSizeHelpMenu.SetActive (false);
		BrightnessHelpMenu.SetActive (false);
		GameVolumeHelpMenu.SetActive (false);
	}
	
	/// <summary>
	/// Activates the control help menu.
	/// </summary>
	public void ActivateControlHelp()
	{
		ControlsHelpMenu.SetActive (!ControlsHelpMenu.activeSelf);
		DialogueHelpMenu.SetActive (false);
	}

	/// <summary>
	/// Activates the dialogue help menu.
	/// </summary>
	public void ActivateDialogueHelp()
	{
		ControlsHelpMenu.SetActive (false);
		DialogueHelpMenu.SetActive (!DialogueHelpMenu.activeSelf);
	}

	/// <summary>
	/// Deactivates all Help Menu help menus.
	/// </summary>
	public void DeactivateHelpMenuHelps()
	{
		ControlsHelpMenu.SetActive (false);
		DialogueHelpMenu.SetActive (false);
	}

    /// <summary>
    /// Deactivates all Results Menu help menus.
    /// </summary>
    public void DeactivateResultsMenuHelps()
    {
        ProgresssHelpMenu.SetActive(false);
        TestResultsHelpMenu.SetActive(false);
    }

    /// <summary> Deactivates the test results panel
    /// </summary>
    public void DeactivateResultsPanel()
    {
        TestResultsPanel.SetActive(false);
    }

    /// <summary> Deactivates the progress panel
    /// </summary>
    public void DeactivateProgressPanel()
    {
        ProgressPanel.SetActive(false);
    }

}
