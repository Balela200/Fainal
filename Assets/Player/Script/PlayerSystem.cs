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

    [Header("Player System")]
    public Animator anim;
    CharacterController characterController;
    public Rigidbody rb;

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

            if (vertical >= 0.1f || vertical <= -0.1f)
            {
                anim.SetBool("Run", true);
            }
            else if (horizontal >= 0.1f || horizontal <= -0.1f)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime * speed);
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
