using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PerkCard : MonoBehaviour
{
    public Text perkName;
    public Text perkDescription;
    public Image perkBackground;
    public Button perkButton;

    private UnityEvent applyPerkEvent;

    private Perk myPerk;

    public void SetUpCard(Perk perk)
    {
        perkName.text = perk.perkName;
        perkDescription.text = perk.perkDescription;
        perkBackground.color = perk.cardColor;
        applyPerkEvent = perk.connectionPerk;
        myPerk = perk;
    }

    public void ExecutePerk()
    {
        applyPerkEvent?.Invoke();

    }
}
