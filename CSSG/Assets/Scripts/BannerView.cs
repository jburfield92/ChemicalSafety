using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BannerView : MonoBehaviour {
	
	public GameObject Target;
	public bool hit;
	private GameObject MainCam;

	// Use this for initialization
	void Start () {

		hit = false;
		MainCam = GameObject.FindWithTag ("MainCamera");
	
	}
	
	// Update is called once per frame
	void Update () {
		//hit = false;


		if (hit) {
			Target.transform.rotation = MainCam.transform.rotation;
		}

        if (Target.activeSelf)
        {

            Target.GetComponent<Image>().enabled = hit;
            foreach (Image a in Target.GetComponentsInChildren<Image>())
            {
                a.enabled = hit;

            }
        }

		//Target.SetActive(hit);
		hit = false;
}
}
