using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;

public class PauseMenu : MonoBehaviour
{
	public GameObject onOff;
	public static GameObject itembar;
	public GameObject settings;

    private GUIRoot guiRoot;

    private bool paused;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
		itembar = GameObject.FindGameObjectWithTag ("ItemBarPanel");
        Time.timeScale = 1.0f;
        DialogueManager.Unpause();
    }
    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    { 
        if (guiRoot == null)
        {
            guiRoot = DialogueManager.DisplaySettings.dialogueUI.GetComponentInChildren<GUIRoot>();
        }

        if (Input.GetKeyUp(KeyCode.Escape) && ((onOff.activeSelf)|| PickupObject.canRun))
        {
            onOff.SetActive(!onOff.activeSelf);
		
			itembar.SetActive(!itembar.activeSelf);
			PickupObject.canRun = !PickupObject.canRun;
		}

		if (PickupObject.canRun && paused)
        {
            paused = false;

            guiRoot.visible = true;

            DialogueManager.Unpause();

            Time.timeScale = 1.0f;      
        }
        else if (!PickupObject.canRun && !paused)
        {
            paused = true;

            guiRoot.visible = false;

            DialogueManager.Pause();

            Time.timeScale = 0f;
		}
	}

    /// <summary> handles switching our running state
    /// </summary>
	public void Running()
    {
		PickupObject.canRun = !PickupObject.canRun;
	}
}
