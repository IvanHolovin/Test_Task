using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        [SerializeField]
        private void Awake()
        {
            GameStateDispatcher.Instance.AddListener(ShowPopUpEndRound);
            _restart?.onClick.AddListener(()=> FlowController.Instance.GameStateUpdater(GameState.Restart));
        }


        private void ShowPopUpEndRound(GameState state)
        {
            
        }
    }
}