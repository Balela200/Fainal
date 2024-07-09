//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class MovementEnemy : MonoBehaviour
//{
//    public static MovementEnemy movementEnemy;
//    [Header("Enemy System")]
//    Animator anim;
//    public NavMeshAgent navMeshAgent;
//    private float dist;
//    public float howClose;

//    [Header("Attack")]
//    public float timerAttack;
//    public bool canAttackEnemy = true;

//    [Header("Player")]
//    public GameObject player;
//    public Transform playerPos;
//    // Start is called before the first frame update
//    void Start()
//    {
//        movementEnemy = this;

//        navMeshAgent = GetComponent<NavMeshAgent>();
//        anim = GetComponent<Animator>();

//        player = GameObject.FindGameObjectWithTag("Player");
//        playerPos = player.GetComponent<Transform>();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        dist = Vector3.Distance(playerPos.position, transform.position);

//        if (dist <= howClose)
//        {
//            navMeshAgent.SetDestination(playerPos.position);
//            anim.SetBool("Walk", true);
//        }
//        else
//        {
//            anim.SetBool("Walk", false);
//        }


//        timerAttack += Time.deltaTime;
//        if (dist <= 1.5 && timerAttack >= 1f && canAttackEnemy == true)
//        {
//            timerAttack = 0;

//            anim.SetBool("AttackEnemy", true);
//        }
//        else
//        {
//            anim.SetBool("AttackEnemy", false);
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementEnemy : MonoBehaviour
{
    public static MovementEnemy Instance { get; private set; }

    [Header("Enemy System")]
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    Rigidbody rb;
    private float dist;
    public float howClose;
    public float gravity = 9.8f;

    [Header("Attack")]
    public float attackInterval = 1f;
    private float attackTimer;
    public bool canAttackEnemy = true;

    [Header("Player")]
    public GameObject player;
    private Transform playerPos;

    private const float attackDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

        Instance = this;

        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPos = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos == null)
        {
            return;
        }

        dist = Vector3.Distance(playerPos.position, transform.position);

        if (dist <= howClose)
        {
            navMeshAgent.SetDestination(playerPos.position);
            //navMeshAgent.speed = (anim.deltaPosition / Time.deltaTime).magnitude;

            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        attackTimer += Time.deltaTime;
        if (dist <= attackDistance && attackTimer >= attackInterval && canAttackEnemy)
        {
            attackTimer = 0;
            anim.SetBool("AttackEnemy", true);
        }
        else
        {
            anim.SetBool("AttackEnemy", false);
        }

        rb.velocity += Vector3.up * -gravity * Time.deltaTime;
    }

    //private void OnAnimatorMove()
    //{
    //    navMeshAgent.speed = (anim.deltaPosition / Time.deltaTime).magnitude;
    //}
}
