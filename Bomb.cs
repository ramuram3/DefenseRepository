using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public enum Team { Ally, Enemy };
    public Team team;

    public GameObject fragment;
    public bool isFragment;

    public float damage;
    public float bombRadius;
    public int hitNum;

    public int awakeLevel; //only Enemy

    RaycastHit2D[] rayHits;

    void Start()
    {
        if(team == Team.Ally)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(3, -3);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-3, -3);
        }
        bombRadius = isFragment ? 0.8f : 1.2f;

        if (isFragment)
        {
            damage /= 2;
            if(team == Team.Ally)
            {
                int ranX = Random.Range(-10, 30);
                int ranY = Random.Range(50, 100);
                Vector2 bounceVec = new Vector2(ranX, ranY).normalized;
                GetComponent<Rigidbody2D>().velocity = bounceVec * 5f;
            }
            else
            {
                int ranX = Random.Range(-30, 10);
                int ranY = Random.Range(50, 100);
                Vector2 bounceVec = new Vector2(ranX, ranY).normalized;
                GetComponent<Rigidbody2D>().velocity = bounceVec * 5f;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" && team == Team.Ally)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BulletW);
            rayHits = 
            Physics2D.CircleCastAll(transform.position, bombRadius, transform.position, 0, 1 << 6);

            if(hitNum <= rayHits.Length)
            {
                for (int i = 0; i < hitNum; i++)
                {
                    Enemys enemy = rayHits[i].collider.GetComponent<Enemys>();
                    enemy.anim.SetTrigger("doHit");

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
                }
            }
            else
            {
                for (int i = 0; i < rayHits.Length; i++)
                {
                    Enemys enemy = rayHits[i].collider.gameObject.GetComponent<Enemys>();
                    enemy.anim.SetTrigger("doHit");

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
                }
            }
            if(!isFragment)
            {
                for (int i = 0; i < GameManager.instance.awakeLevel[3]; i++)
                {
                    GameObject fragmentObj = Instantiate(fragment, transform.position + Vector3.up * 0.1f, Quaternion.Euler(Vector3.back * 3));
                    fragmentObj.GetComponent<Bomb>().damage = damage;
                    fragmentObj.GetComponent<Bomb>().hitNum = hitNum;
                }
            }

            Destroy(gameObject);
        }

        if (collision.tag == "Ground" && team == Team.Enemy)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.BulletW);

            rayHits =
            Physics2D.CircleCastAll(transform.position, bombRadius, transform.position, 0, 1 << 7);

            if (hitNum <= rayHits.Length)
            {
                for (int i = 0; i < hitNum; i++)
                {
                    Allys ally = rayHits[i].collider.gameObject.GetComponent<Allys>();
                    ally.anim.SetTrigger("doHit");

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
                }
            }
            else
            {
                for (int i = 0; i < rayHits.Length; i++)
                {
                    Allys ally = rayHits[i].collider.GetComponent<Allys>();
                    ally.anim.SetTrigger("doHit");

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
                }
            }

            if (!isFragment)
            {
                for (int i = 0; i < awakeLevel; i++)
                {
                    GameObject fragmentObj = Instantiate(fragment, transform.position + Vector3.up * 0.1f, Quaternion.Euler(Vector3.forward * 3));
                    fragmentObj.GetComponent<Bomb>().damage = damage;
                    fragmentObj.GetComponent<Bomb>().hitNum = hitNum;
                }
            }

            Destroy(gameObject);
        }
    }
}
