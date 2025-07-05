using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float moveSpeed = 0.1f;
    private static float fireRate = 1f;
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; } 
    public static float FireRate { get => fireRate; set => fireRate = value; }
#pragma warning disable IDE0044 // Add readonly modifier
    
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

     }
}
