using System;
using DefaultNamespace.Vegitables;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Assistants
{
    public class TouchSelector : MonoBehaviour
    { 
        [SerializeField] private Camera _playerCamera;

        private void Update()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0 && Input.touchCount < 2)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        Target(Input.GetTouch(0).position);
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Target(Input.mousePosition);
                }
            }
        }

        private void Target(Vector3 position)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(_playerCamera.ScreenPointToRay(Input.mousePosition), out rayHit,
                int.MaxValue, ~12))
            {
                ClickDispatcher(rayHit.transform.gameObject);
            }
        }

        private void ClickDispatcher(GameObject gameObjectToCheck)
        {
            if (gameObjectToCheck.GetComponent<Vegetable>() != null)
            {
                Vegetable vegetable = gameObjectToCheck.GetComponent<Vegetable>();
                TouchDispatcher.Instance.ActionHappened(vegetable);
            }
        }
        
    }
}