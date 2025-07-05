using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack,
};

public class EnemyController : MonoBehaviour
{

    public HealthManager healthManager;
    GameObject Player;
    public EnemyState currentState = EnemyState.Wander;
    public float range;
    public float speed;
    public float attackRange;

    private bool ChooseDir = false;

    public Vector3 RandomDirection { get; private set; }

    private Vector3  RandomDir;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case(EnemyState.Wander):
                Wander();
            break;
            case(EnemyState.Follow):
                Follow();
            break;
            case(EnemyState.Die):
            break;
        }

      
    if (IsPlayerInRange(range) && currentState != EnemyState.Die)
    {
        currentState = EnemyState.Follow;
    }
    else if (!IsPlayerInRange(range) && currentState != EnemyState.Die)
    {
        currentState = EnemyState.Wander;
    }
    if(Vector3.Distance(transform.position, Player.transform.position) <= attackRange)
    {
        currentState = EnemyState.Attack;
    }
}

private bool IsPlayerInRange(float range)
{
    return Vector3.Distance(transform.position, Player.transform.position) <= range;
}
    private IEnumerator ChooseDirection()
    {
        ChooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        RandomDirection = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(RandomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        ChooseDir = false;
    }
        void Wander()
{
    if (!ChooseDir)
    {
        StartCoroutine(ChooseDirection());
    }
    transform.position += -transform.right * speed * Time.deltaTime;
    if (IsPlayerInRange(range))
    {
        currentState = EnemyState.Follow;
    }
}   

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healthManager.TakeDamage(1);
        }
    }

    public void Death()
    {
        Destroy (gameObject); 
    }
}