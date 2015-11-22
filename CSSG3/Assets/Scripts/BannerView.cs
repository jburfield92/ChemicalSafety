using UnityEngine;

public class BannerView : MonoBehaviour
{
	public GameObject Target;
	public bool hit;

	// Use this for initialization
	void Start ()
    {
		hit = false;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (hit)
        {
			Target.transform.rotation = Camera.main.transform.rotation;
		}

		Target.SetActive(hit);

		hit = false;
    }
}
