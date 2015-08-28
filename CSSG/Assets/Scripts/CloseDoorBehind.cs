using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class CloseDoorBehind : MonoBehaviour
{
	private Animator anim;
    private List<GameObject> doors;
    private float timer;
    private bool isTiming;
    private bool firstEnter;
    private AudioSource source;

    public float NumberOfSecondsToWait = 1;
    public AudioClip clip;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        doors = GameObject.FindGameObjectsWithTag("door").ToList();
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
	    if (isTiming)
        {
            timer += Time.deltaTime;
        }

        if (timer > NumberOfSecondsToWait)
        {
            DestroyOldStuffAfterDoorClose(GameObject.FindGameObjectsWithTag("ToDelete"));
			Items.DestroyItems();
            isTiming = false;
        }
	}

    /// <summary> Closes the door behind the user once the user
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter (Collider other)
    {
        if (!firstEnter)
        {
            firstEnter = true;
            GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
            GameObject closestDoor = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            // closes the closest door to the area trigger
            foreach (GameObject go in doors)
            {
                Vector3 difference = (go.transform.position - position);
                float currentDistance = difference.sqrMagnitude;
                if (currentDistance < distance)
                {
                    closestDoor = go;
                    distance = currentDistance;
                }
            }

            GameObject door = closestDoor.transform.Find("DoorPart").gameObject;
            anim = door.GetComponent<Animator>();
            anim.SetBool("Close", true);
			PickupObject.carryBlock = false;
			Destroy(RandomRoom.used);
			RandomRoom.used = (GameObject) Instantiate (Resources.Load("itembar/used"));
            StartCoroutine(DelaySound());

            BeginTimer();
        }
    }

    /// <summary> Plays the closing sound after a given delay
    /// </summary>
    /// <returns></returns>
    IEnumerator DelaySound()
    {
        yield return new WaitForSeconds(0.35f);
        source.time = 2.5f;
        source.Play();
    }

    /// <summary> Destroys the old objects
    /// </summary>
    /// <param name="oldObjects"></param>
    void DestroyOldStuffAfterDoorClose(GameObject[] oldObjects)
    {
        foreach (GameObject go in oldObjects)
        {
            Destroy(go);
        }
    }

    /// <summary> Creates a delay before deleting the old objects
    /// </summary>
    void BeginTimer()
    {
        timer = 0;
        isTiming = true;
    }
}
