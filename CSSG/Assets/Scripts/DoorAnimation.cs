using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
	private Animator anim;
    public bool open;
    public GameObject door;

    public AudioClip clip;
    private AudioSource source;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
	}

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
        if (open)
        {
            OpenUp();
        }
	}

    /// <summary> opens the door 
    /// TODO: This will occur after the same event that will trigger a new room to generate
    /// </summary>
	void OpenUp()
    {
		Openable p = GetComponent<Openable>();

        if (p != null)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("Open", true);

            source.time = 2.5f;
            source.Play();
        }

        open = false;
	}
}
