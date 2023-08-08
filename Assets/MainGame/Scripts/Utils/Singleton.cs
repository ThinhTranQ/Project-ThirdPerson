using UnityEngine;

namespace MainGame.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T InstancePrivate { get; set; }

        public virtual void Awake()
        {
            if (InstancePrivate == null)
            {
                InstancePrivate = this as T;
                DontDestroyOnLoad(gameObject);
                Initial();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void Initial()
        {
        }
    }
}