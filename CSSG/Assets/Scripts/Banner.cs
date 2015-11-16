using UnityEngine;

public class Banner : MonoBehaviour
{
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
		int x = Screen.width / 2;
		int y = Screen.height / 2;
			
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(x,y));
		RaycastHit hit;

		int layerMask = 1 << 10;
		    
		if(Physics.Raycast(ray,out hit,3.0f,layerMask))
		{
			BannerView p = hit.collider.GetComponent<BannerView>();

			if(p != null && Vector3.Distance(Camera.main.transform.position, p.transform.position) < 3.0f)
			{
				p.hit = true;
			}
		}
	}
}
