using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Key : MonoBehaviour
{
	private int hotkeyOne;
	private int hotkeyTwo;
	private int hotkeyThree;
	private int qkey;
	private int enterkey;
    private KeyCode temp;

    public static KeyCode one;
    public static KeyCode two;
    public static KeyCode three;
	public static KeyCode four;
	public static KeyCode five;
	public static KeyCode six;
	public static KeyCode enter;
    public static KeyCode q;
	
	public Text keyOne;
	public Text keyTwo;
	public Text keyThree;
	public Text keyEnter;
    public Text qEnter;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
        enter = KeyCode.E;
        q = KeyCode.Q;
        one = KeyCode.Alpha1;
        two = KeyCode.Alpha2;
        three = KeyCode.Alpha3;
		four = KeyCode.Alpha4;
		five = KeyCode.Alpha5;
		six = KeyCode.Alpha6;
		
		keyOne = keyOne.gameObject.GetComponent<Text> ();
		keyTwo = keyTwo.gameObject.GetComponent<Text> ();
		keyThree = keyThree.gameObject.GetComponent<Text> ();
		keyEnter = keyEnter.gameObject.GetComponent<Text> ();
		qEnter = qEnter.gameObject.GetComponent<Text> ();

		enterkey = 0;
		hotkeyOne = 0;
		hotkeyTwo = 0;
		hotkeyThree = 0;
		qkey = 0;
	}

    /// <summary> Sets the key for first inventory spot
    /// </summary>
	public void SetKeyInventoryOne()
    {       
		hotkeyOne = 1;
		hotkeyTwo = 0;
		hotkeyThree = 0;
		enterkey = 0;
		qkey = 0;
	}

    /// <summary> Sets the key for second inventory spot
    /// </summary>
	public void SetKeyInventoryTwo()
    {
		hotkeyOne = 0;
		hotkeyTwo = 1;
		hotkeyThree = 0;
		enterkey = 0;
		qkey = 0;
	}

    /// <summary> Sets the key for third inventory spot
    /// </summary>
	public void SetKeyInventoryThree()
    {
		hotkeyOne = 0;
		hotkeyTwo = 0;
		hotkeyThree = 1;
		enterkey = 0;
		qkey = 0;
	}

    /// <summary> Sets the key for the interaction key
    /// </summary>
	public void SetInteractionKey()
    {
		hotkeyOne = 0;
		hotkeyTwo = 0;
		hotkeyThree = 0;
		enterkey = 1;
		qkey = 0;
	}

    /// <summary> Sets the key for the collect key
    /// </summary>
	public void SetCollectKey()
    {
		hotkeyOne = 0;
		hotkeyTwo = 0;
		hotkeyThree = 0;
		enterkey = 0;
		qkey = 1;
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
                if (!(temp == one || temp == two || temp == three || temp == q
                     || temp == KeyCode.W || temp == KeyCode.A || temp == KeyCode.S || temp == KeyCode.D))
                {
                    enter = temp;
                    keyEnter.text = Input.inputString;
                }
                enterkey = 0;

            }

            if (qkey == 1 && Input.anyKeyDown)
            {
                if (!(temp == one || temp == two || temp == three || temp == enter
                     || temp == KeyCode.W || temp == KeyCode.A || temp == KeyCode.S || temp == KeyCode.D))
                {
                    q = temp;
                    qEnter.text = Input.inputString;
                }
                qkey = 0;

            }

            if (hotkeyOne == 1 && Input.anyKeyDown)
            {
                if (!(temp == enter || temp == two || temp == three || temp == q
                     || temp == KeyCode.W || temp == KeyCode.A || temp == KeyCode.S || temp == KeyCode.D))
                {
                    one = temp;
                    keyOne.text = Input.inputString;
                }
                hotkeyOne = 0;
            }


            if (hotkeyTwo == 1 && Input.anyKeyDown)
            {
                if (!(temp == one || temp == enter || temp == three || temp == q
                     || temp == KeyCode.W || temp == KeyCode.A || temp == KeyCode.S || temp == KeyCode.D))
                {
                    two = temp;
                    keyTwo.text = Input.inputString;
                }
                hotkeyTwo = 0;
            }
            if (hotkeyThree == 1 && Input.anyKeyDown)
            {
                if (!(temp == one || temp == two || temp == three || temp == q
                     || temp == KeyCode.W || temp == KeyCode.A || temp == KeyCode.S || temp == KeyCode.D))
                {
                    three = temp;
                    keyThree.text = Input.inputString;
                }
                hotkeyThree = 0;
            }
        }
    }
}