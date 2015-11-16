using UnityEngine;

public class TriggerThis : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	void OnTriggerEnter(Collider other)
    {
		Destroy(this.gameObject);
	}
}
