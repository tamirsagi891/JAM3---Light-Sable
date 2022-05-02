using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    [Range(1, 9)] [SerializeField] private int _lifePoints = 3;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject soul;
    [SerializeField] private bool canAim = true;
    [Range(1f, 3f)] [SerializeField] private float respawnTime = 1.5f;
    [Range(1f, 4f)] [SerializeField] private float respawnTimeAfterMonster = 3f;
    private float _respawnTimer;
    private bool _hasDiedByMonster;
    private Vector3 _basePos;
    private readonly Vector3 _soulPosOffset = new Vector3(-0.1f, -0.15f, 0);


    public GameObject Gun { get; set; }
    public int Ammo { get; set; }
    public bool PlayerDied { get; set; }
    public bool CanAim { get; set; }
    
    
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
        SetData();
    }

    private void SetData()
    {
        Gun = gun;
        PlayerDied = false;
        CanAim = canAim;
        _basePos = transform.position;
        _respawnTimer = respawnTime;
        Ammo = _gameManager.Ammo;
    }

    void Update()
    {
        _gameManager.Ammo = Ammo;
        
        if (PlayerDied)
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer < 0)
                Respwan();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        var item = collision.gameObject;
        if (item.CompareTag("Spikes"))
        {
            if(!PlayerDied)
                PlayerDeath();
        }
        
        if (item.CompareTag("Ammo"))
        {
            item.GetComponent<AmmoScript>().GetAmmo();
            AudioManager.PlayAmmoPickupMusic();
            Destroy(item.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss Fight Trigger"))
        {
            _gameManager.BossState = true;
            _basePos = transform.position;
            Destroy(other.gameObject);
        }
        
        else if (other.gameObject.CompareTag("Monster"))
        {
            if (!PlayerDied)
            {
                _hasDiedByMonster = true;
                _respawnTimer = respawnTimeAfterMonster;
                PlayerDeath();
            }
        }
        
    }


 
    void PlayerDeath()
    {
        PlayerDied = true;
        Gun.SetActive(false);
        AudioManager.PlayPlayerDeathMusic();
        // should no instantiate if was killed by monster
        if (!_hasDiedByMonster)
        {
            _animator.SetBool("PlayerDead", true);
            var soulPos = transform.position + _soulPosOffset;
            Instantiate(soul, soulPos, quaternion.identity);
        }
        else
        {
            _animator.SetBool("PlayerMonsterDead", true);
        }
        
    }

    private void Respwan()
    {
        _animator.SetBool("PlayerDead", false);
        _animator.SetBool("PlayerMonsterDead", false);
        _respawnTimer = respawnTime;
        transform.position = _basePos;
        _hasDiedByMonster = false;
        _gameManager.DeathCounter += 1; 
        PlayerDied = false;
        Gun.SetActive(true);
        DeductAmmo();
    }

    private void DeductAmmo()
    {
        Ammo -= 2;
        if (Ammo < 2)
            Ammo = 2;
    }
}