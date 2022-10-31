using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text coinsCounter;

    private void Start()
    {
        ItemManager.Instance.OnAddCoins += UpdateCoins;
    }

    private void UpdateCoins(int amount)
    {
        coinsCounter.text = $"x {amount}";
    }
}
