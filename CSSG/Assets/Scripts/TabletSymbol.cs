using UnityEngine;

public class TabletSymbol : MonoBehaviour
{
    private Vector3 x;
    private Quaternion Rx;
    public static bool Run;
    public GameObject TempArm;

    public static GameObject leftArm;
    public static GameObject rightArm;

    // Use this for initialization
    void Start ()
    {
		leftArm = GameObject.FindGameObjectWithTag ("LeftArm");
		rightArm = GameObject.FindGameObjectWithTag ("RightArm");
		x = transform.position;
		Rx = transform.rotation;
		Run = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Run)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int x = Screen.width / 2;
                int y = Screen.height / 2;

                Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "IsTablet" && !PickupObject.carrying && Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position) < 3.0f)
                    {
                        PauseMenu.itembar.SetActive(!PauseMenu.itembar.activeSelf);
                        leftArm.SetActive(!leftArm.activeSelf);
                        rightArm.SetActive(!rightArm.activeSelf);
                        PickupObject.canRun = !PickupObject.canRun;

                        if (Run)
                        {
                            Pullin();
                            TempArm.SetActive(!TempArm.activeSelf);
                            Run = false;
                        }
                    }
                }
            }
		}
	}

	void Pullin()
    {
		transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 0.7f) ;
		Vector3 posi = Camera.main.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation (posi);
		transform.Rotate (90.0f,180.0f,0.0f);
	}

	public void Putback()
    {
	    TempArm.SetActive (!TempArm.activeSelf);
	    transform.position = x;
	    transform.rotation = Rx;
		PauseMenu.itembar.SetActive (!PauseMenu.itembar.activeSelf);
		leftArm.SetActive (!leftArm.activeSelf);
		rightArm.SetActive (!rightArm.activeSelf);

		PickupObject.canRun = !PickupObject.canRun;
		Run = true;
	}
}