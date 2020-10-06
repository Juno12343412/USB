using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class EnemyDieEffect : PoolingObject
{
    public override string objectName => "DieEffect";

    // Start is called before the first frame update
    public override void Init()
    {
        Invoke("Release", 0.2f);
        base.Init();
    }

    public override void Release()
    {
        base.Release();
    }
}
