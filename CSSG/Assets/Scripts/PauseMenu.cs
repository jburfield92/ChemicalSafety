using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject onOff;
	public static GameObject itembar;
	public GameObject settings;

    private GameObject npc;
    private GUIRoot guiRoot;
    private AudioSource source;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
        npc = GameObject.FindGameObjectWithTag("NPC");
        source = (AudioSource)npc.GetComponent("AudioSource");
		itembar = GameObject.FindGameObjectWithTag ("ItemBarPanel");
    }
    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    { 
		if (Input.GetKeyUp(KeyCode.Escape) && ((onOff.activeSelf)|| PickupObject.canRun))
        {
            onOff.SetActive(!onOff.activeSelf);
		
			itembar.SetActive(!itembar.activeSelf);
			PickupObject.canRun = !PickupObject.canRun;
		}

		if (PickupObject.canRun)
        {
            if (!source.isPlaying)
            {
                source.UnPause();
            }

            if (guiRoot != null)
            {
                guiRoot.visible = true;
            }
            else
            {
                guiRoot = DialogueManager.DisplaySettings.dialogueUI.GetComponentInChildren<GUIRoot>();
            }

            if (DialogueManager.IsConversationActive)
            {
                DialogueManager.Unpause();
            }

            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            if (source.isPlaying)
            {
                source.Pause();
            }

            if (guiRoot != null)
            {
                guiRoot.visible = false;
            }
            else
            {
                guiRoot = DialogueManager.DisplaySettings.dialogueUI.GetComponentInChildren<GUIRoot>();
            }

            if (DialogueManager.IsConversationActive)
            {
                DialogueManager.Pause();
            }

            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0f;
            }
		}


	}

    /// <summary> handles switching our running state
    /// </summary>
	public void Running()
    {
		PickupObject.canRun = !PickupObject.canRun;
	}
}
