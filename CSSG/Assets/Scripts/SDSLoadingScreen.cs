﻿using UnityEngine;

public class SDSLoadingScreen : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("SDS");
        }
    }
}
