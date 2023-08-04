using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    Rigidbody2D rigid;
    public BoxCollider2D coll;
    public Animator anim;

    public GameObject bullet;
    public GameObject bulletUp;
    public GameObject hitObj;
    public RuntimeAnimatorController animatorController;

    public float moveSpeed;
    public float attackSpeed;
    public float initAttackSpeed; // s스킬 때문에만듦
    public float attackDamage;
    public float initAttackDamage; // s스킬 때문에만듦
    public float range;
    public float maxHp;
    public float hp;
    public int hitNum;
    public float timer;
    public int id;
    public int level;
    public int awakeLevel;

    public float shieldHp; //guard 스킬
    public float shieldTimer;

    public float reinforceTimer;
    public bool reinforceState;


    public bool onAttack;
    public bool isDead;
    public bool deadTrigger;
    public bool isElite;

    public float goldValue;
    public float diamondValue;

    RaycastHit2D hit;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        timer = attackSpeed;
        if (gameObject.tag == "EnemyCastle")
        {
            maxHp = 200 + GameManager.instance.stage * 20;
        }

        diamondValue *= (1 + 0.05f * GameManager.instance.curStage);
        reinforceTimer = 5;

    }
    void Start()
    {
        if (isElite)
        {
            anim.runtimeAnimatorController = animatorController;
            attackDamage *= 1.5f;
            maxHp *= 1.5f;
            hp *= 1.5f;
            //등장 소리
        }
        initAttackDamage = attackDamage;
        initAttackSpeed = attackSpeed;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (id == 1)
            shieldTimer += Time.deltaTime;
        if (id == 2)
        {
            reinforceTimer += Time.deltaTime;
            if (reinforceState)
            {
                attackDamage = initAttackDamage * 1.2f;
                attackSpeed = initAttackSpeed * 1.5f;
            }
            else
            {
                attackDamage = initAttackDamage;
                attackSpeed = initAttackSpeed;
            }

        }
        CheckEnemy();  //onAttack 확인
        Move();
        Dead();

        if (onAttack && !isDead && timer >= attackSpeed)
        {
            StartCoroutine("Attack");
            timer = 0;
        }

        if (id == 1)
        {
            if (shieldHp > 0)
                transform.GetChild(0).gameObject.SetActive(true);
            else
                transform.GetChild(0).gameObject.SetActive(false);

            if (awakeLevel != 0 && shieldTimer >= 5)
            {
                shieldHp = 20 + awakeLevel * 30;
                shieldTimer = 0;
            }
        }
    }

    void Move()
    {
        if (tag == "EnemyCastle") return;

        if (onAttack || isDead)
        {
            rigid.velocity = Vector2.zero;
        }
        else
        {
            rigid.velocity = Vector2.left * moveSpeed;
            anim.SetTrigger("doMove");
        }

    }

    void CheckEnemy()
    {
        if (tag == "EnemyCastle") return;

        if (isDead) return;
        hit = Physics2D.Raycast(transform.position, Vector2.left, range, LayerMask.GetMask("Ally"));
        if(hit.collider != null) hitObj = hit.collider.gameObject;
        else hitObj = null;
        onAttack = hitObj == null ? false : true;

    }

    void Dead()
    {
        
        if (gameObject.tag == "EnemyCastle" && isDead)
        {
            WinLoseUI.instance.StageEnd(true);
            return;
        }

        if (isDead && !deadTrigger)
        {
            GameManager.instance.curStageDiamond += diamondValue * (1 + level * 0.2f);
            int ran = Random.Range(0, 100);
            if(ran < 3)
            {
                GameManager.instance.curStageDropItems[id]++;
            }
            StopCoroutine("Attack");
            Destroy(gameObject, 0.5f);
            deadTrigger = true;
        }
    }

    IEnumerator Attack()
    {
        if (tag == "EnemyCastle") yield break;

        Allys ally;
 

        try
        {
            ally = hitObj.GetComponent<Allys>();
        }
        catch
        {
            yield break;
        }
        switch (id)
        {
            case 0: //archer
                int ran = Random.Range(0, 100);
                if (ran < GameManager.instance.awakeLevel[id] * 25)
                {
                    GameObject instBullet = Instantiate(bulletUp, transform);
                    instBullet.GetComponent<Bullet>().damage = attackDamage * 2f;
                }
                else
                {
                    GameObject instBullet = Instantiate(bullet, transform);
                    instBullet.GetComponent<Bullet>().damage = attackDamage;
                }
                anim.SetTrigger("doAttack");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.AtkA);
                break;
            case 1: 
            case 2:
                if (id == 2 && reinforceTimer > 5 && GameManager.instance.awakeLevel[2] != 0)
                {
                    anim.SetTrigger("doSkill");
                    StartCoroutine("Reinforce");
                    reinforceTimer = 0;
                    yield break;
                }

                if (ally.shieldHp > 0)
                {
                    ally.shieldHp -= attackDamage;
                    if (ally.shieldHp < 0)
                    {
                        ally.hp += ally.shieldHp;
                    }
                }
                else
                    ally.hp -= attackDamage;

                anim.SetTrigger("doAttack");
                if (id == 1) AudioManager.instance.PlaySfx(AudioManager.Sfx.AtkG);
                else AudioManager.instance.PlaySfx(AudioManager.Sfx.AtkS);

                ally.anim.SetTrigger("doHit");
                if (ally.hp <= 0)
                {
                    ally.anim.SetTrigger("doDie");
                    ally.coll.enabled = false;
                    ally.isDead = true;
                    ally.StopCoroutine(Attack());
                    ally.gameObject.layer = 8;
                    hitObj = null;
                    StopCoroutine(Attack());
                }
                break;
            case 3:
                GameObject instBomb = Instantiate(bullet, transform);
                instBomb.GetComponent<Bomb>().awakeLevel = awakeLevel;
                instBomb.transform.SetParent(null);
                instBomb.transform.position = hit.transform.position + new Vector3(2, 2, 0);
                instBomb.GetComponent<Bomb>().damage = attackDamage;
                instBomb.GetComponent<Bomb>().hitNum = hitNum;
                anim.SetTrigger("doAttack");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.AtkW);
                break;

        }
    }

    IEnumerator Reinforce()
    {
        reinforceState = true;
        float time = 2 + awakeLevel * 0.5f;
        yield return new WaitForSeconds(time);
        reinforceState = false;
    }
}
