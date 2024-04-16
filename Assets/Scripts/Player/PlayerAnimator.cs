using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Component References")]
    public Animator playerAnimator;

    private int playerMovementAnimationID;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        SetupAnimationIDs();
    }

    private void SetupAnimationIDs()
    {
        playerMovementAnimationID = Animator.StringToHash("Movement");
    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        playerAnimator.SetFloat(playerMovementAnimationID, movementBlendValue);
    }
}
