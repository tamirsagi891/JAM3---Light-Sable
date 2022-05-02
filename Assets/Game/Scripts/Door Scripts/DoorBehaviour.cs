using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    #region fields

    private Animator _animator;
    private Dictionary<KeysState, int> _animationMap;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private Collider2D entranceCollider;

    #endregion

    
    #region Animation Tags
    
    private static readonly int InitialState = Animator.StringToHash("Initial State");
    private static readonly int FirstKeyUnlock = Animator.StringToHash("First Unlock");
    private static readonly int SecondKeyUnlock = Animator.StringToHash("Second Unlock");
    private static readonly int ThirdKeyUnlock = Animator.StringToHash("Third Unlock");

    #endregion
    
    #region MonoBehaviour

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animationMap = new Dictionary<KeysState, int>()
        {
            [KeysState.ZERO] = InitialState,
            [KeysState.ONE] = FirstKeyUnlock,
            [KeysState.TWO] = SecondKeyUnlock,
            [KeysState.Complete] = ThirdKeyUnlock,
            
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.CompareTag("Player") &&
          GameManager.KeysState == KeysState.Complete)
        {
            doorCollider.enabled = false;
            entranceCollider.enabled = false;
            AudioManager.PlayDoorLockOpenMusic();
            _animator.SetTrigger(_animationMap[KeysState.Complete]);
        }
    }
    

    #endregion

    #region Methods

    public void UnlockOneKey()
    {
        if (GameManager.KeysState != KeysState.Complete)
        {
            _animator.SetTrigger(_animationMap[GameManager.KeysState]);
        }
        else
        {
            entranceCollider.enabled = true;
        }
    }

    #endregion
}
