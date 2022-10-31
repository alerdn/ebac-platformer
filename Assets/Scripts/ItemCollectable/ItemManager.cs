using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public Action<int> OnAddCoins;

    public SOInt coins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        OnAddCoins?.Invoke(coins.value);
    }
}
