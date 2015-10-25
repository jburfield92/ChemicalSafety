using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PickupObject : MonoBehaviour
{
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
	//private static Vector3 dropping;
	
	public static bool carrying;
	public static bool carryBlock;
	public static GameObject carriedObject;
	public float distance;
	public float smooth;

	public static bool canRun;

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
		canRun = true;
	}
	
	/// <summary> Update is called once per frame
	/// </summary>
	void Update ()
	{
		if (carrying && carriedObject != null)
		{
			if(!carryBlock)
			{
				//Debug.Log("working");
				CheckDrop();
			}
			else
			{
				carriedObject.tag = "ToDelete";
				carrying = false;
				carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
				carriedObject.transform.SetParent (RandomRoom.used.transform);
				//carriedObject.transform.position = dropping;
				//carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(carriedObject.transform.forward * Time.deltaTime);
				carriedObject = null;
			}
		}
		else
		{
			rightHandPosition.transform.position = rightHand.transform.position;
			leftArm.transform.position = leftHandPosition.transform.position;
			if(!carryBlock)
			{
				Pickup();
			}
		}
	}
	
	/// <summary> Handles picking up the object
	/// </summary>
	void Pickup()
	{
		if(Input.GetKeyDown (Key.enter))
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			int layerMask = 1 << 10;
			layerMask = ~layerMask;
			
			if(Physics.Raycast(ray,out hit,3.0f,layerMask))
			{



				Placement l = hit.collider.GetComponent<Placement>();

				if(l != null){


				}else{


				Pickupable p = hit.collider.GetComponent<Pickupable>();
						if(p.Check != true){
				if(p != null && Vector3.Distance(mainCamera.transform.position, p.transform.position) < 3.0f)
				{
					p.Check = false;
					p.TriggerCheck = false;
					carrying = true;
					carriedObject = p.gameObject;
					carriedObject.transform.rotation = mainCamera.transform.rotation;
					SetArms();
					carriedObject.transform.position = rightHandPosition.transform.position;
					carriedObject.transform.SetParent(rightHand.transform.parent);
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
						carriedObject.GetComponent<Collider>().enabled = false;
						}}}
			}
		}
	}
	/// <summary>
	/// Sets the Arms location.
	/// </summary>
	public static void SetArms()
	{
		Transform RightHandPlacment = carriedObject.transform.Find ("RightSpot");
		carriedObject.transform.rotation = RightHandPlacment.transform.rotation;
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
	
	/// <summary> Detects if the player wants to drop the object
	/// </summary>
	void CheckDrop()
	{

		if (Input.GetKeyDown (Key.enter))
		{
			//Debug.Log("working");
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				Placing p = hit.collider.GetComponent<Placing>();
				if(p == null)
				{
					carriedObject.GetComponent<Collider>().enabled = true;
					DropObject ();
				}else{
					Placement.Place();
				}
			}
		}
	}
	
	/// <summary> Handles droping the object
	/// </summary>
	void DropObject()
	{
		carrying = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
		carriedObject.transform.SetParent (RandomRoom.used.transform);
		carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().AddForce(carriedObject.transform.forward);
		carriedObject = null;
	}
}