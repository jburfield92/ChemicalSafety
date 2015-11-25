using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(player.transform.position.x,transform.position.y, player.transform.position.z);
	
	}
}
