using UnityEngine;
using System.Collections;

public class PutOutFire : MonoBehaviour
{
    public GameObject fire;
    public GameObject smoke;
    private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[20];
    private ParticleSystem fireParticles;
    private ParticleSystem smokeParticles;
    public static bool IsActive = true;

    /// <summary> Use this for initialization
    /// </summary>
    void Start () {
        fireParticles = fire.GetComponent<ParticleSystem>();
        smokeParticles = smoke.GetComponent<ParticleSystem>();

        fireParticles.emissionRate = 0;
        smokeParticles.emissionRate = 0;
	}

    /// <summary> Update is called once per frame
    /// </summary>
    void Update() {
        if (fireParticles.emissionRate <= 0)
        {
            IsActive = false;
        }
        else
        {
            Debug.Log("Collision");
            IsActive = true;
        }
	}

    /// <summary> Detects if the fire extinguisher emissions collide with the fire object.
    /// </summary>
    /// <param name="gameobject"></param>
    void OnParticleCollision (GameObject gameobject)
    {
        if (gameobject.tag == "extinguisherEmission")
        {
            ParticleSystem ps = gameobject.GetComponent<ParticleSystem>();

            int safeLength = ps.GetSafeCollisionEventSize();

            if (collisionEvents.Length < safeLength)
            {
                collisionEvents = new ParticleCollisionEvent[safeLength];
            }

            for (int i = 0; i < collisionEvents.Length; i++)
            {
                fireParticles.emissionRate -= 2;
                smokeParticles.emissionRate -= 2;
            }
        }
    }

    /// <summary> Starts the fire animation
    /// </summary>
    void StartFire()
    {
        fireParticles.emissionRate = 15;
        smokeParticles.emissionRate = 15;
    }
}
