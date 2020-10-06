using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class HitEffect : PoolingObject
{

    public override string objectName => "Effect";

    public override void Init()
    {
        Invoke("Release", 0.25f);
        base.Init();
    }

    public override void Release()
    {
        base.Release();
    }
}
