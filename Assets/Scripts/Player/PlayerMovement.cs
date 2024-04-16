using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component References")]
    public Rigidbody playerRigidbody;

    [Header("Movement Settings")]
    public float movementSpeed = 3f;
    public float turnSpeed = 0.1f;

    private Camera mainCamera;
    private Vector3 movementDirection;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    public void HandleAllMovement()
    {
        MoveThePlayer();
        RotateThePlayer();
    }

    public void UpdateMovementData(Vector3 direction)
    {
        movementDirection = direction;
    }

    private void MoveThePlayer()
    {
        Vector3 movement = CameraDirection(movementDirection) * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void RotateThePlayer()
    {
        if(movementDirection.sqrMagnitude > 0.01f)
        {
            Vector3 direction = CameraDirection(movementDirection);
            Quaternion rotation = Quaternion.Lerp(playerRigidbody.transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
            playerRigidbody.transform.rotation = rotation;
        }
    }

    private Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;
    }
}
