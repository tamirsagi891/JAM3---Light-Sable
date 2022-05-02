using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Animator _animator;
    private CharacterController2D _controller;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (_playerManager.PlayerDied)
        {
            horizontalMove = 0;
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        FixGunPosition();
        _animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            _playerManager.Gun.SetActive(false);
            _animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        _playerManager.Gun.SetActive(true);
        _animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        _controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void FixGunPosition()
    {
        var gunPos = _playerManager.Gun.transform.localPosition;
        if (horizontalMove > 0)
            gunPos.z = -1;
        else if (horizontalMove < 0)
            gunPos.z = 1;
        _playerManager.Gun.transform.localPosition = gunPos;
    }
}
