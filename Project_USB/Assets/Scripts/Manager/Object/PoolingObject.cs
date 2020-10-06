using UnityEngine;

namespace Pooling
{
    // 2 단계 기본적으로 가져야할 요소들을 더 풍성하게 만들어준다.
    public abstract class PoolingObject : MonoBehaviour, IPoolingInit
    { 
        public virtual string objectName { get; set; }
        public bool isActive { get; set; }
        public virtual void Init()
        {
            isActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Release()
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }
}
