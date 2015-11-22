﻿using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Placement : MonoBehaviour 
{
    public static GameObject Ghost;
    public static List<GameObject> GhostItems;

    private int i;
	// Use this for initialization
	void Start () 
	{
		Ghost = (GameObject)Instantiate(Resources.Load("used"));
	}

    public static void ClearList()
    {
        GhostItems.Clear();
    }

    // Update is called once per frame
    void Update () 
	{
		GhostItems = GameObject.FindGameObjectsWithTag("items").ToList();

		for(i = 0 ; i < GhostItems.Count () ; i++)
		{
			GhostItems[i].transform.SetParent(Ghost.transform);
		}
		
		bool test = false;

		int j = Ghost.transform.childCount;
		
		for(i = 0 ; i < j ; i++)
		{
			Placing one = Ghost.transform.GetChild(i).GetComponent<Placing>();
			
			if(PickupObject.carrying)
			{
				Pickupable two = PickupObject.carriedObject.GetComponent<Pickupable>();

				if(string.Compare(one.Name , two.Name) == 0 || string.Compare(one.Name ,"default") == 0)
				{
					test = true; 
				}
				else 
				{
					test = false;
				}
			}

			if(test)
			{
				Ghost.transform.GetChild(i).gameObject.SetActive(true);
				Ghost.transform.GetChild(i).GetComponent<Placing>().Placed = false;
			}
			else 
			{
				Ghost.transform.GetChild(i).gameObject.SetActive(false);
			}

			int l =  RandomRoom.used.transform.childCount;
			int r;

			for(r = 0; r<l ; r++)
			{
				if(Ghost.transform.GetChild(i).position != RandomRoom.used.transform.GetChild(r).position)
				{
					if(test)
					{
						Ghost.transform.GetChild(i).gameObject.SetActive(true);
					}
					else 
					{
						Ghost.transform.GetChild(i).gameObject.SetActive(false);
					}
				}
				else
				{
					Ghost.transform.GetChild(i).gameObject.SetActive(false) ;
					r=l;
				}
			}
		}
	}

	public static void Place()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			int x = Screen.width / 2;
			int y = Screen.height / 2;
			
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				Placing p = hit.collider.GetComponent<Placing>();

				if(p != null && Vector3.Distance(Camera.main.transform.position, p.transform.position) < 3.0f)
				{
                    p.gameObject.SetActive(!p.gameObject.activeSelf);
                    if (string.Compare(p.Value, PickupObject.carriedObject.GetComponent<Pickupable>().Value) == 0)
                    {
                        p.Placed = true;
                        PickupObject.carriedObject.GetComponent<Pickupable>().Check = true;
                        PickupObject.carriedObject.GetComponent<Collider>().enabled = true;
                        PickupObject.carriedObject.transform.SetParent(RandomRoom.used.transform);
                        PickupObject.carriedObject.transform.position = p.transform.position;
                        PickupObject.carriedObject.transform.rotation = p.transform.rotation;
                        PickupObject.carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        PickupObject.carriedObject.transform.SetParent(RandomRoom.used.transform);
                        PickupObject.carrying = false;
                        PickupObject.carriedObject = null;
                        GhostItems.Remove(p.gameObject);
                        Destroy(p.gameObject);
                        GameObject.Find("CorrectVoice").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        GameObject.Find("IncorrectVoice").GetComponent<AudioSource>().Play();
                    }
                }
                else
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "end");
                }
            }
        }
	}
}