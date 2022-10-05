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
                VegetableType.tomato => Get(_tomato),
                VegetableType.celera => Get(_celera),
                VegetableType.orange => Get(_orange),
                VegetableType.cherry => Get(_cherry),
                VegetableType.apple => Get(_apple),
                VegetableType.eggplant => Get(_eggplant),
                VegetableType.banana => Get(_banana)
            };
        }
        
        private Vegetable Get(Vegetable prefab)
        {
            Vegetable instance = GetInstance(prefab);
            return instance;
        }
        
    }
}