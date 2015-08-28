using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	public GameObject onOff;
	public GameObject itembar;
	public GameObject settings;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {

	}
    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
		if(Input.GetKeyUp(KeyCode.Escape) && ((onOff.activeSelf)|| Items.canRun))
        {
			onOff.SetActive(!onOff.activeSelf);
		
			itembar.SetActive(!itembar.activeSelf);
			Items.canRun = ! Items.canRun;

		}

		if (Items.canRun)
        {
			Time.timeScale = 1.0f;
		}
        else
        {
			Time.timeScale = 0f;
		}
	}

    /// <summary> handles switching our running state
    /// </summary>
	public void Running()
    {
		Items.canRun = !Items.canRun;
	}
}
