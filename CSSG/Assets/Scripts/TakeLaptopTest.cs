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
		//o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance - new Vector3(0, 0.75f), Time.deltaTime * smooth);
		//o.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0, 180, 0);
		//mainCamera.transform.parent.GetComponent<Animation>().Play ();

		if (Input.GetKeyDown (KeyCode.Space) && !UsingLaptop) {
			carriedLaptop.GetComponent<Animation>().Play("GrabLaptop");
			UsingLaptop = true;
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
				if (hit.collider.gameObject.tag == "IsLaptop"){
					carriedLaptop = hit.collider.gameObject;
					mainCamera.transform.parent.gameObject.transform.parent = carriedLaptop.transform;
					CanSit = false;
					StartCoroutine(DoAnimation());
					//mainCamera.transform.parent.GetComponent<Animation>().Play ("SitDown");
					//yield return new WaitForSeconds(2f);

					mainCamera.GetComponent<MouseLook>().enabled = false;
					if (GameObject.Find("Player").GetComponent<CharacterMotor>()) {
						GameObject.Find("Player").GetComponent<CharacterMotor>().enabled = false;
						GameObject.Find("Player").GetComponent<MouseLook>().enabled = false;

					}
					//mainCamera.transform.parent.gameObject.transform.parent = null;

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
		carriedLaptop.GetComponent<Animation> ().Play ("ReturnLaptop");
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
		yield return new WaitForSeconds(2f);
		mainCamera.transform.parent.gameObject.transform.parent = null;
		carryingLaptop = true;
	}
	
}
