using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCountController : MonoBehaviour
{
    public TextMeshProUGUI coinsCounter;

    private int coinsCount = 0;

    private void Awake()
    {
        GlobalEventManager.OnCoinAdd.AddListener(UpdateCoinCount);
    }

    void UpdateCoinCount(int coins)
    {
        coinsCount += coins;
        coinsCounter.text = coinsCount.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
