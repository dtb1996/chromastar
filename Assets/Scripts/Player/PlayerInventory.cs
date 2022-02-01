using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public int coins;

    public delegate void InventoryDelegate();
    public static InventoryDelegate inventoryChange;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseCoins(int amount)
    {
        coins += amount;

        inventoryChange.Invoke();
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
