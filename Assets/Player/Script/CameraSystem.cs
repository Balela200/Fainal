using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraSystem : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    public GameObject targetPlayerGameObject;

    private void Awake()
    {
        targetPlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        targetPlayer = targetPlayerGameObject.GetComponent<Transform>();

        _offset = transform.position - targetPlayer.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = targetPlayer.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
