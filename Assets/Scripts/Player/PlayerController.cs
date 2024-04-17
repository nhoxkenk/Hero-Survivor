using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    private PlayerActions playerActions;

    [Header("Sub components")]
    public PlayerMovement playerMovement;
    public PlayerAnimator playerAnimator;
    public PlayerStat playerStat;
    public PlayerCombat playerCombat;
    public List<PlayerWeapon> playerWeaponsActive;

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

        SetupCharacter();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerStat = GetComponent<PlayerStat>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        CharacterMovement();
        HandleCharacterAttacking();
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

    public override void CharacterMovement()
    {
        CalculateMovementInputSmoothing();
        playerMovement.UpdateMovementData(smoothInputMovement);
        playerAnimator.UpdateMovementAnimation(smoothInputMovement.magnitude);
    }

    public void HandleCharacterAttacking()
    {
        playerCombat.Attack(calculateNearestEnemy());
    }

    private BaseCharacterStat calculateNearestEnemy()
    {
        BaseCharacterStat[] enemyList = FindObjectsOfType<EnemyStat>();
        BaseCharacterStat nearestEnemy = null;

        if (enemyList.Length > 0)
        {
            float nearestDistance = Mathf.Infinity;

            foreach (BaseCharacterStat enemy in enemyList)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    public override void SetupCharacter()
    {
        Transform characterTransform = Instantiate(characterData.characterModel).transform;
        characterTransform.SetParent(transform, false);
    }
}
