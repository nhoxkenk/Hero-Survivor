using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableCharacterData : ScriptableObject
{
    public CharacterFaction characterFaction;
    public GameObject characterModel;
    public int characterDamage;
    public int characterHealth;
    public int characterSpeed;
}
