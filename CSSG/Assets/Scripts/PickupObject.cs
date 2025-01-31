﻿using UnityEngine;

public class PickupObject : MonoBehaviour
{
<<<<<<< HEAD
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
=======
	private GameObject mainCamera;

	public GameObject rightHand;
	public GameObject rightHandTemp;
	public static GameObject rightHandPosition;
	private static GameObject rightHandPositionLocation;

	public GameObject leftArm;
	public GameObject leftHand;
	public GameObject leftHandPosition;
	private static GameObject leftHandPositionLocation;
	public static GameObject leftArmTemp;
	private static GameObject leftDiff;
	private static Vector3 dropping;

    public static bool carrying;
	public static bool carryBlock;
    public static GameObject carriedObject;
	public float distance;
	public float smooth;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
		mainCamera = GameObject.FindWithTag ("MainCamera");
		rightHandPosition = rightHandTemp;
		rightHandPositionLocation = rightHand;
		leftArmTemp = leftArm;
		leftDiff = leftHand;
		leftHandPositionLocation = leftHandPosition;
		carryBlock = false;
	}

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
		if (carrying && carriedObject != null)
        {
			if(!carryBlock){
			carry(carriedObject);
			CheckDrop();
			}
			else{
				carriedObject.tag = "ToDelete";
				carrying = false;
				carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
				carriedObject.transform.SetParent (RandomRoom.used.transform);
				carriedObject.transform.position = dropping;
				carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(carriedObject.transform.forward * Time.deltaTime);
				carriedObject = null;
			}
		}
        else
        {
			rightHandPosition.transform.position = rightHand.transform.position;
			leftArm.transform.position = leftHandPosition.transform.position;
			if(!carryBlock)
			Pickup();
		}
	}
>>>>>>> David

    /// <summary> places the object in front of the player
    /// </summary>
    /// <param name="o"></param>
	void carry(GameObject o)
    {
<<<<<<< HEAD
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance - new Vector3(0, 0.75f), Time.deltaTime * smooth);
        o.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0, 180, 0);
    }
=======



       // o.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0,180,0);
		//dropping = rightHandPosition.transform.position;
		//LeftHand.transform.position = LeftHandPosition.transform.position;
	}
>>>>>>> David

    /// <summary> Handles picking up the object
    /// </summary>
	void Pickup()
    {
<<<<<<< HEAD
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
=======
		if(Input.GetKeyDown (Key.enter))
        {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
            {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null)
                {
					carrying = true;
					carriedObject = p.gameObject;
					carriedObject.transform.rotation = mainCamera.transform.rotation;
					SetArms();
					carriedObject.transform.position = rightHandPosition.transform.position;
					carriedObject.transform.SetParent(rightHand.transform.parent);
					carriedObject.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0,180,0);
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
		}
	}
	/// <summary>
	/// Sets the Arms location.
	/// </summary>
	public static void SetArms(){

		Transform RightHandPlacment = carriedObject.transform.Find ("RightSpot");
		rightHandPosition.transform.position = rightHandPositionLocation.transform.position;
		float x = ( rightHandPosition.transform.position.x + (carriedObject.transform.position.x  - RightHandPlacment.transform.position.x));
		float y = ( rightHandPosition.transform.position.y +  (carriedObject.transform.position.y  - RightHandPlacment.transform.position.y));
		float z = ( rightHandPosition.transform.position.z + (carriedObject.transform.position.z  - RightHandPlacment.transform.position.z));
		rightHandPosition.transform.position = new Vector3(x,y,z);
		carriedObject.transform.position = rightHandPosition.transform.position;
		carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		Transform LeftHandPlacment = carriedObject.transform.Find ("LeftSpot");
		leftArmTemp.transform.position = leftHandPositionLocation.transform.position;
		x = ( LeftHandPlacment.transform.position.x + leftArmTemp.transform.position.x - leftDiff.transform.position.x);
		y = ( LeftHandPlacment.transform.position.y + leftArmTemp.transform.position.y - leftDiff.transform.position.y);
		z = ( LeftHandPlacment.transform.position.z + leftArmTemp.transform.position.z - leftDiff.transform.position.z);
		leftArmTemp.transform.position = new Vector3(x,y,z);
	
	}
>>>>>>> David

    /// <summary> Detects if the player wants to drop the object
    /// </summary>
	void CheckDrop()
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(Key.enter))
        {
            DropObject();
        }
    }
=======
		if (Input.GetKeyDown (Key.enter))
        {
			DropObject ();
		}
	}
>>>>>>> David

    /// <summary> Handles droping the object
    /// </summary>
	void DropObject()
    {
<<<<<<< HEAD
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject = null;
    }
=======
		carrying = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
		carriedObject.transform.SetParent (RandomRoom.used.transform);
		carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		//carriedObject.transform.position = dropping;
		carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(carriedObject.transform.forward);
		carriedObject = null;
	}
>>>>>>> David
}