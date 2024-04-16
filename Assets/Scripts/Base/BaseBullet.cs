using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public ScriptableBullet bulletData;
    [SerializeField] protected LayerMask activeEffect;
    [SerializeField] protected float radiusEffect;

    [Header("References")]
    public BaseCharacterController characterController;
}
