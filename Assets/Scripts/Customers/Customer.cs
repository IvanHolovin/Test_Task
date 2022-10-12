using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public enum CustomerType
    {
        OldMan,
        Woman,
        Man
    }
    public class Customer : MonoBehaviour
    {
        [SerializeField] private Order _order;
        [SerializeField] private CustomerType _customerType;
        private Animator _animator;
        private CustomerSpawnSide _wasSpawned;

        public CustomerType CustomerType() => _customerType;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void MoveToOrderPlace(CustomerSpawnSide side)
        {
            
            if (side == CustomerSpawnSide.Left)
            {
                _wasSpawned = CustomerSpawnSide.Left;
                transform.DORotate(new Vector3(0, 125, 0), 0.1f);
                transform.DOMove(new Vector3(-0.29f, 0, 1.2f), 2f).SetEase(Ease.Linear).OnComplete(() =>
                    transform.DORotate(new Vector3(0, 180, 0), 0.1f)
                        .OnComplete(() => SetActiveOrder(true)));
            }
            else
            {
                _wasSpawned = CustomerSpawnSide.Right;
                transform.DORotate(new Vector3(0, -121, 0), 0.1f);
                transform.DOMove(new Vector3(-0.29f, 0, 1.2f), 2f).SetEase(Ease.Linear).OnComplete(() =>
                    transform.DORotate(new Vector3(0, 180, 0), 0.1f)
                        .OnComplete(() => SetActiveOrder(true)));
            }
        }

        public void MoveBack()
        {
            SetActiveOrder(false);
            if (_wasSpawned == CustomerSpawnSide.Left)
            {
                transform.DORotate(new Vector3(0, 60, 0), 0.1f);
                transform.DOMove(new Vector3(3f, 0, 3f), 2f).SetEase(Ease.Linear).OnComplete(()=>transform.gameObject.SetActive(false));
            }
            else
            {
                transform.DORotate(new Vector3(0, -30, 0), 0.1f);
                transform.DOMove(new Vector3(-3f, 0, 3f), 2f).SetEase(Ease.Linear).OnComplete(()=>transform.gameObject.SetActive(false));
            }
            
        }
        

        public void SetOrder(Color color)
        {
            _order.SetColorOfOrder(color);
        }

        private void SetActiveOrder(bool onPlace)
        {
            _order.gameObject.SetActive(onPlace);
            _order.StartRotate(onPlace);
            _animator.SetBool("OnPlace", onPlace);
        }
        
        
    }
}