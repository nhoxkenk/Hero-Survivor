using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerkManager : Singleton<PerkManager>
{
    List<Perk> perkPool = new List<Perk>();
    List<Perk> perkCurrent = new List<Perk>();
    Perk[] unweightedPerks;

    List<PerkCard> offeredPerkCard = new List<PerkCard>();

    [SerializeField] private PerkCard cardTemplate;

    [SerializeField] private GameObject perkMenu;

    private void Start()
    {
        unweightedPerks = GetComponents<Perk>();
        cardTemplate.gameObject.SetActive(false);
        UIManager.Instance.ExperienceBarFull += GenerateSelectablePerks;
    }

    private void GenerateSelectablePerks()
    {
        List<Perk> selectedPerks = new List<Perk>();

        for (int i = 0; i < 3; i++)
        {
            List<Perk> possiblePerks = new List<Perk>();
            //create weighted list of perks
            foreach (Perk p in unweightedPerks)
            {
                bool isValidPerk = true;

                foreach (Perk pp in p.prerequisitePerks)
                {
                    if (!perkCurrent.Contains(pp))
                    {
                        isValidPerk = false;
                    }
                }

                if (p.maxRepeatsOfPerk <= 0 || selectedPerks.Contains(p))
                {
                    isValidPerk = false;
                }

                //check if the player health if full or not ?
                //int playerHealth = FindObjectOfType<PlayerManager>().returnCurrentHealth();

                //if (p.perkName.Equals("Heal") && playerHealth == 5)
                //{
                //    isValidPerk = false;
                //}

                if (isValidPerk)
                {
                    for (int j = 0; j < p.weight; j++)
                    {
                        possiblePerks.Add(p);

                    }
                }
            }
            //Debug.Log(possiblePerks.Count);
            selectedPerks.Add(possiblePerks[Random.Range(0, possiblePerks.Count)]);
        }
        foreach (Perk p in selectedPerks)
        {
            //Debug.Log(p.title.ToString());
            PerkCard pc = Instantiate(cardTemplate, cardTemplate.transform.parent);
            pc.transform.SetParent(perkMenu.transform, false);
            pc.SetUpCard(p);
            pc.perkButton.onClick.AddListener(() => OnCompletePerkSelection(p));
            pc.gameObject.SetActive(true);
            offeredPerkCard.Add(pc);
        }

        perkMenu.gameObject.SetActive(true);
    }

    public void OnCompletePerkSelection(Perk p)
    {
        p.maxRepeatsOfPerk -= 1;

        Time.timeScale = 1.0f;

        perkCurrent.Add(p);

        foreach (PerkCard pp in offeredPerkCard)
        {
            Destroy(pp.gameObject);
        }

        offeredPerkCard.Clear();

        perkMenu.gameObject.SetActive(false);
    }

}
