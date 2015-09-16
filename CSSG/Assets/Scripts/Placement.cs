using UnityEngine;
using System.Collections;
using System.Linq;

public class Placement : MonoBehaviour {
	private static GameObject mainCamera;
	private int i;
	// Use this for initialization
	void Start () {

		mainCamera = GameObject.FindWithTag ("MainCamera");

	
	}
	
	// Update is called once per frame
	void Update () {

		Place ();
		GameObject [] GhostItems = GameObject.FindGameObjectsWithTag("items");
		for(i = 0 ; i < GhostItems.Count () ; i++)
		{


		}


	
	}

	public static void Place()
	{
		if(Input.GetKeyUp(KeyCode.E))
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			
			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;


			
			if(Physics.Raycast(ray, out hit))
			{
				Placing p = hit.collider.GetComponent<Placing>();
				if(p != null && Vector3.Distance(mainCamera.transform.position, p.transform.position) < 3.0f)
				{
					p.gameObject.SetActive(!p.gameObject.activeSelf);
					Debug.Log("working");
					PickupObject.carriedObject.GetComponent<Collider>().enabled = true;
					PickupObject.carriedObject.tag = "ToDelete";
					PickupObject.carriedObject.transform.SetParent (RandomRoom.used.transform);
					PickupObject.carriedObject.transform.position = p.transform.position;
					PickupObject.carriedObject.transform.rotation = p.transform.rotation;
					PickupObject.carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
					PickupObject.carriedObject.transform.SetParent (RandomRoom.used.transform);
					PickupObject.carrying = false;
					PickupObject.carriedObject = null;


				}
			}
		}
	}


}
