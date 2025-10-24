using System.Collections.Generic;

using UnityEngine;

public class Crafting : MonoBehaviour
{
    public static Crafting Instance;

    public Dictionary<string, int> collected = new Dictionary<string, int>(); //Inventory
    public Dictionary<string, int> smore = new Dictionary<string, int> // Smore Recipe --> Dictionaries let us use quanitity :)))
    {
        { "marshmallow", 1},
        { "graham", 2},
        { "chocolate", 1}
    };

    private Dictionary<string, int> slotMap = new Dictionary<string, int>()
    {
        { "marshmallow", 0 },
        { "graham", 1 },
        { "chocolate", 2 }
    };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddFood(string food)
    {

        if (string.IsNullOrEmpty(food))
        {
            Debug.LogWarning("Oopsie in AddFood");
            return;
        }

        if(!smore.ContainsKey(food))
        {
            Debug.Log("EWWWWWWW");
            collected.Clear();

            if(GameManager.instance != null && GameManager.instance.player1Inventory != null)
            {
                GameManager.instance.player1Inventory.ClearInventory();
            }

            return;
        }

        if(collected.ContainsKey(food))
        {
            collected[food]++;
        }
        else
        {
            collected[food] = 1;
        }

        if (GameManager.instance != null && GameManager.instance.player1Inventory != null)
        {
            GameManager.instance?.player1Inventory?.AddItemByName(food);
        }

        if(smoreCheck())
        {
            Craft();
        }
    }

    public bool smoreCheck()
    {
        foreach(var ingredient in smore)
        {
            if(!collected.ContainsKey(ingredient.Key))
            {
                return false;
             }
            if(collected[ingredient.Key] < ingredient.Value)
            {
                return false;
            }
        }
        Debug.Log("Can Craft!");
        return true;
    }

    public void UsedIngredients()
    {
        foreach(var ingredient in smore)
        {
            int amount = ingredient.Value;
            int slotIndex = slotMap[ingredient.Key];

            for(int i = 0; i < amount; i++)
            {
                collected[ingredient.Key] = Mathf.Max(0, collected[ingredient.Key] - 1);
                GameManager.instance.player1Inventory.Decrease(slotIndex);
            }
        }
    }

    public void Craft()
    {
        UsedIngredients();
        if (GameManager.instance != null && GameManager.instance.player1Inventory != null)
        {
            GameManager.instance.player1Inventory.AddSmore();
        }
    }

    public void ClearCollectedAndInventory()
    {
        collected.Clear();

        if (GameManager.instance != null && GameManager.instance.player1Inventory != null)
        {
            GameManager.instance.player1Inventory.ClearInventory();
        }

        Debug.Log("Collected ingredients cleared due to fire!");
    }


}
