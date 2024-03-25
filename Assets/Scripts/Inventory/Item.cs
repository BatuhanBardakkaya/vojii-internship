using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item", order = 0)]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public int value;
        public Sprite icon;
        public ItemType itemType;
        
        public enum ItemType
        {
            HealthPotion
        }
        

    }
}