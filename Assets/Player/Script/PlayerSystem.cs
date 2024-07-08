using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSystem : MonoBehaviour
{
    public static PlayerSystem playerSystem;
    [Header("Player Movement")]
    private float vertical, horizontal;
    public float speed;
    public float rotaionSpeed;
    public float gravity = 9.18f;

    private Vector3 moveDirection = Vector3.zero;

    public bool canMove = true;
    public bool canRun = true;

    [Header("Roll")]
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    private bool isRolling = false;
    private float rollTimer = 0f;
    private Vector3 rollDirection = Vector3.zero;

    public bool canRoll = true;
    public float canRollTimer;

    [Header("Player System")]
    public Animator anim;
    CharacterController characterController;

    public LayerMask layerMask;

    [Header("Animation")]
    // IK
    [Range(0f, 1f)]
    public float DistanceToGround;

    [Header("Attack")]
    public bool canAttack = true;
    public float timerAttack;
    public GameObject AttackOneBox;

    [Header("Sword")]
    public GameObject swordHand;
    public GameObject swordBody;
    void Start()
    {
        playerSystem = this;

        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); // Player Movement
        AttackInput();

        MagicInput();
    }

    public void Movement()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if(canMove)
        {
            if (characterController.isGrounded)
            {


                moveDirection = new Vector3(horizontal, 0, vertical);
                moveDirection.Normalize();

                // Rotate Player
                if (moveDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotaionSpeed * Time.deltaTime);
                }

                // Run
                if ((vertical >= 0.1f || vertical <= -0.1f) && speed == 10)
                {
                    anim.SetBool("Run", true);
                    anim.SetBool("Walk", false);
                }
                else if ((horizontal >= 0.1f || horizontal <= -0.1f) && speed == 10)
                {
                    anim.SetBool("Run", true);
                    anim.SetBool("Walk", false);
                }

                // Walk
                else if ((vertical >= 0.1f || vertical <= -0.1f) && speed == 3)
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);
                }
                else if ((horizontal >= 0.1f || horizontal <= -0.1f) && speed == 3)
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);
                }

                // Stop Walk and Run
                else
                {
                    anim.SetBool("Run", false);
                    anim.SetBool("Walk", false);
                }
            }
        }


        moveDirection.y -= gravity * Time.deltaTime;

        // Run Walk
        if (Input.GetKey(KeyCode.LeftShift) && PlayerManger.stamina > 0 && canRun == true)
        {
            speed = 10;
            characterController.Move(moveDirection * Time.deltaTime * speed);

            PlayerManger.playerManger.Stamina();

            // UI Stamina
            //PlayerManger.playerManger.staminaGOj.SetActive(true);
            PlayerManger.playerManger.animatorStamina.SetBool("StaminaOn", true);
        }
        else
        {
            speed = 3;
            characterController.Move(moveDirection * Time.deltaTime * speed);

            // UI Stamina
            PlayerManger.playerManger.animatorStamina.SetBool("StaminaOn", false);
        }

        canRollTimer += Time.deltaTime;
        // Roll
        if (!isRolling && Input.GetKeyDown(KeyCode.Space) && canRoll == true && canRollTimer >= 1.5f)
        {
            canRollTimer = 0;
            StartRoll();
        }
    }

    void StartRoll()
    {
        isRolling = true;
        rollTimer = 0f;
        rollDirection = transform.forward; // Adjust direction as needed
    }

    void FixedUpdate()
    {
        // Roll
        if (isRolling)
        {
            rollTimer += Time.fixedDeltaTime;

            if (rollTimer < rollDuration)
            {
                // Apply roll movement
                anim.SetBool("Roll", true);
                canMove = false;
                canRun = false;
                characterController.Move(rollDirection * rollSpeed * Time.fixedDeltaTime);

                // Attack
                canAttack = false;
            }
            else
            {
                // End rolling
                canMove = true;
                canRun = true;

                isRolling = false;
                anim.SetBool("Roll", false);

                // Attack
                canAttack = true;
            }
        }
    }

    public void AttackInput()
    {
        timerAttack += Time.deltaTime;
        if(Input.GetMouseButton(0) && timerAttack >= 1.0f && canAttack == true)
        {
            // Move Player = 0
            speed = 0;
            characterController.Move(moveDirection * Time.deltaTime * speed);
            moveDirection = new Vector3(0, 0, 0);

            timerAttack = 0f;

            canMove = false;
            canRun = false;
            canRoll = false;

            anim.SetTrigger("Attack");

            StartCoroutine(AttackOne());

            //attackOneVFX.SetActive(true);

            // Sword
            swordHand.SetActive(true);
            swordBody.SetActive(false);
        }
    }

    public void MagicInput()
    {

    }


    IEnumerator AttackOne()
    {
        AttackOneBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AttackOneBox.SetActive(false);
    }

    // IK foot
    private void OnAnimatorIK(int layerIndex)
    {
        if(anim)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            RaycastHit hit;
            // Left Foot
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {
                if(hit.transform.tag == "Walk")
                {
                    Vector3 footPosition = hit.point;

                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            // Right Foot
            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {
                if (hit.transform.tag == "Walk")
                {
                    Vector3 footPosition = hit.point;

                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }
}
