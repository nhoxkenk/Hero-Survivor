using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    [Header("Components")]
    public ScriptableCharacterData characterData;
    public BaseCharacterStat characterStat;

    protected virtual void Awake()
    {
        characterStat = GetComponent<BaseCharacterStat>();
    }

    public abstract void SetupCharacter();
    public abstract void CharacterMovement();
}
