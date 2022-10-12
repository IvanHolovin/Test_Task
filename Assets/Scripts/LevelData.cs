using System.Collections.Generic;
using DefaultNamespace.Vegitables;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New level", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<VegetableType> _vegetablesToSpawn;
        [SerializeField] private Color _colorResult;
        [SerializeField] private CustomerType _customerType;
        [SerializeField] private CustomerSpawnSide _side;
        
        public List<VegetableType> VegetablesToSpawn() => _vegetablesToSpawn;
        public Color ColorResult() => _colorResult;
        public CustomerType CustomerType() => _customerType;
        public CustomerSpawnSide SpawnSide() => _side;

    }
}