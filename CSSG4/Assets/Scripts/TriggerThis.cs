using PixelCrushers.DialogueSystem;
using UnityEngine;

public class TriggerThis : MonoBehaviour
{
    public static bool trigger4;
    GameObject pickupObj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (trigger4 && other.gameObject.transform.name == "Player" && PickupObject.carriedObject.transform.name == "container1")
        {
            trigger4 = false;
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "dropped");
            pickupObj.GetComponent<PickupObject>().DropObject();
        }
    }

    void ActivateTrigger()
    {
        trigger4 = true;
        pickupObj = GameObject.Find("EmptyPickupObject");
        Room4SDSPickup.moved2 = false;
    }
}
