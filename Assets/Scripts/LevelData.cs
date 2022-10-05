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
        
        public List<VegetableType> VegetablesToSpawn() => _vegetablesToSpawn;
        public Color ColorResult() => _colorResult;
        
    }
}