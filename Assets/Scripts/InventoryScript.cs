using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryScript : MonoBehaviour
{
    public static event EventHandler OnFoodAmountChanged;
    private static int FoodAmount = 0;
    
    public static void AddFoodAmount(int amount)
    {
        FoodAmount += amount;
        if (OnFoodAmountChanged != null)
        {
            OnFoodAmountChanged(null, EventArgs.Empty);
        }   
    }

    public static int GetFoodAmount()
    {
        return FoodAmount;
    }
}
