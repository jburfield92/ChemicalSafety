using PixelCrushers.DialogueSystem;
using UnityEngine;

public class SymbolRoom1 : MonoBehaviour
{
	private bool pointOne; 
	private bool pointTwo;
	private bool[] BannersCheck =  new bool[9];
	public GameObject[] Banners = new GameObject[9];
	public GameObject TempArm;
	public GameObject PointOneSign;
	public GameObject PointTwoSign;
	public GameObject PointTwoSign2;
    private bool playVoiceFirst = false;
    private GameObject banner;

    // Use this for initialization
    void Start ()
    {
        Camera.main.GetComponent<MouseLook>().enabled = true;
        PickupObject.canRun = true;
        TakeLaptopTest.UsingLaptop = false;

        int i = 0;
        
        pointOne = false;
		pointTwo = false;
		
        for (i = 0; i < 9; i++)
        {
            BannersCheck[i] = false;
        }
	}

    // Update is called once per frame
    void Update()
    {
        int i;

        if (pointOne)
        {
            Vector3 posi = Camera.main.transform.position - PointTwoSign.transform.position;
            PointTwoSign.transform.rotation = Quaternion.LookRotation(posi);
            PointTwoSign.transform.Rotate(0.0f, 90.0f, 0.0f);
            posi = Camera.main.transform.position - PointTwoSign2.transform.position;
            PointTwoSign2.transform.rotation = Quaternion.LookRotation(posi);
            PointTwoSign2.transform.Rotate(0.0f, 90.0f, 0.0f);

            for (i = 0; i < 9; i++)
            {
                if (Banners[i].activeSelf)
                {
                    BannersCheck[i] = true;
                }
            }

            bool Ban = true;

            for (i = 0; i < 9 && Ban; i++)
            {
                if (!BannersCheck[i])
                {
                    Ban = false;
                }
            }

            if (Ban)
            {
                PointTwoSign2.SetActive(!PointTwoSign2.activeSelf);
                PointTwoSign.SetActive(!PointTwoSign.activeSelf);
                DialogueManager.Instance.SendMessage("OnSequencerMessage", "BannerEnd");
                pointOne = false;
            }
        }

        if (PointOneSign.activeSelf == true)
        {
            Vector3 posi = Camera.main.transform.position - PointOneSign.transform.position;
            PointOneSign.transform.rotation = Quaternion.LookRotation(posi);
            PointOneSign.transform.Rotate(0.0f, 90.0f, 0.0f);
        }

        if (pointTwo)
        {
            if (TempArm.activeSelf)
            {
                PointOneSign.SetActive(!PointOneSign.activeSelf);
                DialogueManager.Instance.SendMessage("OnSequencerMessage", "InfoEnd");
                pointTwo = false;
            }
        }

      /*  int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Banner" && Vector3.Distance(Camera.main.transform.position, hit.collider.gameObject.transform.position) < 3.0f)
            {
                if (!playVoiceFirst)
                {
                    banner = hit.collider.gameObject;
                    banner.GetComponent<AudioSource>().Play();
                    playVoiceFirst = true;
                }
            }
            else
            {
                if (banner != null)
                {
                    banner.GetComponent<AudioSource>().Stop();
                    playVoiceFirst = false;
                    banner = null;
                }
            }
        } */
    }

    void PointOne()
    {
        pointOne = true;
    }

    void Pointer()
    {
        PointTwoSign2.SetActive(!PointTwoSign2.activeSelf);
        PointTwoSign.SetActive(!PointTwoSign.activeSelf);
    }

    void PointTwo()
    {
        pointTwo = true;
    }

    void Point()
    {
        PointOneSign.SetActive(!PointOneSign.activeSelf);
    }

    void noPickUp()
    {
        TabletSymbol.Run = false;
        PickupObject.carryBlock = true;
    }

    void pickUpOne()
    {
        TabletSymbol.Run = true;
    }

    void pickUpTwo()
    {
        PickupObject.carryBlock = false;
    }
}
