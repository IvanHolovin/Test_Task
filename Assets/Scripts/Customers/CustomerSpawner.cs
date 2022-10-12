using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public enum CustomerSpawnSide
    {
        Left,
        Right
    }
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private CustomerInstanceFactory _factory;
        [SerializeField] private Transform _leftSpawnPoint;
        [SerializeField] private Transform _rightSpawnPoint;

        private List<Customer> _spawnedCustomersPool = new List<Customer>();
        private Customer _currentCustomer;
        
        public void SpawnCustomer(LevelData levelData)
        {
            if(_currentCustomer != null)
                _currentCustomer.MoveBack();
            
            
            foreach (Customer customer in _spawnedCustomersPool)
            {
                if (customer.CustomerType() == levelData.CustomerType())
                {
                    _currentCustomer = customer;
                    _currentCustomer.gameObject.SetActive(true);
                    if (levelData.SpawnSide() == CustomerSpawnSide.Left)
                    {
                        _currentCustomer.transform.SetParent(_leftSpawnPoint);
                    }
                    else
                    {
                        _currentCustomer.transform.SetParent(_rightSpawnPoint);
                    }
                    _currentCustomer.transform.localPosition = Vector3.zero;
            
                    _currentCustomer.SetOrder(levelData.ColorResult());
                    _currentCustomer.MoveToOrderPlace(levelData.SpawnSide());
                    break;
                }
            }

            if ( _currentCustomer == null)
            {
                _currentCustomer = _factory.Get(levelData.CustomerType());
                _spawnedCustomersPool.Add(_currentCustomer);
            }
            else if (_currentCustomer.CustomerType() != levelData.CustomerType())
            {
                _currentCustomer = _factory.Get(levelData.CustomerType());
                _spawnedCustomersPool.Add(_currentCustomer);
            }

            if (levelData.SpawnSide() == CustomerSpawnSide.Left)
            {
                _currentCustomer.transform.SetParent(_leftSpawnPoint);
            }
            else
            {
                _currentCustomer.transform.SetParent(_rightSpawnPoint);
            }
            _currentCustomer.transform.localPosition = Vector3.zero;
            
            _currentCustomer.SetOrder(levelData.ColorResult());
            _currentCustomer.MoveToOrderPlace(levelData.SpawnSide());
        }
        
    }
}