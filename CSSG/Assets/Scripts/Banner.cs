using UnityEngine;
using System.Collections;

public class Banner : MonoBehaviour {
	
	private GameObject MainCam;

	// Use this for initialization
	void Start () {

		MainCam = GameObject.FindWithTag ("MainCamera");
	
	}
	
	// Update is called once per frame
	void Update () {


			int x = Screen.width / 2;
			int y = Screen.height / 2;
			
			Ray ray = MainCam.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			
			
		int layerMask = 1 << 10;
		//layerMask = ~layerMask;
		    
		if(Physics.Raycast(ray,out hit,3.0f,layerMask))
			{
				BannerView p = hit.collider.GetComponent<BannerView>();
				if(p != null && Vector3.Distance(MainCam.transform.position, p.transform.position) < 3.0f)
				{
				p.hit = true;
				}
			}
	}
}
