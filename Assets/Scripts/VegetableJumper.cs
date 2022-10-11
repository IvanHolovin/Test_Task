using DefaultNamespace.Vegitables;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class VegetableJumper : MonoBehaviour
    {
        
        public void JumpVegetableInMixer(Vegetable vegetable,Transform jumpLocation)
        {
            if (vegetable.VegetableType() == VegetableType.Banana)
            {
                vegetable.transform.DOScale(new Vector3(0.3f,0.3f,0.3f),0.5f).SetEase(Ease.Linear);
                vegetable.transform.DORotate(new Vector3(50f, 0f, 0f), 0.5f).SetEase(Ease.Linear);
            }
            else
            {
                vegetable.transform.DOScale(new Vector3(0.5f,0.5f,0.5f),0.5f);
            }
            vegetable.GetComponent<Rigidbody>().isKinematic = false;
            vegetable.transform.DOJump(jumpLocation.position, 0.3f, 1, 1f).SetEase(Ease.Linear);
        }
    }
}