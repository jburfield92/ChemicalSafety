using UnityEngine;
using System.Collections.Generic;
using System.Linq;

<<<<<<< HEAD
public class RandomRoomsList : MonoBehaviour
{
    /// <summary> Gets the list of rooms
    /// </summary>
    /// <returns></returns>
	public static GameObject[] GetRooms()
	{
		GameObject[] rooms =  Resources.LoadAll<GameObject> ("Rooms");
		List<GameObject> list = rooms.ToList();
		
		for(int i = 0 ; i < list.Count ; i ++)
        {
=======
public class RandomRoomsList : MonoBehaviour {



	public static GameObject[] GetRooms()
	{

		GameObject[] rooms =  Resources.LoadAll<GameObject> ("Rooms");
		List<GameObject> list = rooms.ToList();
		
		for(int i = 0 ; i < list.Count ; i ++){
>>>>>>> David
			
			GameObject temp = list[i];
			int random = Random.Range(i,list.Count);
			list[i] = list[random];
			list[random] = temp;
<<<<<<< HEAD
		}

		return list.ToArray();
=======

		}
		return list.ToArray ();
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
>>>>>>> David
	}
}
