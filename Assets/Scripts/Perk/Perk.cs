using UnityEngine;
using UnityEngine.Events;

public class Perk : MonoBehaviour
{
    public string perkName;
    public string perkDescription;
    public Color cardColor = Color.white;
    public UnityEvent connectionPerk;
    public int maxRepeatsOfPerk;
    public int MiniumLevel;
    public Perk[] prerequisitePerks;
    public int weight = 10;
}
