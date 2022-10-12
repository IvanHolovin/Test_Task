using System;
using UnityEngine;

namespace DefaultNamespace.Vegitables
{
    [CreateAssetMenu(fileName = "New Factory", menuName = "Factory/VegFactory")]
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
                VegetableType.Tomato => GetInstance(_tomato),
                VegetableType.Celera => GetInstance(_celera),
                VegetableType.Orange => GetInstance(_orange),
                VegetableType.Cherry => GetInstance(_cherry),
                VegetableType.Apple => GetInstance(_apple),
                VegetableType.Eggplant => GetInstance(_eggplant),
                VegetableType.Banana => GetInstance(_banana)
            };
        }
    }
}