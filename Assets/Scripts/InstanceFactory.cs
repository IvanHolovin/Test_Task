using UnityEngine;

namespace DefaultNamespace
{
    public class InstanceFactory : ScriptableObject
    {
        protected T GetInstance<T>(T prefab) where T : MonoBehaviour
            {
                T instance = Instantiate(prefab);
                return instance;
            }
    }
}