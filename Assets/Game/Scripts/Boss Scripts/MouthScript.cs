using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class MouthScript : MonoBehaviour
{
    private Animator _animator;
    private float mouthCloseTimeSpectrum = 2;
    private float _timer;
    private bool _isMouthOpen = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _timer = mouthCloseTimeSpectrum;
    }
    
    void Update()
    {
        
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
        {
            _timer = Random.Range(0.1f, mouthCloseTimeSpectrum);
            _isMouthOpen = !_isMouthOpen;
            _animator.SetBool("MouthOpen", _isMouthOpen);
        }
    }
    
}
