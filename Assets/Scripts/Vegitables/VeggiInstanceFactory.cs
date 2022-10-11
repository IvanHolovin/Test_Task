using System;
using UnityEngine;

namespace DefaultNamespace.Vegitables
{
    [CreateAssetMenu(fileName = "New Factory", menuName = "VegFactory")]
    public class VeggiInstanceFactory : InstanceFactory
    {
        [SerializeField] private Vegetable _tomato;
        [SerializeField] private Vegetable _celera;
        [SerializeField] private Vegetable _orange;
        [SerializeField] private Vegetable _cherry;
        [SerializeField] private Vegetable _apple;
        [SerializeField] private Vegetable _eggplant;
        [SerializeField] private Vegetable _banana;
        
        public Vegetable Get(VegetableType type)
        {
            return type switch
            {
                VegetableType.Tomato => Get(_tomato),
                VegetableType.Celera => Get(_celera),
                VegetableType.Orange => Get(_orange),
                VegetableType.Cherry => Get(_cherry),
                VegetableType.Apple => Get(_apple),
                VegetableType.Eggplant => Get(_eggplant),
                VegetableType.Banana => Get(_banana)
            };
        }
        
        private Vegetable Get(Vegetable prefab)
        {
            Vegetable instance = GetInstance(prefab);
            return instance;
        }
        
    }
}