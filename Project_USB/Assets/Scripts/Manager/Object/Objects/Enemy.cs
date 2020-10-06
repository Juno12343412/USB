using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public enum Direction
{
    LT, LB,
    RT, RB
}

public enum Kind
{
    Normal, Red, Green
}

[System.Serializable] public class Stats
{
    public Kind      enemyState;
    public Direction direction;
    public Vector3   vecMove;
    public int       curHeart, MaxHeart = 1;
    public float     moveSpeed;
    public Kind      enemyKind;
}

public class Enemy : PoolingObject
{
    public override string objectName => "Enemy";
    [Tooltip("몬스터 정보")] public Stats enemyStats;
    [Tooltip("생성 위치")] public Transform[] spawnPos;
    [Tooltip("범위 들어왔을떄 오브젝트")] public SpriteRenderer spriteRenderer;
    
    public GameObject target;
    private int enemyKindRange = 0;
    [SerializeField] private Sprite EnemyImage1 = null;
    [SerializeField] private Sprite EnemyImage2 = null;
    [SerializeField] private Sprite EnemyImage3 = null;
    [SerializeField] private GameObject EnemyRange = null;
    [SerializeField] private GameObject EnemyAttack = null;
    private SpriteRenderer EnemyImage;
    private Rigidbody2D rigid;
    private Animator EnemyAnim;
    private bool isAttack = false;
    
    public override sealed void Init()
    {
        EnemyAnim = GetComponent<Animator>();
        enemyStats = new Stats();
        EnemyImage = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        switch (MainGame.instance.gameinstance.stage)
        {
            case Stage.Stage1:
                enemyKindRange = 1;
                break;
            case Stage.Stage2:
                enemyKindRange = 2;
                break;
            case Stage.Stage3:
                enemyKindRange = 3;
                break;
            case Stage.Stage4:
                enemyKindRange = 3;
                break;
        }

        enemyStats.enemyKind = (Kind)Random.Range(0, enemyKindRange);
        switch (enemyStats.enemyKind)
        {
            case Kind.Normal:
                EnemyImage.sprite = EnemyImage2;
                enemyStats.curHeart = enemyStats.MaxHeart = 1;
                EnemyAnim.SetBool("Enemy2", true);

                break;
            case Kind.Red:
                EnemyImage.sprite = EnemyImage3;
                enemyStats.curHeart = enemyStats.MaxHeart = 2;
                EnemyAnim.SetBool("Enemy3", true);

                break;
            case Kind.Green:
                EnemyImage.sprite = EnemyImage1;
                enemyStats.curHeart = enemyStats.MaxHeart = 2;
                EnemyAnim.SetBool("Enemy1", true);

                break;
               
        }
        spriteRenderer.enabled = false;
        enemyStats.direction = (Direction)Random.Range(0, 4);

        switch (enemyStats.direction)
        {
            case Direction.LT:
                transform.position = spawnPos[0].position;
                break;
            case Direction.LB:
                transform.position = spawnPos[3].position;
                break;
            case Direction.RT:
                transform.position = spawnPos[1].position;
                break;
            case Direction.RB:
                transform.position = spawnPos[2].position;
                break;
        }
        enemyStats.vecMove = new Vector3(0, 0, 0) - transform.position; 

        enemyStats.moveSpeed = 0.4f;
        base.Init();
    }

    private void Update()
    {
        transform.position += enemyStats.vecMove * enemyStats.moveSpeed * Time.deltaTime;
        
        float angle = Mathf.Atan2(enemyStats.vecMove.y, enemyStats.vecMove.x) * Mathf.Rad2Deg + 125;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (enemyStats.curHeart <= 0)
        {
            MainGame.instance.EnemyDieEffect.Spawn(transform.position, transform.rotation);
            Release();
        }
        //Debug.Log(enemyStats.MaxHeart);
    }

    public sealed override void Release()
    {
        

        switch (enemyStats.enemyKind)
        {
            case Kind.Green:
                MainGame.instance.gameinstance.ScoreList.Add(200);
                break;
            case Kind.Normal:
                MainGame.instance.gameinstance.ScoreList.Add(100);

                //Debug.Log("gdgdgddg");
                break;
            case Kind.Red:
                MainGame.instance.gameinstance.ScoreList.Add(300);
                break;
        }
        base.Release();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            enemyStats.curHeart--;
            if(enemyStats.curHeart > 0)
                MainGame.instance.HitEffectPool.Spawn(transform.position, transform.rotation);
            StartCoroutine(KnockBack());
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player");
            base.Release();
        }

        if (other.CompareTag("Range"))
        {        
            spriteRenderer.enabled = true;
        }
    }

    IEnumerator KnockBack()
    {
        rigid.AddForce(-enemyStats.vecMove, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.35F);
        rigid.velocity = Vector2.zero;
    }

    IEnumerator Attack()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.02F);
        EnemyAttack.SetActive(true);
        EnemyAnim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.02F);
        EnemyAttack.SetActive(false);
        EnemyAnim.SetBool("Attack", false);
        isAttack = false;
    }

    public void StartAttack()
    {
        if(!isAttack)
            StartCoroutine(Attack());
    }
}
