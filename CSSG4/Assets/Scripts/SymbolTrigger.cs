using PixelCrushers.DialogueSystem;
using UnityEngine;

public class SymbolTrigger : MonoBehaviour
{
    public GameObject point;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Point()
    {
        point.SetActive(!point.activeSelf);
    }

    void StartRoom()
    {
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = true;
        ((MouseLook)player.GetComponent("MouseLook")).enabled = true;
        ((MouseLook)Camera.main.GetComponent("MouseLook")).enabled = true;

        GameObject[] invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");

        foreach (GameObject go in invisibleWalls)
        {
            go.SetActive(true);
        }
    }

    void StartArrows()
    {
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = false;
        ((MouseLook)player.GetComponent("MouseLook")).enabled = false;
        ((MouseLook)Camera.main.GetComponent("MouseLook")).enabled = false;
    }
}