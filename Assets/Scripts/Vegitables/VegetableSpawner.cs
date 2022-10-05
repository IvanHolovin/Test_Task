using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Vegitables
{
    public class VegetableSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnSpots;
        [SerializeField] private VeggiInstanceFactory _factory;
        [SerializeField] private int _objectPoolCapacity;

        public List<Vegetable> objectPool;
        
        [SerializeField] private LevelData CurrLevel;

        private void Start()
        {
            SpawnVegetables(CurrLevel);
        }

        private void SpawnVegetables(LevelData levelToSpawn)
        {
            for (int i = 0; i < Mathf.Min(_spawnSpots.Count,levelToSpawn.VegetablesToSpawn().Count) ; i++)
            {
                Vegetable vegetable = _factory.Get(levelToSpawn.VegetablesToSpawn()[i]);
                vegetable.transform.localPosition = _spawnSpots[i].position;
                vegetable.transform.SetParent(_spawnSpots[i]);
                objectPool.Add(vegetable);
                Debug.Log("vegetable");
            }
        }
    }
}