using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerSystem : MonoBehaviour
{
    [Header("Player Movement")]
    public float speed;
    public float rotaionSpeed;
    public float gravity = 9.18f;

    private Vector3 moveDirection = Vector3.zero;

    public bool canMove = true;

    [Header("Roll")]
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    private bool isRolling = false;
    private float rollTimer = 0f;
    private Vector3 rollDirection = Vector3.zero;

    [Header("Player System")]
    public Animator anim;
    CharacterController characterController;
    //public Rigidbody rb;

    public LayerMask layerMask;

    // IK
    [Range(0f, 1f)]
    public float DistanceToGround;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); // Player Movement
    }

    public void Movement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
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
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
            characterController.Move(moveDirection * Time.deltaTime * speed);
        }
        else
        {
            speed = 3;
            characterController.Move(moveDirection * Time.deltaTime * speed);
        }

        // Roll
        if (!isRolling && Input.GetKeyDown(KeyCode.Space))
        {
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
                characterController.Move(rollDirection * rollSpeed * Time.fixedDeltaTime);
            }
            else
            {
                // End rolling
                canMove = true;
                isRolling = false;
                anim.SetBool("Roll", false);
            }
        }
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
