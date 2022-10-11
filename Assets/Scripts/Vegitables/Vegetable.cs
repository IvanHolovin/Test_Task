using UnityEngine;

namespace DefaultNamespace.Vegitables
{
    public enum VegetableType
    {
        Tomato,
        Celera,
        Orange,
        Cherry,
        Apple,
        Eggplant,
        Banana
    }
    public class Vegetable : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private VegetableType _vegetableType;

        public Color Color() => _color;
        public VegetableType VegetableType() => _vegetableType;
        

    }
}