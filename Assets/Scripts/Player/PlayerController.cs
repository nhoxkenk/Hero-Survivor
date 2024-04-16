using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerActions playerActions;

    [Header("Sub components")]
    public PlayerMovement playerMovement;
    public PlayerAnimator playerAnimator; 

    [Header("Input Settings")]
    public float movementSmoothingSpeed = 1f;
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private Vector3 rawInputMovement;
    [SerializeField] private Vector3 smoothInputMovement;

    private void OnEnable()
    {
        if (playerActions == null)
        {
            playerActions = new PlayerActions();
            playerActions.PlayerControls.Movement.performed += i => inputMovement = i.ReadValue<Vector2>();
        }

        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        CalculateMovementInputSmoothing();
        playerMovement.UpdateMovementData(smoothInputMovement);
        playerAnimator.UpdateMovementAnimation(smoothInputMovement.magnitude);
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }

    public void CalculateMovementInputSmoothing()
    {
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, movementSmoothingSpeed * Time.deltaTime);
    }
}
