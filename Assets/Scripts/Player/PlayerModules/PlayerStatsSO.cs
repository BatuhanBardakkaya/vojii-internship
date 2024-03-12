using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats", order = 0)]
    public class PlayerStatsSO : ScriptableObject
    {
        public PlayerStats PlayerStats;
    }
}