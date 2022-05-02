using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SoulScript : MonoBehaviour
{
    [SerializeField] private float appearTime = 1.0f;
    [Range(0f, 1f)] [SerializeField] private float flickerIntensity = 0.5f;
    [SerializeField] private Light2D light;
    
    void Start()
    {
        light.intensity = 0;
        var color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;


    }
    
    void Update()
    {
        while (appearTime > 0)
        {
            appearTime -= Time.deltaTime;
            return;
        }
        var color = GetComponent<SpriteRenderer>().color;
        color.a = 255;
        GetComponent<SpriteRenderer>().color = color;
        
        light.intensity = flickerIntensity + Mathf.PingPong(Time.time/1.85f, 1-flickerIntensity); //1.85 to match walk animation

    }
}
