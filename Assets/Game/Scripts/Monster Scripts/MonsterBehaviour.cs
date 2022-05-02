using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MonsterBehaviour : MonoBehaviour
{
    #region Inspector

    [SerializeField] private Vector2 walkingRadiusRange;
    [SerializeField] private float distanceFromPlayerToAttack;
    [SerializeField] private float initialSpeed;
    [SerializeField] private GameObject soul;
    [SerializeField] private Vector3 soulPosOffset;

    #endregion

    #region Fields
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rb;
    private GameObject _lightSource;
    private float _speed;
    private float _initialXPosition;
    private float _distanceFromInitialPos;
    private float _walkingRadius;
    private int _runningDirection = -1;
    private bool _isAttacking;
    private bool _isEatingSoul;
    private bool _eatASoul;

    #endregion
    
    #region Animation Tags
    
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int EatSoul = Animator.StringToHash("EatSoul");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int RunLight = Animator.StringToHash("RunLight");
    private static readonly int AttackLight = Animator.StringToHash("AttackLight");
    private static readonly int EatSoulLight = Animator.StringToHash("EatSoulLight");
    private static readonly int DeathLight = Animator.StringToHash("DeathLight");
    #endregion

    #region MonoBehavior

    private void Awake()
    {
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _lightSource = transform.GetChild(1).gameObject;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _initialXPosition = transform.position.x;
        _walkingRadius = Random.Range(walkingRadiusRange.x, walkingRadiusRange.y);
        _lightSource.gameObject.SetActive(false);
        _speed = initialSpeed;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = _speed * Vector2.right * _runningDirection;
        _distanceFromInitialPos = math.abs(transform.position.x - _initialXPosition);
        if (_distanceFromInitialPos > _walkingRadius)
        {
            if (_runningDirection < 0)
            {
                transform.position += new Vector3(0.1f, 0, 0);
            }
            else
            {
                transform.position -= new Vector3(0.1f, 0, 0);
            }
            _runningDirection *= -1;
            _rb.velocity = _speed * Vector2.right * _runningDirection;
            _spriteRenderer.flipX = _runningDirection == 1;
        }
       
        if (!_isAttacking)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                Vector2.right * _runningDirection,
                distanceFromPlayerToAttack);
            
            if (hit.collider != null &&
                hit.transform.CompareTag("Player"))
            {
                if (!_eatASoul)
                {
                    _animator.SetTrigger(Attack);
                }
                else
                {
                    _animator.SetTrigger(AttackLight);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isEatingSoul)
        {
            if (other.gameObject.CompareTag("Player") && !_isEatingSoul)
            {
                _speed = 0;
                AudioManager.PlayMonsterAttackMusic();
                if (!_eatASoul)
                {
                    _animator.SetTrigger(EatSoul);
                }
                else
                {
                    _animator.SetTrigger(EatSoulLight); 
                }
                _isEatingSoul = true;
            }
            
            else if (other.gameObject.CompareTag("Bullet"))
            {
                _speed = 0;
                AudioManager.PlayMonsterDeath();
                if (!_eatASoul)
                {
                    _animator.SetTrigger(Death);
                }
                else
                {
                    _animator.SetTrigger(DeathLight);
                    var soulPos = transform.position + soulPosOffset;
                    Instantiate(soul, soulPos, quaternion.identity);
                }
            }
        }
    }

    #endregion


    #region Methods

    
    public void AttackIsOver()
    {
        print("attack is over");
        _isAttacking = false;
    }
    
    public void EatingSoulIsOver()
    {
        _speed = initialSpeed;
        _lightSource.SetActive( true);
        _animator.SetTrigger(RunLight);
        _isEatingSoul = false;
        _eatASoul = true;

    }

    public void DeathIdOver()
    {
        gameObject.SetActive(false);
    }
    

    #endregion
}
