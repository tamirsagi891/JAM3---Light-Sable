using UnityEngine;

public class GunScript : MonoBehaviour
{
    private PlayerManager _playerManager;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [Range(1,12)][SerializeField] private float bulletSpeed = 6;
    [Range(0, 10f)][SerializeField] private float fireRate = 1;
    private float _coolDown = 0;
    [SerializeField] private float offset = 0;

    private void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        bulletSpeed *= 100;
    }


    private void Update()
    {
        if (_playerManager.PlayerDied)
        {
            gameObject.SetActive(false);
            return;
        }
            
        
        if(_playerManager.CanAim)
            AimGun();

        if (_coolDown > 0)
            _coolDown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _coolDown <= 0 && _playerManager.Ammo > 0)
        {
            Shoot();
            _playerManager.Ammo -= 1;
            _coolDown = fireRate;
        }
    }

    private void AimGun()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var gunRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, gunRotation + offset);

        _spriteRenderer.flipY = Mathf.Abs(gunRotation) >= 90;
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed);
        AudioManager.PlayShotMusic();
        // play shot sound
        // play muzzle effect

    }
}