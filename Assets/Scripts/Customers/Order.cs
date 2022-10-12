using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class Order : MonoBehaviour
    {
        [SerializeField] private Material _material;
        private string shaderName = "_BaseColor";

        public void SetColorOfOrder(Color setColor)
        {
           _material.SetColor(shaderName, setColor);
        }

        public void StartRotate(bool rotate)
        {
            if (rotate)
            {
                transform.DORotate(new Vector3(0, 360, 0),5f).SetLoops(-1).SetEase(Ease.Linear); 
            }
            else
            {
                transform.DOKill();
            }
        }
    }
}