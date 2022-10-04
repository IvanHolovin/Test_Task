using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    
    void Start()
    {
        _liquidRenderer = _liquid.GetComponent<Renderer>();
        _liquidRenderer.material.SetFloat("_Fill", 0);
        
        StartMixer(_shakeDuration);
        TakeLidOff();
    }
    
    void Update()
    {
        
    }

    private void StartMixer(float duration)
    {
        transform.DOShakeRotation(duration, _shakeStrength);
        FillMixe(duration);
        
    }

    private async void FillMixe(float time)
    {
        float timer = 0f;
        while (timer < time/2)
        {
            timer += Time.deltaTime;
            _liquidRenderer.material.SetFloat("_Fill", _liquidMaxValue * 2*timer/time);
            await Task.Yield();
        }
    }

    private void TakeLidOff()
    {
        _cupLid.transform.DOJump(_lidOffPlace.position,0.3f,1,1f);
    }
    
}
