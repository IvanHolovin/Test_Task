using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [SerializeField] private GameObject _liquid;
    [SerializeField] private float _shakeDuration = 5f;
    [SerializeField] private float _shakeStrength = 6f;
    [SerializeField] private float _liquidMaxValue = 0.15f;

    private Renderer _liquidRenderer;
    
    void Start()
    {
        FillMixer(_shakeDuration);
        _liquidRenderer = _liquid.GetComponent<Renderer>();
        _liquidRenderer.material.SetFloat("_Fill", 0);
    }
    
    void Update()
    {
        
    }

    private void FillMixer(float _duration)
    {
        transform.DOShakeRotation(_duration, _shakeStrength);
    }
    
    
}
