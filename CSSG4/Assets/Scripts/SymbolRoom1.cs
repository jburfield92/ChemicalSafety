using PixelCrushers.DialogueSystem;
using UnityEngine;

public class SymbolRoom1 : MonoBehaviour
{
	private bool pointOne; 
	private bool pointTwo;
	public GameObject TempArm;
	public GameObject PointOneSign;
	public GameObject PointTwoSign;
	public GameObject PointTwoSign2;
    public GameObject PointThreeSign;
    public GameObject PointThreeSign2;
    private GameObject player;
    private bool playVoiceFirst = false;
    private GameObject banner;

    // Use this for initialization
    void Start ()
    {
        Camera.main.GetComponent<MouseLook>().enabled = true;
        PickupObject.canRun = true;
        TakeLaptopTest.UsingLaptop = false;

        player = GameObject.FindGameObjectWithTag("Player");
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = false;
        ((MouseLook)player.GetComponent("MouseLook")).enabled = false;
        ((MouseLook)Camera.main.GetComponent("MouseLook")).enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PointerToHazardLists()
    {
        PointTwoSign2.SetActive(!PointTwoSign2.activeSelf);
        PointTwoSign.SetActive(!PointTwoSign.activeSelf);
    }

    void PointerToHazardSymbols()
    {
        PointThreeSign.SetActive(!PointThreeSign.activeSelf);
        PointThreeSign2.SetActive(!PointThreeSign2.activeSelf);
    }

    void StartEducation()
    {
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = true;
        ((MouseLook)player.GetComponent("MouseLook")).enabled = true;
        ((MouseLook)Camera.main.GetComponent("MouseLook")).enabled = true;
    }
}
