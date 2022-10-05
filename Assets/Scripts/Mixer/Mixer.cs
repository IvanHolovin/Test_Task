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
    
    public Transform JumpOnPlace() => _lidOnPlace;
    
    void Start()
    {
        _liquidRenderer = _liquid.GetComponent<Renderer>();
        _liquidRenderer.material.SetFloat("_Fill", 0);
    }
    
    void Update()
    {
        
    }

    public void StartMixer(float duration)
    {
        FillMixer(duration);
        transform.DOShakeRotation(duration, _shakeStrength);
    }

    private async void FillMixer(float time)
    {
        float timer = 0f;
        while (timer < time/2)
        {
            timer += Time.deltaTime;
            _liquidRenderer.material.SetFloat("_Fill", _liquidMaxValue * 2*timer/time);
            await Task.Yield();
        }
    }

    public Sequence TakeLidOff()
    {
        return _cupLid.transform.DOJump(_lidOffPlace.position, 0.3f, 1, 1f);
    }

    public void StopLid()
    {
        _cupLid.transform.DOKill();
    }
    
    public Sequence TakeLidOn()
    {
        return _cupLid.transform.DOJump(_lidOnPlace.position, 0.3f, 1, 1f);
    }
    
}
