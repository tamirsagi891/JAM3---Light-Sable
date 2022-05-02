using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletLifeTime = 5;
    void Start()
    {
        
    }
    
    void Update()
    {
        bulletLifeTime -= Time.deltaTime;
        
        if (bulletLifeTime < 0)
            Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (Random.Range(0, 2) == 1)
            AudioManager.PlayObjectBreaksMusic();
       else
       {
           AudioManager.PlayObjectBreaksMusic();
       }
       
       Destroy(gameObject);
    }
}
