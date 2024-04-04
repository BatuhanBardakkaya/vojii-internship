using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        public List<Item> Items = new List<Item>();
        
        public Transform ItemContent;
        
        public GameObject InventoryItem;
                
        public GameObject Inventory;

        public Toggle EnableRemove;

        public InventoryItemController[] InventoryItems;
        
        public List<GameObject> children = new List<GameObject>();

        public bool isInventoryOpen;
        private void Awake()
        {
            Instance = this;
            
        }

        private void OnEnable()
        {
            CoreGameSignals.OnItemUsed += RRefreshInventory;
        }

        private void OnDisable()
        {
            CoreGameSignals.OnItemUsed -= RRefreshInventory;
            
            
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                isInventoryOpen = !isInventoryOpen;
                if (isInventoryOpen )
                {
                    if (Inventory != null)
                    {
                        ListItems();
                        Inventory.SetActive(true);
                    }
                    else
                    {
                        Debug.LogError("Yok.");
                    }
                }
                else
                {
                    ClearList();
                    Inventory.SetActive(false);
                }
                
            }
        }

        public void RRefreshInventory()
        {
            StartCoroutine(RefreshInventory());
        }
        IEnumerator RefreshInventory()
        {
            ClearList();
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Girmiyor");
            ListItems();
        }

        public void Add(Item item)
        {
            bool itemExists = false;
            foreach (var inventoryItem in Items)
            {
                if (inventoryItem.id == item.id) // Öğe zaten envanterde var mı?
                {
                    inventoryItem.amount += 1; // Miktarı artır
                    item.amount = inventoryItem.amount;
                    itemExists = true;
                    break;
                }
            }
    
            if (!itemExists)
            {
                Items.Add(item);
                item.amount = 1;
            }
        }

        public void Remove(Item item)
        {
            Items.Remove(item);
        }

        public void ClearList()
        {
            foreach (Transform item in ItemContent)
            {
                Destroy(item.gameObject);
            }
        }

        public void ListItems()
        {
            foreach (var item in Items)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var itemAmount = obj.transform.Find("ItemAmount").GetComponent<TMP_Text>();
                var DeleteButton = obj.transform.Find("DeleteItem").GetComponent<Button>();
                
                
                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
                itemAmount.text = item.amount.ToString();

               if (EnableRemove.isOn)
                {
                    DeleteButton.gameObject.SetActive(true);
                }
            }
            SetInventoryItems();
            Debug.Log("Remov");
        }

        public void EnableItemsRemove()
        {
            if (EnableRemove.isOn)
            {
                foreach (Transform item in ItemContent)
                {
                    item.Find("DeleteItem").gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform item in ItemContent)
                {
                    item.Find("DeleteItem").gameObject.SetActive(false);
                }
            }
            
        }

        public void SetInventoryItems()
        {
           InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

           for (int i = 0; i < Items.Count; i++)
           {
               InventoryItems[i].AddItem(Items[i]);
           }
           
        }
    }
}