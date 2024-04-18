using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    public float moveSpeed = 25f;
    public float detectionDistance = 5f;
    public event Action OnCollectable;

    protected virtual void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    protected virtual void Update()
    {
        MoveTowardPlayer();
    }

    protected virtual void MoveTowardPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerController.transform.position);
        //Debug.Log(distance.ToString());

        if (distance <= detectionDistance)
        {
            Vector3 targetPosition = new Vector3(playerController.transform.position.x, transform.position.y, playerController.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    } 
    
    protected virtual void OnCollect()
    {
        OnCollectable?.Invoke();
    }
}
