using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Team { Ally, Enemy };
    public Team team;
    public float damage;
    public float speed;

    Rigidbody2D rigid;
    BoxCollider2D coll;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        transform.position = transform.position + Vector3.up * 0.2f;
        Destroy(gameObject, 0.8f);
    }

    void Update()
    {
        if (team == Team.Ally)
            transform.position += Vector3.right * speed * Time.deltaTime;
        if (team == Team.Enemy)
            transform.position += Vector3.left * speed * Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (team == Team.Ally && collision.gameObject.layer == 6 && collision.gameObject.layer != 8)
        {
            Enemys enemy = collision.GetComponent<Enemys>();
            enemy.anim.SetTrigger("doHit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BulletA);

            if (enemy.shieldHp > 0)
            {
                enemy.shieldHp -= damage;
                if (enemy.shieldHp < 0)
                {
                    enemy.hp += enemy.shieldHp;
                }
            }
            else
                enemy.hp -= damage;

            if (enemy.hp <= 0)
            {
                SpawnAlly.instance.gold += enemy.goldValue;
                enemy.isDead = true;
                enemy.anim.SetTrigger("doDie");
                enemy.coll.enabled = false;
                enemy.gameObject.layer = 8;
            }
            Destroy(gameObject);
        }

        if (team == Team.Enemy && collision.gameObject.layer == 7 && collision.gameObject.layer != 8)
        {
            Allys ally = collision.GetComponent<Allys>();
            ally.anim.SetTrigger("doHit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BulletA);

            if (ally.shieldHp > 0)
            {
                ally.shieldHp -= damage;
                if (ally.shieldHp < 0)
                {
                    ally.hp += ally.shieldHp;
                }
            }
            else
                ally.hp -= damage;

            if (ally.hp <= 0)
            {
                ally.isDead = true;
                ally.anim.SetTrigger("doDie");
                ally.coll.enabled = false;
                ally.gameObject.layer = 8;
            }
            Destroy(gameObject);
        }
    }

}
