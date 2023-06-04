using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private int level = 1;
    private float exp = 0f;

    public float addExpAmount = 35f;
    private float levelThreshold = 100f;

    public TextMeshProUGUI levelText;
    public HealthBar expBar; 
    // Start is called before the first frame update
    private void Awake()
    {
        GlobalEventManager.OnEnemyDead.AddListener(UpdateExperience);
    }

    void UpdateExperience()
    {
        if (exp + addExpAmount >= levelThreshold)
        {
            exp = addExpAmount - (levelThreshold - exp);
            level += 1;
            levelThreshold += 50; 
        }
        else
        {
            exp += addExpAmount; 
        }

        expBar.SetMaxHeath(levelThreshold);
        expBar.SetHeath(exp);
        levelText.text = level.ToString(); 
    }
}
