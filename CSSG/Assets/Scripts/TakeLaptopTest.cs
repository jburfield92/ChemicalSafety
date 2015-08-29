using UnityEngine;
using System.Collections;

public class TakeLaptopTest : MonoBehaviour {

	private GameObject mainCamera;
	public static bool carryingLaptop;
	public static bool CanSit;
	public static bool UsingLaptop;
	public static GameObject carriedLaptop;
	public static GameObject tempPlayerObj;

	/// <summary> Use this for initialization
	/// </summary>
	void Start()
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
		CanSit = true;
		UsingLaptop = false;
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
		if (!UsingLaptop) {
			carriedLaptop.GetComponent<Animation>().Play("GrabLaptop");
			UsingLaptop = true;

			// add script to call laptop overlay canvas here
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
			
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
			RaycastHit hit;

			
			if (Physics.Raycast(ray, out hit))
			{
				float distance = Vector3.Distance (hit.collider.gameObject.transform.position, mainCamera.transform.position);
				if (hit.collider.gameObject.tag == "IsLaptop" && distance <= 3){
					carriedLaptop = hit.collider.gameObject;
					mainCamera.transform.parent.gameObject.transform.parent = carriedLaptop.transform;
					CanSit = false;
					StartCoroutine(DoAnimation());


					mainCamera.GetComponent<MouseLook>().enabled = false;
					if (GameObject.Find("Player").GetComponent<CharacterMotor>()) {
						GameObject.Find("Player").GetComponent<CharacterMotor>().enabled = false;
						GameObject.Find("Player").GetComponent<MouseLook>().enabled = false;

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
		if (Input.GetKeyDown(KeyCode.E))
		{
			DropObject();
		}
		CanSit = true;
	}
	
	/// <summary> Handles droping the object
	/// </summary>
	void DropObject()
	{
		carryingLaptop = false;
		if (UsingLaptop) carriedLaptop.GetComponent<Animation> ().Play ("ReturnLaptop");
		UsingLaptop = false;
		carriedLaptop = null;
		mainCamera.transform.parent.gameObject.transform.Translate (Vector3.up * .3f);
		mainCamera.GetComponent<MouseLook>().enabled = true;
		if (GameObject.Find("Player").GetComponent<CharacterMotor>()) {
			GameObject.Find("Player").GetComponent<CharacterMotor>().enabled = true;
			GameObject.Find("Player").GetComponent<MouseLook>().enabled = true;
			
		}
	}

	IEnumerator DoAnimation()
	{
		mainCamera.transform.parent.GetComponent<Animation>().Play ("SitDown");
		yield return new WaitForSeconds(1.5f);
		mainCamera.transform.parent.gameObject.transform.parent = null;
		carryingLaptop = true;
	}
	
}
