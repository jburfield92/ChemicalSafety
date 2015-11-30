using UnityEngine;

public class Open : MonoBehaviour
{
	public GameObject set;

    /// <summary> 
    /// </summary>
    /// <param name="on"></param>
    public void OnOff(GameObject on)
    {
        on.SetActive(!on.activeSelf);
    }
}
