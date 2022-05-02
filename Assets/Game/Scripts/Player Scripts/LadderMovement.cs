using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private PlayerManager _playerManager;
    [SerializeField] private Rigidbody2D _rb;
    private float _vertical;
    private float _speed = 8f;
    private bool _isLadder;
    private bool _isClimbing;
    private float _baseGravityScale;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _baseGravityScale = _rb.gravityScale;
    }
    
    void Update()
    {
        if (_playerManager.PlayerDied)
        {
            _isClimbing = false;
            return;
        }
        
        _vertical = Input.GetAxis("Vertical");

        if (_isLadder && Mathf.Abs(_vertical) > 0f)
            _isClimbing = true;
    }

    private void FixedUpdate()
    {
        if (_isClimbing)
        {
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(_rb.velocity.x, _vertical * _speed);
        }
        else
        {
            _rb.gravityScale = _baseGravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _isLadder = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;

        }
    }
}
