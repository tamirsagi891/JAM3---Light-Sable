using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private void Start()
    {
        AudioManager.PlayEndingAndCreditsMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            SceneManager.LoadScene("Intro");

    }
}
