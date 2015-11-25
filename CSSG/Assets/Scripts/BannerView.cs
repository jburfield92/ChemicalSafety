using UnityEngine;

public class BannerView : MonoBehaviour
{
	public GameObject Target;
	public bool hit;
   private bool playing;
    private float time;

	// Use this for initialization
	void Start ()
    {
        if(Target == null)
        {
            Target = null;
        }
		hit = false;
        playing = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if (hit)
        {
            time += Time.deltaTime;
            if(Target != null)
            Target.transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            if (transform.GetComponent<AudioSource>() != null) {
                time = 0.0f;
                transform.GetComponent<AudioSource>().Stop();
                playing = false; }
        }
        


        if ( time >= 1.0f && playing == !true && transform.GetComponent<AudioSource>() != null)
        {
            playing = true;
            transform.GetComponent<AudioSource>().Play();   
        }
        if(Target != null)
		Target.SetActive(hit);

		hit = false;
    }
}
