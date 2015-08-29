using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour
{
    private int one;
    private int two;
    private int three;
    private int four;
    private int five;
    private int six;
    private Vector3 tempmouse;
    private int wait;

    public Text movetext;
	public GameObject up;
	public GameObject down;
	public GameObject left;
	public GameObject right;
	public GameObject arrowLeft;
	public GameObject arrowRight;
	public GameObject inter;
	public GameObject info;
	public GameObject key;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
		movetext = movetext.gameObject.GetComponent<UnityEngine.UI.Text> ();
		info.SetActive (true);
		key.SetActive (true);
		movetext.text = "To move forward hit the W key.";
		up.SetActive (!up.activeSelf);
		one = 1;
		two = 0;
		three = 0;
		four = 0;
		five = 0;
		six = 0;
		wait = 0;
	}

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
        if (wait < 75)
        {
            wait++;
        }

		if (Input.GetKeyUp (KeyCode.W) &&  one == 1 && wait >= 75 )
        {
			one = 0;
			two = 1;
			wait = 0;
			up.SetActive (!up.activeSelf);
			down.SetActive (!down.activeSelf);
			movetext.text = "To move backward hit the S key.";
	    }

		if (Input.GetKeyUp (KeyCode.S) && two == 1 && wait >= 75 )
        {
			two = 0 ;
			three = 1;
			wait = 0;
			down.SetActive (!down.activeSelf);
			left.SetActive (!left.activeSelf);
			movetext.text = "To move left hit the A key.";
		}

		if (Input.GetKeyUp (KeyCode.A) && three == 1 && wait >= 75 )
        {
			three = 0 ;
			four = 1;
			wait = 0;
			left.SetActive (!left.activeSelf);
			right.SetActive (!right.activeSelf);
			movetext.text = "To move right hit the D key.";
		}

		if (Input.GetKeyUp (KeyCode.D) && four == 1 && wait >= 75 )
        {
			four = 0;
			five =1;
			wait = 0;
			right.SetActive (!right.activeSelf);
			arrowLeft.SetActive(!arrowLeft.activeSelf);
			arrowRight.SetActive(!arrowRight.activeSelf);
			tempmouse = Input.mousePosition;
			movetext.text = "To turn move the mouse left or right.";
		}

		if ((tempmouse != Input.mousePosition)  && five == 1 && wait >= 75 )
        {
			five = 0;
			six = 1;
			arrowLeft.SetActive(!arrowLeft.activeSelf);
			arrowRight.SetActive(!arrowRight.activeSelf);
			movetext.text = "To intract hit the E key.";
			inter.SetActive (!inter.activeSelf);

		}

		if (Input.GetKeyUp (KeyCode.E) && six == 1 && wait >= 75 )
        {
			six = 0;
			wait = 0;
			inter.SetActive (!inter.activeSelf);
			info.SetActive(false);
			key.SetActive(false);
			this.enabled = false;
		}
	}
}