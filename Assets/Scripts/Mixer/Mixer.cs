using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [SerializeField] private GameObject _liquid;
    [SerializeField] private GameObject _cupLid;
    [SerializeField] private float _shakeDuration = 5f;
    [SerializeField] private float _shakeStrength = 6f;
    [SerializeField] private float _liquidMaxValue = 0.15f;
    [SerializeField] private Transform _lidOnPlace;
    [SerializeField] private Transform _lidOffPlace;

    private Renderer _liquidRenderer;
    private string _rendererFill = "_Fill";
    private string _rendererColor = "_Color";

    public Transform JumpOnPlace() => _lidOnPlace;
    
    
    void Awake()
    {
        _liquidRenderer = _liquid.GetComponent<Renderer>();
        _liquidRenderer.material.SetFloat(_rendererFill, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.DOShakeRotation(0.5f, 3f);
    }

    public void StartMixer()
    {
        FillMixer(_shakeDuration);
        transform.DOShakeRotation(_shakeDuration, _shakeStrength);
    }

    private async void FillMixer(float time)
    {
        float timer = 0f;
        while (timer < time/2)
        {
            timer += Time.deltaTime;
            _liquidRenderer.material.SetFloat(_rendererFill, _liquidMaxValue * 2*timer/time);
            await Task.Yield();
        }
        Debug.Log("changeState");
        FlowController.Instance.GameStateUpdater(GameState.EndRound);
    }
    
    public void TakeOffLid()
    {
        _cupLid.transform.DOKill();
        _cupLid.transform.DOJump(_lidOffPlace.position, 0.3f, 1, 1f)
            .OnComplete(() => _cupLid.transform.DOJump(_lidOnPlace.position, 0.3f, 1, 1f).OnComplete(() => FlowController.Instance.GameStateUpdater(GameState.Idle)));
    }

    public void ResetMixer()
    {
        _liquidRenderer.material.SetFloat(_rendererFill, 0);
        _liquidRenderer.material.SetColor(_rendererColor,new Color(0f,0f,0f,0f));
    }

    public void SetLiquidColor(Color color)
    {
        _liquidRenderer.material.SetColor(_rendererColor, color);
    }
    
}
