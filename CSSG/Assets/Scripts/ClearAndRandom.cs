using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers.DialogueSystem;

public class ClearAndRandom : MonoBehaviour {
	public List<GameObject> list;
	public float[] x;
	public float[] y;
	public float[] z;
	public Quaternion[] Rx;
	private bool end;
	private bool ClearEnd;

	// Use this for initialization
	void Start () {

		list = GameObject.FindGameObjectsWithTag("PickupItems").ToList();
		int i = 0;
		ClearEnd = false;

		x = new float[list.Count];
		y = new float[list.Count];
		z = new float[list.Count];
		Rx = new Quaternion[list.Count];
		end = true;

		for(i = 0 ; i < list.Count ; i++ ){
			x[i] =  list[i].transform.position.x;
			y[i] =  list[i].transform.position.y;
			z[i] =  list[i].transform.position.z;
			Rx[i] =  list[i].transform.rotation;
		}

		for( i = 0 ; i < list.Count ; i ++)
		{	
			GameObject temp = list[i];
			int random = Random.Range(i,list.Count);
			list[i] = list[random];
			list[random] = temp;
		}



		for(i = 0 ; i < list.Count() ; i++ ){
			list[i].transform.position = new Vector3(x[i],y[i],z[i]);
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (ClearEnd)
			Clear ();
	
					}

	void SetClear(){
		ClearEnd = true;
		Debug.Log ("worked");
	}

	void Clear(){
		int i;
		int ClearRoomEnd = 0;
		
		for(i = 0 ; i < list.Count () && ClearRoomEnd == 0 ; i++)
		{
			if(list[i].transform.GetComponent<Pickupable>().TriggerCheck == false){
					ClearRoomEnd = 1;
			}
		}

		if (ClearRoomEnd == 0) {
			//Debug.Log ("end");
			for (i = 0; i < list.Count() && ClearRoomEnd == 0; i++) {
				if (!(list[i].transform.GetComponent<Pickupable> ().Check == true))
					ClearRoomEnd = 1;
			}
			if(ClearRoomEnd == 0){
				if(end){
				DialogueLua.SetVariable("Reset", true);
				DialogueManager.Instance.SendMessage("OnSequencerMessage", "end");
				list.Clear();
				end = false;
				}
			}else{
				DialogueManager.Instance.SendMessage("OnSequencerMessage", "end");
				for(i = 0 ; i < list.Count() ; i++ ){
					if(!(list[i].GetComponent<Pickupable>().Check)){
					list[i].transform.position = new Vector3(x[i],y[i],z[i]);
					list[i].transform.rotation = Rx[i];
					list[i].GetComponent<Pickupable>().TriggerCheck = false;
					list[i].GetComponent<Pickupable>().Check = false;
					}
				}
			}

		}
	}
}
