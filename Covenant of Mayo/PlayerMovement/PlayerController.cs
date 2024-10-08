using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb2D;
    public TextMeshProUGUI collectedText;
    public static int collectedAmount = 0;

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

        float shootHor = UnityEngine.Input.GetAxis("ShootHorizontal");
        float shootVer = UnityEngine.Input.GetAxis("ShootVertical");
        if ((shootHor != 0 || shootVer != 0) && Time.time > LastFire + FireDelay)
        {
            Shoot(shootHor,shootVer);
            LastFire = Time.time;
        }

        // Calculate movement vector and set velocity
        Vector2 movement = new Vector2(horizontal, vertical) * speed;
        rb2D.velocity = movement;
        collectedText.text = "items Collected: " + collectedAmount;
    }

    //Shooting Bullets
   void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0;
        Vector2 bulletVelocity = new Vector2(x , y).normalized * bulletSpeed;

        // Set the bullet's velocity    
        bulletRb.velocity = bulletVelocity;
    }
}