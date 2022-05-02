using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossManager : MonoBehaviour
{
    private GameManager _gameManager;
    private int _bossLifePoints = 10;
    private bool _isBossDead = false;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float attackSpawnTime = 3f;
    private float _attackTimer;
    [SerializeField] private float ammoSpawnTime = 5f;
     private float _ammoTimer;
    private const int BossRangeStart = 61;
    private const int BossRangeEnd = 90;
    private bool _firstApear;
    


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _attackTimer = attackSpawnTime;
        _ammoTimer = ammoSpawnTime;
    }
    
    void Update()
    {
        if (!_gameManager.BossState)
            return;

        if (_gameManager.BossState && !_firstApear)
        {
            _firstApear = true;
            AudioManager.PlayBossBattleMusic();
        }
        
        UpdateBossLife();

        if (_isBossDead)
        {
            _gameManager.EndScene();
        }

        if (_bossLifePoints == 0)
            _isBossDead = true;

        if (_attackTimer > 0)
            _attackTimer -= Time.deltaTime;
        else
        {
            var pos = new Vector3(Random.Range(BossRangeStart, BossRangeEnd),Random.Range(-3, 7), 2);
            Instantiate(attackPrefab, pos, quaternion.identity);
            _attackTimer = attackSpawnTime;
        }

        if (_ammoTimer > 0)
            _ammoTimer -= Time.deltaTime;
        else
        {
            var pos = new Vector3(Random.Range(BossRangeStart, BossRangeEnd), 12, 2);
            Instantiate(ammoPrefab, pos, quaternion.identity);
            _ammoTimer = ammoSpawnTime;
            
        }

    }

    private void UpdateBossLife()
    {
        var eyes = GameObject.FindGameObjectsWithTag("Eye");
        _bossLifePoints = eyes.Length;
        if (_bossLifePoints == 0)
            _isBossDead = true;
    }
}
