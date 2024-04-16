using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableBullet : MonoBehaviour
{
    public BulletType bulletType;
    public GameObject bulletModel;
    public float bulletSpeed = 5f;
    public float bulletCurrentTimer;
    public float bulletDelayTimer;
}
