using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using Pooling;
using View;

public enum Stage
{
    Stage1, Stage2,
    Stage3, Stage4,
    END
}

public class GameInstance
{
    public float Score = 0;
    public Stage stage = Stage.Stage1;
    public List<float> ScoreList = new List<float>();
}

public class MainGame : BaseScreen<MainGame>
{
    #region Private
    private Stage stageState;
    private float spawnDelay = 0.5f;
    #endregion

    #region Public
    [SerializeField] private GameObject[]      enemyKind  = null;
    [SerializeField] private GameObject[]      Effect  = null;
                     public  ObjectPool<Enemy> enemyPool  = new ObjectPool<Enemy>();
                     public  ObjectPool<HitEffect> HitEffectPool  = new ObjectPool<HitEffect>();
                     public  ObjectPool<EnemyDieEffect> EnemyDieEffect = new ObjectPool<EnemyDieEffect>();
                     public  GameInstance gameinstance = new GameInstance();
    #endregion

    public void Start()
    {
        #region [Screen Setting]
        Camera camera = Camera.main;
        Rect   rect   = camera.rect;
        float  scaleH = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float  scaleW = 1f / scaleH;

        if (scaleH < 1)
        {
            rect.height = scaleH;
            rect.y = (1f - scaleH) / 2f;
        }
        else
        {
            rect.width = scaleW;
            rect.x = (1f - scaleW) / 2f;
        }
        camera.rect = rect;
        #endregion

        stageState = Stage.Stage1;
        enemyPool.Init(enemyKind[0], 10, null, null, null, true);
        HitEffectPool.Init(Effect[0], 5, Vector3.zero, Quaternion.identity);
        EnemyDieEffect.Init(Effect[1], 5, Vector3.zero, Quaternion.identity);
        StartCoroutine(spawnEnemy());
    }

    public override sealed void ShowScreen()
    {
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }

    IEnumerator spawnEnemy()
    {
        while (stageState != Stage.END)
        {
            enemyPool.Spawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void Update()
    {
        if(gameinstance.Score >= 10000)
        {
            gameinstance.stage = Stage.Stage4;
        }
        else if (gameinstance.Score >= 5000)
        {
            gameinstance.stage = Stage.Stage3;
        }
        else if (gameinstance.Score >= 2000)
        {
            gameinstance.stage = Stage.Stage2;
        }
        
    }
}
