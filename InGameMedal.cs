using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMedal : MonoBehaviour
{
    public Animator[] anims;


    void OnEnable()
    {
        StartCoroutine("medalAppearance");
    }
    IEnumerator medalAppearance()
    {
        anims[0].SetTrigger("doMove");
        yield return new WaitForSeconds(0.6f);
        if (SpawnAlly.instance.gameObject.GetComponent<Allys>().hp <
            SpawnAlly.instance.gameObject.GetComponent<Allys>().maxHp / 2)
        {
            yield break;
        }
        anims[1].SetTrigger("doMove");
        yield return new WaitForSeconds(0.6f);
        if (SpawnAlly.instance.gameObject.GetComponent<Allys>().hp <
            SpawnAlly.instance.gameObject.GetComponent<Allys>().maxHp)
        {
            yield break;
        }
        anims[2].SetTrigger("doMove");
    }
}
