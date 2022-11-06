using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public SOInt planets;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        planets.value = 0;
        coins.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
    }

    public void AddPlanets(int amount = 1)
    {
        planets.value += amount;
    }
}
