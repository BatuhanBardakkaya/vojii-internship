using System.Collections;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

namespace Inventory
{
    public class ItemPickUp : MonoBehaviour ,IInteractable
    {
        public Item Item;
        [SerializeField] private string _prompt;

        public string InteractionPrompt => _prompt;
        IEnumerator Pickup()
        {
            
            if (InventoryManager.Instance.isInventoryOpen )
            {
                InventoryManager.Instance.ClearList();
                yield return new WaitForSeconds(0.1f);
                InventoryManager.Instance.Add(Item);
                InventoryManager.Instance.ListItems();
            }
            else
            {
                InventoryManager.Instance.Add(Item);
            }
            
            Destroy(gameObject);

        }
        
        public bool Interact(Interactor interactor)
        {
            StartCoroutine(Pickup());
            //Pickup();
            return true;
        }
    }
}