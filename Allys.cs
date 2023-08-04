using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allys : MonoBehaviour
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

    public float shieldHp; //guard 스킬
    public float shieldTimer;

    public float reinforceTimer;
    public bool reinforceState;

    public bool onAttack; //rayhit에 잡혔다 or 안잡혔다
    public bool isDead;
    public bool isElite;

    public RaycastHit2D hit;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        if (gameObject.tag == "AllyCastle")
        {
            maxHp = 200 + GameManager.instance.stage * 20;
            hp = 200 + GameManager.instance.stage * 20;
        }

        timer = attackSpeed;
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
            if(reinforceState)
            {
                attackDamage = initAttackDamage * 1.2f;
                attackSpeed = initAttackSpeed * (float)2/3f;
                transform.GetChild(0).transform.gameObject.SetActive(true);
            }
            else
            {
                attackDamage = initAttackDamage;
                attackSpeed = initAttackSpeed;
                transform.GetChild(0).transform.gameObject.SetActive(false);
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

        if(id == 1)
        {
            if (shieldHp > 0)
                transform.GetChild(0).gameObject.SetActive(true);
            else
                transform.GetChild(0).gameObject.SetActive(false);

            if(GameManager.instance.awakeLevel[1] != 0 && shieldTimer >= 5)
            {
                shieldHp = 50 + GameManager.instance.awakeLevel[1] * 50;
                shieldTimer = 0;
            }
        }
    }
    
    void Move()
    {
        if (tag == "AllyCastle") return;

        if (onAttack || isDead)
        {
            rigid.velocity = Vector2.zero;
        }
        else
        {
            rigid.velocity = Vector2.right * moveSpeed;
            anim.SetTrigger("doMove");
        }

    }

    void CheckEnemy()
    {
        if (tag == "AllyCastle") return;

        if (isDead) return;
        hit = Physics2D.Raycast(transform.position, Vector2.right, range, LayerMask.GetMask("Enemy"));
        if (hit.collider != null) hitObj = hit.collider.gameObject;
        else hitObj = null;
        onAttack = hitObj == null ? false : true;

    }

    void Dead()
    {
        if (gameObject.tag == "AllyCastle" && isDead)
        {
            WinLoseUI.instance.StageEnd(false);
            return;
        }

        if (isDead)
        {
            StopCoroutine("Attack");
            Destroy(gameObject, 0.5f);
        }
    }
    IEnumerator Attack()
    {
        if (tag == "AllyCastle") yield break;

        Enemys enemy;

        try
        {
            enemy = hitObj.GetComponent<Enemys>();
        }
        catch
        {
            yield break;
        }
        switch (id)
        {
            case 0: //archer
                int ran = Random.Range(0, 100);
                if(ran < GameManager.instance.awakeLevel[id] * 25)
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
                if(id == 2 && reinforceTimer > 5 && GameManager.instance.awakeLevel[2] != 0)
                {
                    anim.SetTrigger("doSkill");
                    StartCoroutine("Reinforce");
                    reinforceTimer = 0;
                    yield break;
                }
                if(enemy.shieldHp > 0)
                {
                    enemy.shieldHp -= attackDamage;
                    if(enemy.shieldHp < 0)
                    {
                        enemy.hp += enemy.shieldHp;
                    }
                }
                else
                    enemy.hp -= attackDamage;


                anim.SetTrigger("doAttack");
                if(id == 1) AudioManager.instance.PlaySfx(AudioManager.Sfx.AtkG);
                else AudioManager.instance.PlaySfx(AudioManager.Sfx.AtkS);

                enemy.anim.SetTrigger("doHit");
                if (enemy.hp <= 0)
                {
                    SpawnAlly.instance.gold += enemy.goldValue;
                    enemy.anim.SetTrigger("doDie");
                    enemy.coll.enabled = false;
                    enemy.isDead = true;
                    enemy.StopCoroutine(Attack());
                    enemy.gameObject.layer = 8;
                    hitObj = null;
                    StopCoroutine(Attack());
                }
                break;
            case 3:
                GameObject instBomb = Instantiate(bullet, transform);
                instBomb.transform.SetParent(null);
                instBomb.transform.position = hit.transform.position + new Vector3(-2, 2, 0);
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
        float time = 2 + GameManager.instance.awakeLevel[id] * 0.5f;
        yield return new WaitForSeconds(time);
        reinforceState = false;
    }
}
