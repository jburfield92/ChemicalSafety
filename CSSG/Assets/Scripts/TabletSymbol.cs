using UnityEngine;
using System.Collections;

public class TabletSymbol : MonoBehaviour {


	public GameObject mainCamera;
	private Vector3 x;
	private Quaternion Rx;
	public static bool Run;
	public GameObject TempArm;

	public static GameObject leftArm;
	public static GameObject rightArm;

	// Use this for initialization
	void Start () {

		mainCamera = GameObject.FindWithTag ("MainCamera");
		leftArm = GameObject.FindGameObjectWithTag ("LeftArm");
		rightArm = GameObject.FindGameObjectWithTag ("RightArm");
		x = transform.position;
		Rx = transform.rotation;
		Run = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Run)
		if (Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			
			Ray ray = mainCamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;
			
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.tag == "IsTablet") {
					
					PauseMenu.itembar.SetActive (!PauseMenu.itembar.activeSelf);
					leftArm.SetActive (!leftArm.activeSelf);
					rightArm.SetActive (!rightArm.activeSelf);
					PickupObject.canRun = !PickupObject.canRun;
					if(Run){
						Pullin();
						TempArm.SetActive (!TempArm.activeSelf);
						Run = false;
					}

				}
			}


	
		}
	}

	void Pullin(){

		//Vector3 pos = transform.position - transform.position;
		transform.position = mainCamera.transform.position + (mainCamera.transform.forward * 0.7f) ;
		Vector3 posi = mainCamera.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation (posi);
		transform.Rotate (90.0f,180.0f,0.0f);
		//Tablet.transform.rotation = mainCamera.transform.rotation;
		//Tablet.transform.Rotate (mainCamera.transform.rotation.x,0,0);
		//Tablet.transform.Rotate (new Vector3(0,Tablet.transform.position.y,0),180.0f);

	}

	public void Putback(){
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
