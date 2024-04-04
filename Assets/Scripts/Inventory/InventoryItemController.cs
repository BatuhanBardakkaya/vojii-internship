using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button DeleteItem;

    public void RemoveItem()
    {
        if (item.amount > 1)
        {
            
            item.amount -= 1;
            
        }
        else
        {
            
            InventoryManager.Instance.Remove(item);
            Destroy(gameObject);
        }
        
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        
    }

    public void UseItem()
    {
        
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                CoreGameSignals.OnHealthPotionUsed?.Invoke(item.value);
                break;
        }

        RemoveItem();
        CoreGameSignals.OnItemUsed?.Invoke();
        
    }
}
