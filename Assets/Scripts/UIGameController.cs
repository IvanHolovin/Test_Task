using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIGameController : MonoBehaviour
    {
        [SerializeField] private Button _pause;
        [SerializeField] private Button _mix;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _nextLevel;
        [SerializeField] private GameObject _endLevelPopUp;
        private TextMeshProUGUI _text;
        
        
        private void Awake()
        {
            GameStateDispatcher.Instance.AddListener(ShowPopUpEndRound);
            EndRoundDispatcher.Instance.AddListener(PopUpMenu);
            _pause?.onClick.AddListener(()=> FlowController.Instance.GameStateUpdater(GameState.Restart));
            _mix?.onClick.AddListener(()=> FlowController.Instance.GameStateUpdater(GameState.Mixing));
            _nextLevel?.onClick.AddListener(()=>FlowController.Instance.GameStateUpdater(GameState.NextLevel));
            _restart?.onClick.AddListener(()=>FlowController.Instance.GameStateUpdater(GameState.Restart));
            _text = _endLevelPopUp.GetComponentInChildren<TextMeshProUGUI>();
        }


        private void ShowPopUpEndRound(GameState state)
        {
            _mix.gameObject.SetActive(state == GameState.Idle || state == GameState.AddingVegetable);
            _endLevelPopUp.gameObject.SetActive(state == GameState.EndRound);
            _nextLevel.gameObject.SetActive(state == GameState.EndRound && FlowController.Instance.RoundResult == RoundResult.Win);
            _restart.gameObject.SetActive(state == GameState.EndRound && FlowController.Instance.RoundResult == RoundResult.Lose);
            
        }

        private void PopUpMenu(float result)
        {
            if (result > 99.1f)
                result = 100;
            int intResult = (int) result;
            string stringResult = intResult.ToString();
            _text.SetText(stringResult + "%");
        }
    }
}