using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UISlider healthUI;
    [SerializeField] private UISlider experienceUI;
    [SerializeField] private int progress = 0;
    [SerializeField] private int maxProgressValue = 10;
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;

    public event Action ExperienceBarFull;

    private void HandleHealthChanged(float arg1, float arg2)
    {
        healthUI.SetupValue(arg1);
        currentHealthText.text = arg1.ToString();
    }

    private void Start()
    {
        //Debug.Log(GameManager.Instance.playerController.playerStat.maxHealth);
        float playerHealth = GameManager.Instance.playerController.playerStat.maxHealth;
        healthUI.SetupMaxValue(playerHealth);
        currentHealthText.text = playerHealth.ToString();
        maxHealthText.text = playerHealth.ToString();
        experienceUI.SetupMaxValue(maxProgressValue);
        experienceUI.SetupValue(progress);

        GameManager.Instance.playerController.playerStat.HealthChanged += HandleHealthChanged;
    }

    public void HandleOnCollect()
    {
        if(progress == maxProgressValue)
        {
            ExperienceBarFull?.Invoke();
            IncreaseMaxProgress();
        }
        else
        {
            progress++;
            experienceUI.SetupValue(progress);
        } 
    }

    public void IncreaseMaxProgress()
    {
        maxProgressValue += 10;
        progress = 0;
        experienceUI.SetupMaxValue(maxProgressValue);
        experienceUI.SetupValue(progress);
    }
}
