using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using DefaultNamespace.Vegitables;
using DG.Tweening;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    [SerializeField] private Mixer _mixer;
    [SerializeField] private VegetableSpawner _vegetableSpawner;
    private VegetableJumper _jumper;
    //private int _i = 0;
    private List<Sequence> _queue = new List<Sequence>();
    private bool isOpenLid = false;


    void Start()
    {
        _jumper = GetComponent<VegetableJumper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _mixer.StartMixer(5f);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _queue.Add(Jump());
        }

        if (_queue.Count > 0)
        {
            PutInVegetable();
        }
        
    }

    private async void PutInVegetable()
    {
        if (!isOpenLid)
        {
            isOpenLid = true;
            await _mixer.TakeLidOff().AsyncWaitForCompletion();
        }
        
        foreach (var task in _queue)
        {
            await task.AsyncWaitForCompletion();
            _queue.Remove(task);
        }
        
        if (isOpenLid)
        {
            isOpenLid = false;
            await _mixer.TakeLidOn().AsyncWaitForCompletion();
            
        }
    }
    private Sequence Jump()
    {
        return _jumper.JumpVegetableInMixer(_vegetableSpawner.objectPool[0], _mixer.JumpOnPlace());
    }
    
}
