using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private GameObject mainCamera;
    public static bool carrying;
    public static GameObject carriedObject;
    public float distance;
    public float smooth;

    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
        if (carrying && carriedObject != null)
        {
            carry(carriedObject);
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
	void carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance - new Vector3(0, 0.75f), Time.deltaTime * smooth);
        o.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0, 180, 0);
    }

    /// <summary> Handles picking up the object
    /// </summary>
	void Pickup()
    {
        if (Input.GetKeyDown(Key.enter))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null)
                {
                    carrying = true;
                    carriedObject = p.gameObject;
                    p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
    }

    /// <summary> Detects if the player wants to drop the object
    /// </summary>
	void CheckDrop()
    {
        if (Input.GetKeyDown(Key.enter))
        {
            DropObject();
        }
    }

    /// <summary> Handles droping the object
    /// </summary>
	void DropObject()
    {
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject = null;
    }
}