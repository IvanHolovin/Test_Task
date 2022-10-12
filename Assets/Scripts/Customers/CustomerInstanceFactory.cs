using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New Factory", menuName = "Factory/Customer Factory")]

    public class CustomerInstanceFactory : InstanceFactory
    {
        [SerializeField] private Customer _oldMan;
        [SerializeField] private Customer _woman;
        [SerializeField] private Customer _man;
        
        
        
        public Customer Get(CustomerType type)
        {
            return type switch
            {
                CustomerType.OldMan => GetInstance(_oldMan),
                CustomerType.Woman => GetInstance(_woman),
                CustomerType.Man => GetInstance(_man),
            };
        }
    }
}