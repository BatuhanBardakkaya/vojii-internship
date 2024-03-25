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
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.LogError("Yeni eklenen item null.");
            return;
        }

        item = newItem;
        
    }

    public void UseItem()
    {
       /*if (item == null)
        {
            Debug.LogError("UseItem çağrıldı fakat item null.");
            return;
        }*/

        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                CoreGameSignals.OnHealthPotionUsed?.Invoke(item.value);
                break;
        }  
        RemoveItem();
        
    }
}
