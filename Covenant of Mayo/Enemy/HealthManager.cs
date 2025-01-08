using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 3; 
    public int maxHealth = 3;  

    public Image[] Hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    void Update()
    {
        UpdateHeartsUI();
    }

    void UpdateHeartsUI()
    {
        {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (health >= i + 1) // Full heart
            {
                Hearts[i].sprite = fullHeart;
            }
            else if (health > i && health < i + 1) // Half heart
            {
                Hearts[i].sprite = halfHeart;
            }
            else // Empty heart
            {
                Hearts[i].sprite = emptyHeart;
            }
        }
    }
}

    // Reduces health when the player takes damage
    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0 ) // Keep health within valid range
        {
            Destroy(gameObject);
        }
    }
}

     
