using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers.DialogueSystem;

public class ClearAndRandom : MonoBehaviour {
	public List<GameObject> list;
	public List<GameObject> list2;
	public float[] x;
	public float[] y;
	public float[] z;
	public Quaternion[] Rx;
	private bool end;
	private bool TriggerClear;
	private int ClearEnd;
	public int ClearEnding;

	// Use this for initialization
	void Start () {

		list = GameObject.FindGameObjectsWithTag("PickupItems").ToList();
		list2 = GameObject.FindGameObjectsWithTag("items").ToList();
		int i = 0;
		ClearEnd = 0;

		x = new float[list.Count];
		y = new float[list.Count];
		z = new float[list.Count];
		Rx = new Quaternion[list.Count];
		end = true;
		TriggerClear = false;

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
	

			Clear ();
	
					}

	void SetClear(){

		TriggerClear = true;

	}

	
	void NewRoom(){
		
		list = GameObject.FindGameObjectsWithTag("PickupItems").ToList();
		list2 = GameObject.FindGameObjectsWithTag("items").ToList();
		
	}

	void DestroyAll(){
		int i;
		GameObject[] delete = list.ToArray ();
		for(i = 0 ; i < delete.Count() ; i++ ){
			list.Remove(delete[i]);
			Destroy(delete[i]);

		}
		
	}

	void Clear(){
		int i;
		//int ClearRoomEnd = 0;
		for(i = 0 ; i < list.Count() ; i++ ){
			if((list[i].GetComponent<Pickupable>().Check)){
				//Debug.Log("test");
				GameObject Temp =  list[i];
				list.Remove(list[i]);
				Temp.GetComponent<Pickupable>().enabled = false;
				ClearEnd++;
				Debug.Log(ClearEnd);
			}
		}

		if(TriggerClear)
			if(ClearEnd == ClearEnding){
			GameObject[] delete = list.ToArray ();
		for(i = 0 ; i < delete.Count() ; i++ ){
			list.Remove(delete[i]);
			Destroy(delete[i]);
			}

		
				if(end){
				DialogueLua.SetVariable("Reset", true);
				DialogueManager.Instance.SendMessage("OnSequencerMessage", "end");
				list.Clear();
				end = false;
				}

		}}}


