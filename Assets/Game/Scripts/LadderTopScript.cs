using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTopScript : MonoBehaviour
{
    private PlatformEffector2D _effector;
    private Collider2D _collider;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private bool oneSidedLadder = false;
    void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();
        _collider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        if (oneSidedLadder)
            return;
        
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                _effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            _effector.rotationalOffset = 0;
        }
        

    }
    

}
