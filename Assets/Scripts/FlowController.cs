using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using DefaultNamespace.Assistants;
using DefaultNamespace.Vegitables;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public enum GameState
{
    Idle,
    AddingVegetable,
    Mixing,
    EndRound,
    Restart,
    NextLevel
}
public class FlowController : MonoBehaviour
{
    [SerializeField] private Mixer _mixer;
    [SerializeField] private VegetableSpawner _vegetableSpawner;
    [SerializeField] private LevelData[] _levels;
    
    private int _currentLevel = 0;
    private VegetableJumper _jumper;
    private ColorCalculator _colorCalculator;
    private List<Vegetable> _addedVegetables = new List<Vegetable>();

    public static FlowController Instance{ get; private set; }
    public GameState State { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        TouchDispatcher.Instance.AddListener(vegetable => PutVegetableInMixer(vegetable));
    }

    private void OnDestroy()
    {
        TouchDispatcher.Instance.RemoveListener(vegetable => PutVegetableInMixer(vegetable));
    }

    void Start()
    {
        _jumper = GetComponent<VegetableJumper>();
        _colorCalculator = GetComponent<ColorCalculator>();
        GameStateUpdater(GameState.Idle);
        SpawnLevel(_levels[_currentLevel]);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartMixer();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RestartLevel();
        }
    }

    public void GameStateUpdater(GameState newState)
    {
        State = newState;

        switch (State)
        {
            case GameState.Idle:
                break;
            case GameState.AddingVegetable:
                break;
            case GameState.Mixing:
                StartMixer();
                break;
            case GameState.EndRound:
                break;
            case GameState.NextLevel:
                NextLevel();
                break;
        }
        //GameStateDispatcher.Instance.ActionHappened(State);
    }

    private void PutVegetableInMixer(Vegetable vegetable)
    {
        if (_addedVegetables.Contains(vegetable))
        {
            return;
        }
        
        if (State == GameState.Idle || State == GameState.AddingVegetable)
        {
            GameStateUpdater(GameState.AddingVegetable);
            _mixer.TakeOffLid();
            _jumper.JumpVegetableInMixer(vegetable, _mixer.JumpOnPlace());
            _vegetableSpawner.GetVegetableFromPool(vegetable.VegetableType());
            _addedVegetables.Add(vegetable);
        }
    }

    private void StartMixer()
    {
        if (State == GameState.Idle)
        {
            _mixer.StartMixer();
            _mixer.SetLiquidColor(_colorCalculator.CalculateColor(_addedVegetables));
            EndRoundColorResult();
            foreach (Vegetable vegetable in _addedVegetables)
            {
                vegetable.gameObject.SetActive(false);
            }
        }
    }

    private void SpawnLevel(LevelData level)
    {
        _vegetableSpawner.SpawnVegetables(level);
        _mixer.ResetMixer();
    }

    private void NextLevel()
    {
        if (_currentLevel < _levels.Length - 1)
        {
            _currentLevel++;
            
        }
        else
        {
            _currentLevel = 0;
            
        }
        SpawnLevel(_levels[_currentLevel]);
    }

    private void EndRoundColorResult()
    {
        Debug.Log(_colorCalculator.CalculateResult(_levels[_currentLevel], _addedVegetables));
    }


    private void RestartLevel()
    {
        _vegetableSpawner.RestartLevel();
        _addedVegetables.Clear();
    }
}
