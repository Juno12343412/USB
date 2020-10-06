 using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool<T> : List<T> where T : MonoBehaviour, IPoolingInit
    {
        // 이 클래스를 사용해서 PoolObject를 상속한 클래스를 풀형식으로 사용할 수 있다.
        GameObject refObj;          // 해당 T가 풀링할 오브젝트
        Transform  parent;          // 해당 T의 부모 객체
        Vector3    basePos;         // 해당 T의 기본 좌표
        Quaternion baseRot;         // 해당 T의 기본 각도

        public ObjectPool<T> Init(GameObject _obj, Transform _parent = null, bool _active = false)
        {
            return Init(_obj, 1, Vector3.zero, Quaternion.identity, _parent, _active);
        }
        public ObjectPool<T> Init(GameObject _obj, int _amount = 1, Vector3? _pos = null, Quaternion? _rot = null, Transform _parent = null, bool _active = false)
        {
            Clear();
            
            refObj = _obj;
            parent = _parent ?? null;
            basePos = _pos ?? Vector3.zero;
            baseRot = _rot ?? Quaternion.identity;

            for (var iter = 0; iter < _amount; iter++)
            {
                var __obj = Create();
                
                if (_active) __obj.Init();
                else         __obj.Release();
            }

            return this;
        }

        // Prefab 제작
        private T Create()
        {
            var _obj = Object.Instantiate(refObj, basePos, baseRot, parent).GetComponent<T>();
            _obj.transform.localPosition = basePos;
            _obj.name = _obj.objectName + Count;

            Add(_obj);

            return _obj;
        }

        // Spawn
        public T Spawn(Vector3? _pos = null, Quaternion? _rot = null, Transform _parent = null)
        {
            var obj = Find(find => find.isActive == false);

            if (obj == null)
            {
                // Is Full
                obj = Create();
            }

            // Is Not Full
            obj.transform.position = _pos ?? basePos;
            obj.transform.rotation = _rot ?? baseRot;
            obj.transform.SetParent(_parent ?? parent);

            obj.Init(); 

            return obj;
        }

        // Release 삭제
        public void Destroy(T _obj)
        {
            if (_obj != null)
                _obj.Release();
        }
    }
}