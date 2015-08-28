using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public GameObject fireExtinguisherObject;
    public GameObject extinguisherEmission;
    private ParticleSystem ps;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
	    ps = extinguisherEmission.GetComponent<ParticleSystem>();
        ps.Stop();
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
        if (ps.isPlaying && !PickupObject.carrying)
        {
            ps.Stop();
        }

	    if (PickupObject.carriedObject != null && PickupObject.carriedObject.tag == "FireExtinguisher")
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ps.Play();
            }
            else
            {
                ps.Stop();
            }
        }
	}
}
