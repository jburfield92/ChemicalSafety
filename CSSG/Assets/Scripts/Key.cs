using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private int enterkey;
    private KeyCode temp;

    public static KeyCode enter;
    public Text keyEnter;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
        enter = KeyCode.E;

		keyEnter = keyEnter.gameObject.GetComponent<Text> ();

		enterkey = 0;
	}

    /// <summary> Draws the keys
    /// </summary>
	void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey)
        {
            temp = e.keyCode;

            if (enterkey == 1 && Input.anyKeyDown)
            {
                if (!(temp == KeyCode.W || temp == KeyCode.A || temp == KeyCode.S || temp == KeyCode.D))
                {
                    enter = temp;
                    keyEnter.text = Input.inputString;
                }
                enterkey = 0;

            }
        }
    }
}