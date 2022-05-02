using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    #region Fields

    private Animator _animator;

    #endregion


    #region MonoBehaviour

    private void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.UnlockOneKey();
            AudioManager.PlayPointMusic();
            gameObject.SetActive(false);
        }
    }

    #endregion
}
