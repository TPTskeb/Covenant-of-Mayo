using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb2D;

    public GameObject bulletPrefab;
    public float LastFire;
    public float FireDelay;
    public float bulletSpeed;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = UnityEngine.Input.GetAxis("Horizontal");
        float vertical = UnityEngine.Input.GetAxis("Vertical");

        // Movement
        Vector2 movement = new Vector2(horizontal, vertical) * speed;
        rb2D.velocity = movement;

        // Shooting with controller/keyboard
        float shootHor = UnityEngine.Input.GetAxis("ShootHorizontal");
        float shootVer = UnityEngine.Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVer != 0) && Time.time > LastFire + FireDelay)
        {
            Shoot(shootHor, shootVer);
            LastFire = Time.time;
        }

        // Shooting with mouse
        if (UnityEngine.Input.GetMouseButtonDown(0) && Time.time > LastFire + FireDelay)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;

            Shoot(shootDirection.x, shootDirection.y);
            LastFire = Time.time;
        }
    }

    // Shooting Bullets
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0;

        Vector2 bulletVelocity = new Vector2(x, y) * bulletSpeed;
        bulletRb.velocity = bulletVelocity;
    }
}