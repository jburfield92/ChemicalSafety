using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class SymbolTrigger : MonoBehaviour {

	private bool trigger;
	public GameObject point;

	// Use this for initialization
	void Start () {
        transform.GetComponent<Collider>().enabled = false;
        trigger = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (point.activeSelf == true) {

            Vector3 posi = Camera.main.transform.position - point.transform.position;
            point.transform.rotation = Quaternion.LookRotation(posi);
            point.transform.Rotate(0.0f, 90.0f, 0.0f);

        }
	
	}

	void OnTriggerEnter(Collider other){
		if (trigger) {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "trigger");
			point.SetActive(!point.activeSelf);
            transform.GetComponent<Collider>().enabled = false;
            trigger = false;

		}
	}

    void Point()
    {
        point.SetActive(!point.activeSelf);
    }

	void TCheck(){
        transform.GetComponent<Collider>().enabled = true;
		trigger = true;
	}
    void TOff()
    {
        transform.GetComponent<Collider>().enabled = false;
        trigger = false;
        point.SetActive(false);
    }
}
