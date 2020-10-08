using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackKind
{
    Cut,
    Stab,
}

namespace USB
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject[] attackRangeObjs = null;
                         private bool         isTouch = false;
                         private Vector3      dir;
                         private Animator PlayerAnim;
                         private AttackKind attackkind;
                         private float Hp;
        [Tooltip("생성 위치")] public Transform[] spawnPos;

        void Start()
        {
            Hp = 100.0f;
            attackRangeObjs[0].SetActive(false);
            attackRangeObjs[1].SetActive(false);
            attackRangeObjs[2].SetActive(false);
            attackRangeObjs[3].SetActive(false);
            PlayerAnim = GetComponent<Animator>();
        }

        void Update()
        {
            if(Hp <= 0)
            {
                Hp = 0;
                gameObject.SetActive(false);
            }
            KeytoTouch();
        }
        private void KeytoTouch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LeftTopTouch();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                RightTopTouch();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                LeftBottomTouch();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                RightBottomTouch();
            }
        }

        public void LeftTopTouch()
        {
            StartCoroutine(AttackTimer(0));
            dir = spawnPos[0].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        public void LeftBottomTouch()
        {
            StartCoroutine(AttackTimer(1));
            dir = spawnPos[2].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        public void RightTopTouch()
        {
            StartCoroutine(AttackTimer(2));
            dir = spawnPos[1].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        public void RightBottomTouch()
        {
            StartCoroutine(AttackTimer(3));
            dir = spawnPos[3].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        IEnumerator AttackTimer(int direction)
        {
            if (!isTouch)
            {
                while (true)
                {
                    attackkind = (AttackKind)Random.Range(0, 2);
                    switch (attackkind)
                    {
                        case AttackKind.Cut:
                            PlayerAnim.SetBool("Cut", true);
                            break;
                        case AttackKind.Stab:
                            PlayerAnim.SetBool("Stab", true);
                            break;
                    }

                    isTouch = true;                    
                    attackRangeObjs[direction].SetActive(true);
                    yield return new WaitForSeconds(0.02f);
                    attackRangeObjs[direction].SetActive(false);
                    switch (attackkind)
                    {
                        case AttackKind.Cut:
                            PlayerAnim.SetBool("Cut", false);
                            break;
                        case AttackKind.Stab:
                            PlayerAnim.SetBool("Stab", false);
                            break;
                    }
                    isTouch = false;
                    break;
                }
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            
            if(other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
            {
                Hp -= 10;
            }
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyRange") && other.CompareTag("EnemyRange"))
            {
                Debug.Log(other.gameObject.transform.parent);
                other.gameObject.transform.parent.GetComponent<Enemy>().StartAttack();
                //other.gameObject.GetComponentInParent<Enemy>().StartAttack();
            }
        }
    }
}
