using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class EyesScript : MonoBehaviour
{
    private BossManager _bossManager;
    private Animator _animator;
    private float eyesCloseTimeSpectrum = 2;
    private float _timer;
    private bool _isEyeOpen = false;
    void Start()
    {
        _bossManager = GetComponent<BossManager>();
        _animator = GetComponent<Animator>();
        _timer = eyesCloseTimeSpectrum;
    }
    
    void Update()
    {
        
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
        {
            _timer = Random.Range(0.1f, eyesCloseTimeSpectrum);
            _isEyeOpen = !_isEyeOpen;
            _animator.SetBool("EyeOpen", _isEyeOpen);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && _isEyeOpen)
        {
            Destroy(this.gameObject);
        }
    }
    
}
