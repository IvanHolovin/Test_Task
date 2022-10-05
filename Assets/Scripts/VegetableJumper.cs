using DefaultNamespace.Vegitables;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class VegetableJumper : MonoBehaviour
    {
        
        public Sequence JumpVegetableInMixer(Vegetable vegetable,Transform jumpLocation)
        {
            return vegetable.transform.DOJump(jumpLocation.position, 0.3f, 1, 1f);
        }
    }
}