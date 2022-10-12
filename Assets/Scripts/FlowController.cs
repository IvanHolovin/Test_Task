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

public enum RoundResult
{
    Win,
    Lose
}
public class FlowController : MonoBehaviour
{
    [SerializeField] private Mixer _mixer;
    [SerializeField] private VegetableSpawner _vegetableSpawner;
    [SerializeField] private LevelData[] _levels;
    [SerializeField] private CustomerSpawner _customerSpawner;
    
    private int _currentLevel = 0;
    private VegetableJumper _jumper;
    private ColorCalculator _colorCalculator;
    private List<Vegetable> _addedVegetables = new List<Vegetable>();

    public static FlowController Instance{ get; private set; }
    public GameState State { get; private set; }

    public RoundResult RoundResult { get; private set; }


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
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RestartLevel(_levels[_currentLevel]);
        }
    }

    public void GameStateUpdater(GameState newState)
    {
        if(newState == GameState.Mixing && State != GameState.Idle)
            return;
        
        State = newState;

        switch (State)
        {
            case GameState.Idle:
                break;
            case GameState.AddingVegetable:
                break;
            case GameState.Restart:
                RestartLevel(_levels[_currentLevel]);
                break;
            case GameState.Mixing:
                EndRoundColorResult();
                StartMixer();
                break;
            case GameState.EndRound:
                //EndRoundColorResult();
                break;
            case GameState.NextLevel:
                NextLevel();
                break;
        }
        GameStateDispatcher.Instance.ActionHappened(State);
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
        _mixer.StartMixer();
        _mixer.SetLiquidColor(_colorCalculator.CalculateColor(_addedVegetables));
        foreach (Vegetable vegetable in _addedVegetables)
        {
            vegetable.gameObject.SetActive(false);
        }
    }

    private void SpawnLevel(LevelData level)
    {
        _vegetableSpawner.SpawnVegetables(level);
        _mixer.ResetMixer();
        _addedVegetables.Clear();
        _addedVegetables = new List<Vegetable>();
        _customerSpawner.SpawnCustomer(level);
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
        GameStateUpdater(GameState.Idle);
    }

    private void EndRoundColorResult()
    {
        float result = _colorCalculator.CalculateResult(_levels[_currentLevel], _addedVegetables);
        if (result >= 85f)
        {
            RoundResult = RoundResult.Win;
        }
        else
        {
            RoundResult = RoundResult.Lose;
        }
        EndRoundDispatcher.Instance.ActionHappened(result);
        Debug.Log(_colorCalculator.CalculateResult(_levels[_currentLevel], _addedVegetables));
    }


    private void RestartLevel(LevelData level)
    {
        _vegetableSpawner.SpawnVegetables(_levels[_currentLevel]);
        _mixer.ResetMixer();
        _addedVegetables.Clear();
        _addedVegetables = new List<Vegetable>();
        GameStateUpdater(GameState.Idle);
    }
}
