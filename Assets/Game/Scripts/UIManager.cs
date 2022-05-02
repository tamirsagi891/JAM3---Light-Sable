using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI ammoCounter;
    [SerializeField] private TextMeshProUGUI deathCounter;
    [SerializeField] private List<GameObject> keys;
    private int keysOnMonitor = 0;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = _gameManager.Ammo.ToString();
        deathCounter.text = _gameManager.DeathCounter.ToString();
    }

    public void AddKey()
    {
        if (keysOnMonitor >= keys.Count) return;
        var key = keys[keysOnMonitor];
        key.SetActive(true);
        keysOnMonitor += 1;
    }
}
