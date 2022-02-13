using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager Instance { get; private set; }

    public int coins;

    public static event Action PlayerInventoryUpdated;

    //public delegate void InventoryDelegate();
    //public static InventoryDelegate inventoryChange;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable() => OverlapManager.OnAnyCoinCollected += IncreaseCoins;

    private void OnDisable() => OverlapManager.OnAnyCoinCollected -= IncreaseCoins;

    private void IncreaseCoins()
    {
        //coins ++;
        PlayerStats.Coins++;
        //coins = PlayerStats.Coins;
        PlayerInventoryUpdated.Invoke();

        //inventoryChange.Invoke();
    }

    public int GetCurrentCoins()
    {
        return coins;
    }

    //private int maxCoins;

    //private int currentCoins;

    //public int CurrentCoins
    //{
    //    get { return currentCoins; }
    //}

    //public void SetCoins(int coinsCollected)
    //{
    //    currentCoins += coinsCollected;
    //}
}
