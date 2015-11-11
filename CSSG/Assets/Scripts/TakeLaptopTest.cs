using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem.UnityGUI;

public class TakeLaptopTest : MonoBehaviour
{
    public static GameObject player;
    public static GameObject leftArm;
    public static GameObject rightArm;
    public static bool carryingLaptop;
    public static bool CanSit;
    public static bool UsingLaptop;
    public GameObject carriedLaptop;
    public static GameObject tempPlayerObj;
    public GameObject testUI;
    public static bool CanReturn;

    private Animator laptopScreenAnim;
    private GameObject uiRoot;
    private GUIControl control;

    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
        player = GameObject.Find("Player");

        testUI = GameObject.FindGameObjectWithTag("Test");
        laptopScreenAnim = testUI.GetComponent<Animator>();
        leftArm = GameObject.FindGameObjectWithTag("LeftArm");
        rightArm = GameObject.FindGameObjectWithTag("RightArm");
        CanSit = true;
        UsingLaptop = false;
        CanReturn = false;
        carryingLaptop = false;

        uiRoot = GameObject.Find("GUIRoot");
        control = uiRoot.GetComponent<GUIControl>();
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
        if (carryingLaptop && carriedLaptop != null)
        {
            UseLaptop();
            CheckDrop();
        }
        else
        {
            Pickup();
        }
    }

    /// <summary> places the object in front of the player
    /// </summary>
    /// <param name="o"></param>
    void UseLaptop()
    {
        if (!UsingLaptop)
        {
            carriedLaptop.gameObject.transform.parent = Camera.main.transform.parent.transform;
            StartCoroutine(DoGrabLaptopAnimation());
            UsingLaptop = true;
            TurnLaptopOn();
        }
    }

    /// <summary> Handles picking up the object
    /// </summary>
    void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanSit)
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                float distance = Vector3.Distance(hit.collider.gameObject.transform.position, Camera.main.transform.position);
                if (hit.collider.gameObject.tag == "IsLaptop" && distance <= 3)
                {
                    carriedLaptop = hit.collider.gameObject;
                    Camera.main.transform.parent.gameObject.transform.parent = carriedLaptop.transform;
                    CanSit = false;

                    PauseMenu.itembar.SetActive(!PauseMenu.itembar.activeSelf);

                    leftArm.SetActive(!leftArm.activeSelf);
                    rightArm.SetActive(!rightArm.activeSelf);

                    StartCoroutine(DoSitAnimation());

                    Camera.main.GetComponent<MouseLook>().enabled = false;

                    if (player.GetComponent<CharacterMotor>())
                    {
                        player.GetComponent<CharacterMotor>().enabled = false;
                        player.GetComponent<MouseLook>().enabled = false;
                    }

                    carriedLaptop.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    carriedLaptop.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }

    /// <summary> Detects if the player wants to drop the object
    /// </summary>
    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanReturn)
        {
            DropObject();
        }
    }

    /// <summary> Handles droping the object
    /// </summary>
    void DropObject()
    {
        carryingLaptop = false;
        if (UsingLaptop)
        {
            TurnLaptopOff();

            PauseMenu.itembar.SetActive(!PauseMenu.itembar.activeSelf);

            StartCoroutine(DoReturnAnimation());
        }
    }

    /// <summary> Performs the sit animation
    /// </summary>
    IEnumerator DoSitAnimation()
    {

        Camera.main.transform.parent.GetComponent<Animation>().Play("SitDown");
        yield return new WaitForSeconds(1.5f);
        Camera.main.transform.parent.gameObject.transform.parent = null;
        carryingLaptop = true;
    }

    /// <summary> Performs the return laptop animation
    /// </summary>
    IEnumerator DoReturnAnimation()
    {
        carriedLaptop.GetComponent<Animation>().Play("ReturnLaptop");
        yield return new WaitForSeconds(1.5f);
        carriedLaptop.gameObject.transform.parent = null;
        carriedLaptop = null;
        UsingLaptop = false;

        Camera.main.transform.parent.gameObject.transform.Translate(Vector3.up * .3f);

        Camera.main.GetComponent<MouseLook>().enabled = true;

        if (player.GetComponent<CharacterMotor>())
        {
            player.GetComponent<CharacterMotor>().enabled = true;
            player.GetComponent<MouseLook>().enabled = true;
        }

        leftArm.SetActive(!leftArm.activeSelf);
        rightArm.SetActive(!rightArm.activeSelf);
        CanSit = true;
        CanReturn = false;
    }

    IEnumerator DoGrabLaptopAnimation()
    {
        carriedLaptop.GetComponent<Animation>().Play("GrabLaptop");
        yield return new WaitForSeconds(2f);
        CanReturn = true;
    }

    /// <summary> triggers the laptop screen on animation
    /// </summary>
	void TurnLaptopOn()
    {
        control.scaledRect.y = ScaledValue.FromNormalizedValue(-0.50f);
        control.Refresh();
        laptopScreenAnim.SetTrigger("FadeIn");
    }

    /// <summary> triggers the laptop screen off animation
    /// </summary>
    void TurnLaptopOff()
    {
        control.scaledRect.y = ScaledValue.FromNormalizedValue(0.15f);
        control.Refresh();
        laptopScreenAnim.SetTrigger("FadeOut");
    }
}
