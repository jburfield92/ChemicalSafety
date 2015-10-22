using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SymbolRoom1 : MonoBehaviour {

	private bool pointOne; 
	private bool pointTwo;
	public GameObject mainCamera;
	private bool[] BannersCheck =  new bool[9];
	public GameObject[] Banners = new GameObject[9];
	public GameObject TempArm;
	public GameObject PointOneSign;
	public GameObject PointTwoSign;
	public GameObject PointTwoSign2;



	// Use this for initialization
	void Start () {
		pointOne = false;
		pointTwo = false;
		int i = 0;
		mainCamera = GameObject.FindWithTag ("MainCamera");
		for (i = 0; i < 9; i++)
			BannersCheck [i] = false;
	}
	
	// Update is called once per frame
	void Update () {
		int i;
		if (pointOne) {

			Vector3 posi = mainCamera.transform.position - PointTwoSign.transform.position;
			PointTwoSign.transform.rotation = Quaternion.LookRotation (posi);
			PointTwoSign.transform.Rotate (0.0f,90.0f,0.0f);
			posi = mainCamera.transform.position - PointTwoSign2.transform.position;
			PointTwoSign2.transform.rotation = Quaternion.LookRotation (posi);
			PointTwoSign2.transform.Rotate (0.0f,90.0f,0.0f);
			for (i = 0; i < 9; i++){
				if(Banners[i].activeSelf)
					BannersCheck[i] = true;
			}
			bool Ban = true;
			for (i = 0; i < 9 && Ban ; i++){
				if(!BannersCheck[i])
					Ban = false;
			}

			if(Ban){
				PointTwoSign2.SetActive(!PointTwoSign2.activeSelf);
				PointTwoSign.SetActive(!PointTwoSign.activeSelf);
				DialogueManager.Instance.SendMessage("OnSequencerMessage", "BannerEnd");
				pointOne = false;
			}
			
		}

		if (pointTwo) {
			Vector3 posi = mainCamera.transform.position - PointOneSign.transform.position;
			PointOneSign.transform.rotation = Quaternion.LookRotation (posi);
			PointOneSign.transform.Rotate (0.0f,90.0f,0.0f);
			if(TempArm.activeSelf){
				PointOneSign.SetActive(!PointOneSign.activeSelf);
				DialogueManager.Instance.SendMessage("OnSequencerMessage", "InfoEnd");
				pointTwo = false;
			}
			
		}

	
	}

	void PointOne(){
		pointOne = true;
		PointTwoSign2.SetActive(!PointTwoSign2.activeSelf);
		PointTwoSign.SetActive(!PointTwoSign.activeSelf);
	}
	void PointTwo(){
		pointTwo = true;
		PointOneSign.SetActive(!PointOneSign.activeSelf);
	}
}
