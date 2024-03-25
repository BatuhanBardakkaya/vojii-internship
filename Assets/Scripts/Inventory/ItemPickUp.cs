using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

namespace Inventory
{
    public class ItemPickUp : MonoBehaviour ,IInteractable
    {
        public Item Item;
        [SerializeField] private string _prompt;

        public string InteractionPrompt => _prompt;
        void Pickup()
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);

        }
        
        public bool Interact(Interactor interactor)
        {
            Pickup();
            return true;
        }
    }
}