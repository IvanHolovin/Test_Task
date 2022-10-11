using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace.Vegitables
{
    public class VegetableSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnSpots;
        [SerializeField] private VeggiInstanceFactory _factory;
        [SerializeField] private int _objectPoolCapacity;

        private Dictionary<VegetableType, List<Vegetable>> _wholePool = new Dictionary<VegetableType, List<Vegetable>>();

        private List<Vegetable> _objectPool = new List<Vegetable>();
        
        [SerializeField] private LevelData CurrLevel;
        

        private void Start()
        {
            //SpawnVegetables(CurrLevel);
        }
        

        public void SpawnVegetables(LevelData levelToSpawn)
        {
            CurrLevel = levelToSpawn;
            for (int i = 0; i < Mathf.Min(_spawnSpots.Count,levelToSpawn.VegetablesToSpawn().Count); i++)
            {
                List<Vegetable> newList = new List<Vegetable>();
                _wholePool.Add(levelToSpawn.VegetablesToSpawn()[i], newList);
                for (int j = 0; j < _objectPoolCapacity; j++)
                {
                    Vegetable vegetable = _factory.Get(levelToSpawn.VegetablesToSpawn()[i]);
                    vegetable.transform.localPosition = _spawnSpots[i].position;
                    vegetable.transform.SetParent(_spawnSpots[i]);
                    if(j != 0)
                        vegetable.gameObject.SetActive(false);
                    newList.Add(vegetable);
                    _objectPool.Add(vegetable);
                }
            }
        }

        public void GetVegetableFromPool(VegetableType type)
        {
            List<Vegetable> list = _wholePool[type];
            if (list.Count == 0) return;
            Vegetable newVegetable = list[0];
            list.Remove(newVegetable);
            if (list.Count != 0)
                SetActiveNextVegetable(list[0]);
            //return newVegetable;
        }

        private async void SetActiveNextVegetable(Vegetable vegetable)
        {
            await Task.Delay(500);
            vegetable.gameObject.SetActive(true);
        }
        
        public void NextLevel(LevelData nextLevel)
        {
            SpawnVegetables(nextLevel);
        }

        public void RestartLevel()
        {
            foreach (var vegetable in _objectPool)
            {
                Destroy(vegetable.gameObject);
            }
            _objectPool.Clear();
            _wholePool.Clear();
            SpawnVegetables(CurrLevel);
        }
        
    }
}