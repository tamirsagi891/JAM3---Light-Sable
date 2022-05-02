using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudioBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            AudioManager.PlayMonsterIdle();
    }
}
