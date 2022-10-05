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
    private int _i = 0;
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
            
            PutInVegetable(Jump(_vegetableSpawner.GetVegetableFromPool(VegetableType.apple)));
            _i++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
        }
        
        
    }

    private async void PutInVegetable(Sequence task)
    {
        if (!isOpenLid)
        {
            _mixer.StopLid();
            isOpenLid = true;
            await _mixer.TakeLidOff().AsyncWaitForCompletion();
        }
        
        await task.AsyncWaitForCompletion();
        await Task.Delay(1000);
        
        if (isOpenLid)
        {
            isOpenLid = false;
            await _mixer.TakeLidOn().AsyncWaitForCompletion();
        }
    }
    private Sequence Jump(Vegetable vegetable)
    {
        return _jumper.JumpVegetableInMixer(vegetable, _mixer.JumpOnPlace());
    }
    
}
