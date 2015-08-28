using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    private GameObject[] itemsList = new GameObject[4];
    private GameObject storedItems;
    private bool[] spotFull = new bool[4];
    private bool over;
    private bool overA;
    private int itembarIndex;

    public GameObject main;
    public GameObject blank;
    public GameObject pickup;

    public Image itemImageOne;
    public Image itemImageTwo;
    public Image itemImageThree;

    public static bool canRun;

    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
        storedItems = (GameObject)Instantiate(Resources.Load("itembar/empty"));

        for (int i = 0; i < 4; i++)
        {
            spotFull[i] = false;
            itemsList[i] = null;
        }

        itembarIndex = 0;

        canRun = true;
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
            if ((itembarIndex) > 0)
            {
                itembarIndex--;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (itemsList[i] == null)
                itemsList[i] = (GameObject)Resources.Load("itembar/empty");

        }

        GameObject itemOne = itemsList[itembarIndex];
        GameObject itemTwo = itemsList[itembarIndex + 1];
        GameObject itemThree = itemsList[itembarIndex + 2];

        itemImageOne.overrideSprite = GetSprite(itemOne);
        itemImageTwo.overrideSprite = GetSprite(itemTwo);
        itemImageThree.overrideSprite = GetSprite(itemThree);
    }

    /// <summary> Gets the image for the items in the inventory
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
	private Sprite GetSprite(GameObject o)
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
	public void Add(GameObject adding)
    {
        int i = 0;
        bool outA = true;

        while (i < 4 && outA != false)
        {
            if (spotFull[i] == false)
            {
                adding.transform.SetParent(storedItems.transform);
                itemsList[i] = adding;
                outA = false;
                spotFull[i] = true;
                overA = false;
                PickupObject.carriedObject = null;
            }
            else
            {
                i++;
                if (i == 4)
                {
                    overA = true;
                }
            }
        }
    }

    /// <summary> Handles removing the item from the inventory
    /// </summary>
	public void Remove()
    {
        if (Input.GetKeyUp(Key.one) && spotFull[itembarIndex])
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
    }

    /// <summary> Loads the object in front of the user 
    /// </summary>
    /// <param name="itemIndex"></param>
    void CarryObject(int itemIndex)
    {
        string saveName = itemsList[itemIndex].name;

        if (PickupObject.carriedObject != null)
        {
            GameObject temp = PickupObject.carriedObject;
            temp.transform.SetParent(storedItems.transform);
            temp.transform.position = storedItems.transform.position;
            PickupObject.carriedObject = (GameObject)Instantiate(itemsList[itemIndex], main.transform.position + main.transform.forward * 1.5f, Quaternion.identity);
            Destroy(itemsList[itemIndex]);
            PickupObject.carriedObject.name = saveName;
            itemsList[itemIndex] = temp;
            spotFull[itemIndex] = true;
        }
        else
        {
            PickupObject.carriedObject = (GameObject)Instantiate(itemsList[itemIndex], main.transform.position + main.transform.forward * 1.5f, Quaternion.identity);
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

        Ray ray = main.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Pickupable pick = hit.collider.GetComponent<Pickupable>();
            if (pick != null)
            {
                over = true;
                pickup = pick.gameObject;
            }
            else
            {
                over = false;
            }
        }
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
        if (canRun)
        {
            if (Input.GetKeyUp(Key.q))
            {
                Collect();
                if (over == true)
                {
                    Add(pickup);
                }
            }

            if (Input.GetKeyUp(Key.one) || Input.GetKeyUp(Key.two) || Input.GetKeyUp(Key.three))
            {
                Remove();
            }
        }
    }
}
