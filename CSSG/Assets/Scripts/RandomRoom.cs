using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RandomRoom : MonoBehaviour
{
	public GameObject Nhall;
	public GameObject Shall;
	public GameObject Ehall;
	public GameObject Whall;
<<<<<<< HEAD
	public GameObject room;
	public GameObject startroom;
    public GameObject CloseDoorArea;

    public static bool clearRoom;
=======
	public GameObject finalRoom;
	public GameObject startRoom;
    public GameObject CloseDoorArea;

	static public bool clearRoom;
>>>>>>> David

    private float x;
	private float z;
	private float roomSpawnDistance;
	private char tempc;
	private char[] cor = new char[4];
	private int random;
<<<<<<< HEAD
	private GameObject newRoom;
=======
	public static GameObject newRoom;
>>>>>>> David
    private GameObject newHall;
    private GameObject deleteRoom;
	private GameObject deleteHall;
    private GameObject deleteArea;
<<<<<<< HEAD

    private GameObject[] roomsList;
    private int roomsListIndex;
    private int currentRoom;
=======
	private GameObject[] roomsList;
	private int roomsListIndex;
	private int currentRoom;
>>>>>>> David

    private float xMax;
    private float xMin;
    private float zMax;
    private float zMin;
    private bool change;
    private char[] dChange = new char[3];

<<<<<<< HEAD
=======
	public static GameObject used;

>>>>>>> David
    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
<<<<<<< HEAD
        roomsList = RandomRoomsList.GetRooms();
        roomsListIndex = 0;
        clearRoom = false;

        x = 0;
=======
		roomsList = RandomRoomsList.GetRooms();
		roomsListIndex = 0;
		clearRoom = false;
		used = (GameObject) Instantiate (Resources.Load("itembar/used"));
		x = 0;
>>>>>>> David
		z = 0;
        xMax = 0;
        xMin = 0;
        zMax = 0;
        zMin = 0;
<<<<<<< HEAD

=======
>>>>>>> David
        roomSpawnDistance = 17.6f;
        dChange[0] = 'S';
        change = true;
        cor[0] = 'E';
		cor[1] = 'S';
		cor[2] = 'W';
		cor[3] = 'N';
<<<<<<< HEAD

		newRoom = startroom;
	}
    
    /// <summary> Generates the room at random
=======
		newRoom = startRoom;
	}



    
    /// <summary> Generates the room at random
    /// TODO: This will choose from a pool of possible premade rooms instead of a blank room. 
    /// The scene will have a single room by default, the education/starting room, and then followed by series of activity rooms
    /// then folowed by a final testing room. The first and last rooms will be the same no matter the play through.
>>>>>>> David
    /// </summary>
	public void Generate()
    {
        deleteRoom = newRoom;
<<<<<<< HEAD
=======
		char RoomDirection = dChange [0];
>>>>>>> David

        if (change)
        {
            random = Random.Range(0, 3);
            change = false;
        }
        else
        {
            if ((dChange[0] == 'N' && dChange[1] == 'E') || (dChange[0] == 'S' && dChange[1] == 'W') ||
               (dChange[0] == 'E' && dChange[1] == 'S') || (dChange[0] == 'W' && dChange[1] == 'N'))
            {
                random = Random.Range(1, 3);
            }

            if ((dChange[0] == 'N' && dChange[1] == 'W') || (dChange[0] == 'S' && dChange[1] == 'E') ||
               (dChange[0] == 'E' && dChange[1] == 'N') || (dChange[0] == 'W' && dChange[1] == 'S'))
            {
                random = Random.Range(0, 2);
            }
        }

        if (random != 1)
        {
            dChange[2] = dChange[1];
            dChange[1] = dChange[0];
            dChange[0] = cor[random];
<<<<<<< HEAD
=======
			RoomDirection = dChange [0];
>>>>>>> David
        }

        switch (cor [random])
        {
		    case 'N':
			    newHall = (GameObject)Instantiate(Nhall,new Vector3(x,0,z),Quaternion.identity);
                deleteArea = (GameObject)Instantiate(CloseDoorArea, new Vector3(x, 0, z+12), Quaternion.Euler(new Vector3(90, 0, 90)));

                z = z + roomSpawnDistance;

                if (z > zMax)
                {
                    zMax = z;
                    change = true;
                }
                break;
		    case 'S':
			    newHall = (GameObject)Instantiate(Shall,new Vector3(x,0,z),Quaternion.identity);
                deleteArea = (GameObject)Instantiate(CloseDoorArea, new Vector3(x, 0, z- 12), Quaternion.Euler(new Vector3(90, 0, 90)));

<<<<<<< HEAD
                z = z - roomSpawnDistance;
=======
                z = z- roomSpawnDistance;
>>>>>>> David

                if (z < zMin)
                {
                    zMin = z;
                    change = true;

                }
                break;
		    case 'E':
			    newHall = (GameObject)Instantiate(Ehall,new Vector3(x,0,z),Quaternion.identity);
                deleteArea = (GameObject)Instantiate(CloseDoorArea, new Vector3(x+12, 0, z), Quaternion.identity);

                x = x + roomSpawnDistance;

                if (x > xMax)
                {
                    xMax = x;
                    change = true;
                }
                break;
		    case 'W':
			    newHall = (GameObject)Instantiate(Whall,new Vector3(x,0,z),Quaternion.identity);
                deleteArea = (GameObject)Instantiate(CloseDoorArea, new Vector3(x-12, 0, z), Quaternion.identity);

                x = x - roomSpawnDistance;

                if (x < xMin)
                {
                    xMin = x;

                }
                break;
		}

		if (random == 2)
        {
			tempc = cor[0];
			cor[0] = cor[1];
			cor[1] = cor[2];
			cor[2] = cor[3];
			cor[3] =  tempc;
		}

		if (random == 0)
        {
			tempc = cor[2];
			cor[2] = cor[1];
			cor[1] = cor[0];
			cor[0] = cor[3];
			cor[3] =  tempc;
		}
<<<<<<< HEAD

        if (roomsListIndex < roomsList.Count())
        {
            if (clearRoom)
            {
                currentRoom = Random.Range(0, (roomsList.Count() - roomsListIndex));
                newRoom = (GameObject)Instantiate(roomsList[currentRoom], new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                GameObject temp = roomsList[roomsList.Count() - roomsListIndex - 1];
                roomsList[roomsList.Count() - roomsListIndex - 1] = roomsList[currentRoom];
                roomsList[currentRoom] = temp;
                roomsListIndex++;
            }
            else
            {
                currentRoom = Random.Range(0, (roomsList.Count() - roomsListIndex + 1));
                newRoom = (GameObject)Instantiate(roomsList[currentRoom], new Vector3(x, 0, z), Quaternion.Euler(-90, 0, 0));
            }
        }
        else
        {
            newRoom = (GameObject)Instantiate(room, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
            roomsListIndex++;
        }


=======
		if(roomsListIndex < roomsList.Count() )
		{
			if(clearRoom)
			{
				currentRoom = Random.Range(0,(roomsList.Count()- roomsListIndex));
				newRoom = (GameObject)Instantiate(roomsList[currentRoom],new Vector3(x,0,z),Quaternion.Euler(-90,0,0));
				GameObject temp = roomsList[roomsList.Count() - roomsListIndex - 1 ];
				roomsList[roomsList.Count() - roomsListIndex - 1] = roomsList[currentRoom];
				roomsList[currentRoom] = temp;
				roomsListIndex++;
			}
			else
			{
				currentRoom = Random.Range(0,(roomsList.Count()- roomsListIndex + 1));
				newRoom = (GameObject)Instantiate(roomsList[currentRoom],new Vector3(x,0,z),Quaternion.Euler(-90,0,0));
			}

		}
		else
		{
			switch (RoomDirection)
			{
			case 'N':
				newRoom = (GameObject)Instantiate(finalRoom,new Vector3(x,0,z),Quaternion.Euler(0,180,0));
				break;
			case 'S':
				newRoom = (GameObject)Instantiate(finalRoom,new Vector3(x,0,z),Quaternion.Euler(0,0,0));
				break;
			case 'E':
				newRoom = (GameObject)Instantiate(finalRoom,new Vector3(x,0,z),Quaternion.Euler(0,270,0));
				break;
			case 'W':
				newRoom = (GameObject)Instantiate(finalRoom,new Vector3(x,0,z),Quaternion.Euler(0,-270,0));
				break;
			}
			roomsListIndex++;
		}
		//clearRoom = true;
		Items.SetItems ();
		PickupObject.carriedObject.tag = "ToDelete";
>>>>>>> David
        deleteHall = newHall;
        deleteHall.tag = "ToDelete";
        deleteRoom.tag = "ToDelete";
        deleteArea.tag = "ToDelete";
        
        GameObject[] doors = GetHallDoors();

        foreach (GameObject door in doors)
        {
            GameObject doorPart = door.transform.Find("DoorPart").gameObject;
            DoorAnimation doorAnimation = (DoorAnimation)doorPart.GetComponent(typeof(DoorAnimation));

            doorAnimation.open = true;
        }
    }
<<<<<<< HEAD
=======

>>>>>>> David
    
    /// <summary> Gets the two door GameObjects closes to the hall generated
    /// </summary>
    /// <returns></returns>
    GameObject[] GetHallDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
        List<GameObject> list = doors.ToList();

        list.Sort(ByDistance);

        // this will return the two closest doors
        return new GameObject[] { list[0], list[1] };
    }

<<<<<<< HEAD
=======



>>>>>>> David
    /// <summary> Delegate to help sort the doors.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    int ByDistance (GameObject a, GameObject b)
    {
        float distanceToA = Vector3.Distance((newHall.transform.position + deleteArea.transform.position) / 2, a.transform.position);
        float distanceToB = Vector3.Distance((newHall.transform.position + deleteArea.transform.position) / 2, b.transform.position);
        return distanceToA.CompareTo(distanceToB);
    }

    /// <summary> Update is called once per frame
<<<<<<< HEAD
    /// </summary>
    void Update ()
    {
		/*if (Input.GetKeyUp (KeyCode.L) && roomsListIndex <= roomsList.Count())
        {
            clearRoom = true;
			Generate();
		}*/
	}

    void NextRoom()
    {
        clearRoom = true;
        Generate();
    }
=======
    /// TODO: This will instead contain a bool that will generate a new room.
    /// </summary>
    void Update ()
    {
		if (Input.GetKeyUp (KeyCode.L) && roomsListIndex <= roomsList.Count())
        {

			clearRoom = true;
			PickupObject.carryBlock = true;
			Generate();
		}
	}
>>>>>>> David
}
