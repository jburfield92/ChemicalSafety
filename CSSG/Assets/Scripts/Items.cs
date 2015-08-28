using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Items : MonoBehaviour
{
	private static GameObject[] itemsList = new GameObject[6];
	private GameObject storedItems;
	private static bool[] spotFull = new bool[6];
	private bool over;
    private bool overA;
    private int itembarIndex;
	private GameObject mainCamera;

	public GameObject main;
	public GameObject blank;
	public GameObject pickup;
	
	public Image itemImageOne;
	public Image itemImageTwo;
	public Image itemImageThree;
	public Image itemImageFour;
	public Image itemImageFive;
	public Image itemImageSix;




	public static bool canRun;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {

		storedItems = (GameObject) Instantiate (Resources.Load("itembar/empty"));
		mainCamera = GameObject.FindWithTag ("MainCamera");

		for (int i = 0; i < itemsList.Count(); i++)
        {
			spotFull[i] = false;
			itemsList[i] = null;
        }
			
		itembarIndex = 0;
			
		canRun = true;
	}

	public static void SetItems(){
		
		
		for(int i = 0 ; i< itemsList.Count() ; i++){
			
			if(itemsList[i] != null){
				itemsList[i].tag = "ToDelete";
			}
			
		}
		
		
		
	}
	
	public static void DestroyItems(){
		
		for (int i = 0; i< itemsList.Count(); i++) {
			
			if(itemsList[i] != null)
			if (itemsList[i].tag == "ToDelete"){
				Destroy (itemsList [i]);
				spotFull[i] = false;
			}
			
		}
	}

    /// <summary> Draws the inventory items
    /// </summary>
	public void OnGUI()
	{
        if (Event.current.type == EventType.ScrollWheel)
        {
            if (Event.current.delta.y < 0)
            {
                if ((itembarIndex) < 1)
                {
                    itembarIndex++;
                }
            }
        }

		if (Event.current.delta.y > 0)
        {
			if((itembarIndex) > 0 )
            {
				itembarIndex--;
			}
		}

		for (int i = 0; i < itemsList.Count(); i++)
		{
			if(itemsList[i] == null)
				itemsList[i] = (GameObject) Resources.Load("itembar/empty");
			
		}


		GameObject itemOne = itemsList[itembarIndex];
		GameObject itemTwo = itemsList[itembarIndex+1];
		GameObject itemThree = itemsList[itembarIndex+2];
		GameObject itemFour = itemsList[itembarIndex+3];
		GameObject itemFive = itemsList[itembarIndex+4];
		GameObject itemSix = itemsList[itembarIndex+5];


		itemImageOne.overrideSprite = GetSprite (itemOne);
		itemImageTwo.overrideSprite = GetSprite (itemTwo);
		itemImageThree.overrideSprite = GetSprite (itemThree);
		itemImageFour.overrideSprite = GetSprite (itemFour);
		itemImageFive.overrideSprite = GetSprite (itemFive);
		itemImageSix.overrideSprite = GetSprite (itemSix);


		for (int i = 0; i < itemsList.Count(); i++)
		{
			if(itemsList[i] == (GameObject) Resources.Load("itembar/empty"))
				itemsList[i] = null;
			
		}
	}

    /// <summary> Gets the image for the items in the inventory
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
	private Sprite GetSprite(GameObject o )
	{
		GameObject originalObj = o;

        if (originalObj == null)
        {
            return null;
        }

		Image srcImage = originalObj.GetComponent<Image>();

        if (srcImage == null)
        {
            return null;
        }

		return srcImage.sprite;
	}

    /// <summary> Handles adding an item to the inventory
    /// </summary>
    /// <param name="adding"></param>
	public void Add (GameObject adding)
    {
		int i = 0;
		bool outA = true;

		while(i < itemsList.Count() && outA != false)
        {
		    if (spotFull [i] == false)
            {
				if(PickupObject.carriedObject == null){
				adding.transform.SetParent(storedItems.transform);
				itemsList[i] =  adding;
			    outA = false;
			    spotFull[i] = true;
                overA = false;
				adding.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
				else
				{
				//	Debug.Log("working");
				PickupObject.carriedObject.transform.SetParent(storedItems.transform);
				itemsList[i] =  PickupObject.carriedObject;
				PickupObject.carrying = false;
				PickupObject.carriedObject = null;
				outA = false;
				spotFull[i] = true;
				overA = false;
				
				}
            }
            else
            {
				i++;
                if (i == itemsList.Count())
                {
                    overA = true;
                }
            }
		}
	}

    /// <summary> Handles removing the item from the inventory
    /// </summary>
	public void Remove ()
    {
		if (Input.GetKeyUp (Key.one) && spotFull[itembarIndex])
        {
            CarryObject(itembarIndex);
        }

        if (Input.GetKeyUp(Key.two) && spotFull[itembarIndex + 1])
        {
            CarryObject(itembarIndex + 1);
        }

        if (Input.GetKeyUp(Key.three) && spotFull[itembarIndex + 2])
        {
            CarryObject(itembarIndex + 2);
        }
		if (Input.GetKeyUp (Key.four) && spotFull[itembarIndex +  3])
		{
			CarryObject(itembarIndex + 3);
		}
		
		if (Input.GetKeyUp(Key.five) && spotFull[itembarIndex + 4])
		{
			CarryObject(itembarIndex + 4);
		}
		
		if (Input.GetKeyUp(Key.six) && spotFull[itembarIndex + 5])
		{
			CarryObject(itembarIndex + 5);
		}
    }

    /// <summary> Loads the object in front of the user 
    /// </summary>
    /// <param name="itemIndex"></param>
    void CarryObject(int itemIndex)
    {
       // Object o = Resources.Load("itembar/" + itemsList[itemIndex]);
		string saveName = itemsList [itemIndex].name;

        if (PickupObject.carriedObject != null)
        {
            GameObject temp = PickupObject.carriedObject;
			temp.transform.SetParent(storedItems.transform);
			temp.transform.position = storedItems.transform.position;
			PickupObject.carriedObject = (GameObject)Instantiate( itemsList[itemIndex],main.transform.position + main.transform.forward * 1.5f,Quaternion.identity);
			PickupObject.carriedObject.transform.rotation = mainCamera.transform.rotation;
			PickupObject.SetArms();
			PickupObject.carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			PickupObject.carriedObject.transform.SetParent (PickupObject.leftArmTemp.transform.parent);
			PickupObject.carriedObject.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0,180,0);
			Destroy(itemsList[itemIndex]);
			PickupObject.carriedObject.name = saveName;
			itemsList[itemIndex] = temp;
            spotFull[itemIndex] = true;
        }
        else
        {
			PickupObject.carriedObject = (GameObject)Instantiate( itemsList[itemIndex],main.transform.position + main.transform.forward * 1.5f,Quaternion.identity);
			PickupObject.carriedObject.transform.rotation = mainCamera.transform.rotation;
			PickupObject.SetArms();
			PickupObject.carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			PickupObject.carriedObject.transform.SetParent (PickupObject.leftArmTemp.transform.parent);
			PickupObject.carriedObject.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0,180,0);
			Destroy(itemsList[itemIndex]);
			PickupObject.carriedObject.name = saveName;
			itemsList[itemIndex] = null;
            spotFull[itemIndex] = false;
            PickupObject.carrying = true;
        }
    }

    /// <summary> Handles putting the item in the inventory
    /// </summary>
	void Collect()
    {
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = main.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit))
        {
			Pickupable pick = hit.collider.GetComponent<Pickupable> ();
			if(pick != null)
            {
			    over = true;
			    pickup = pick.gameObject;
		    }
            else
            {
			    over = false;
			}
			if(PickupObject.carriedObject != null)
				over = true;
		}
	}

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
		if (canRun)
        {
			if (Input.GetKeyUp (Key.q))
            {
				Collect ();
				if (over == true)
                {
					Add (pickup);
				}
			}

			if (Input.GetKeyUp (Key.one) || Input.GetKeyUp (Key.two) || Input.GetKeyUp (Key.three))
            {
				Remove ();
			}
		}
	}
}
