using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private Vector3 cameraOffset;
    [Range(0f, 1f)]
    public float smoothness = 0.5f;

    private void Start()
    {
        cameraOffset = transform.position - targetToFollow.position;
    }

    private void LateUpdate()
    {
        Vector3 position = targetToFollow.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, position, smoothness);
    }
}
