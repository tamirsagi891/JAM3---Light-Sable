using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoScript : MonoBehaviour
{
    private PlayerManager _playerManager;

    [SerializeField] private List<Sprite> _sprites;
    private int ammoAmount;
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        ammoAmount = Random.Range(1, 4);
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[ammoAmount - 1];
    }

    public void GetAmmo()
    {
        _playerManager.Ammo += ammoAmount;
    }
}
