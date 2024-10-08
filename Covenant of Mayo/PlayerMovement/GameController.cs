using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static int health = 3;
    private static int maxHealth = 3;
    private static float moveSpeed = 5f;
    private static float fireRate = 1f;

    public static int Health { get => health; set => health = value; } 
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; } 
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; } 
    public static float FireRate { get => fireRate; set => fireRate = value; }
#pragma warning disable IDE0044 // Add readonly modifier
    public TMP_Text HealthText;
#pragma warning restore IDE0044 // Add readonly modifier



    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
     void Update()
    {
        HealthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            KillPlayer();
        }
    }
    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, Health + healAmount);
    }

    private static void KillPlayer()
    {
        
    }
}
