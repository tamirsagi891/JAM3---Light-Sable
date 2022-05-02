using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class BossAttack : MonoBehaviour
{
    private float timer;
    [SerializeField]private float lifeTime = 60f;
    [SerializeField] private float  fadeInTime = 3f;
    private Vector2 direction = Vector2.left * 2;
    private Light2D _light;

    private void Start()
    {
        _light = GetComponent<Light2D>();
        _light.intensity = 0.1f;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);

        if (fadeInTime > 0)
            fadeInTime -= Time.deltaTime;
        else
        {
            GetComponent<Collider2D>().enabled = true;
            _light.intensity = 1f;
        }

        GetComponent<Rigidbody2D>().velocity = direction;
        
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = Random.Range(1f, 3f);
            var speed = Random.Range(1, 4);
            if (Random.Range(0, 2) == 0)
                direction = Vector2.left * speed;
            else
                direction = Vector2.right * speed;
        }
    }
    
    
    
}
