using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

    /// <summary> Update is called once per frame
    /// </summary>
	void Update ()
	{
		if (PickupObject.canRun) {
			if (axes == RotationAxes.MouseXAndY)
            {
				float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;
			
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
				transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			}
            else if (axes == RotationAxes.MouseX)
            {
				transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
			}
            else
            {
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
				transform.localEulerAngles = new Vector3 (-rotationY, transform.localEulerAngles.y, 0);
			}

			if (Input.GetKeyDown ("]") == true && sensitivityX <= 9)
            {
				sensitivityX += 1.0f;
				sensitivityY += 1.0f;
			}

			if (Input.GetKeyDown ("[") == true && sensitivityX >= 2)
            {
				sensitivityX -= 1.0f;
			}

			if (Input.GetKeyDown ("[") == true && sensitivityY >= 2)
            {
				sensitivityY -= 1.0f;
			}
		}
	}

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
	{
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
	}
}