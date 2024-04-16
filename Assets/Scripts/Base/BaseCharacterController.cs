using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    [Header("Components")]
    public ScriptableCharacterData characterData;
    public BaseCharacterStat characterStat;
    public BaseCharacterShooting characterShooting;

    protected virtual void Awake()
    {
        characterStat = GetComponent<BaseCharacterStat>();
        characterShooting = GetComponent<BaseCharacterShooting>();
    }

    public abstract void SetupCharacter();
    public abstract void CharacterMovement();
}
