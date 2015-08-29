using UnityEngine;

public class LockThing : MonoBehaviour
{
    public Texture2D texture;
    public int cursorSizeX;
    public int cursorSizeY;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary> Handles drawing the crosshair
    /// </summary>
    void OnGUI()
    {
<<<<<<< HEAD
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - cursorSizeX / 2, Screen.height / 2 - cursorSizeY / 2, cursorSizeX, cursorSizeY), texture);
        }
=======
        GUI.DrawTexture(new Rect(Screen.width/2 - cursorSizeX/2, Screen.height/2 - cursorSizeY/2, cursorSizeX, cursorSizeY), texture);
>>>>>>> David
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
		if (Items.canRun == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
		}
        else
        {
			Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
	}
}
