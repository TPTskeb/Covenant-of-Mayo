using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int num0fHearts;

    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite emptyHeart;

    void Update(){
        for (int i = 0; i < hearts.Length;){
         if(i < num0fHearts){
            hearts[i].enabled = true;
         }else {
            hearts[i].enabled = false;
         }

         }

        }
    }

