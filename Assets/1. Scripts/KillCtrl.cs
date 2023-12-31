using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCtrl : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject kill_anim, text_anim, mainView;
    
    List<int> number = new List<int>();

    int count;

    // 초기화
    public void KillReset()
    {
        kill_anim.SetActive(false);
        text_anim.SetActive(false);

        number.Clear();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(spawnPoints[i].childCount != 0)
            {
                Destroy(spawnPoints[i].GetChild(0).gameObject);
            }
        }

        NPCSpawn();
    }

    // NPC 스폰
    public void NPCSpawn()
    {
        int rand = Random.Range(0, 10);

        for (int i = 0; i < 5;)
        {
            // 중복되었다면
            if (number.Contains(rand))
            {
                rand = Random.Range(0, 10);
            }
            // 중복되지 않았다면
            else
            {
                number.Add(rand);
                i++;
            }
        }

        // 스폰
        for (int i = 0; i < number.Count; i++)
        {
            Instantiate(Resources.Load("NPC"), spawnPoints[number[i]]);
        }
    }

    // 킬 하면 호출
    public void Kill()
    {
        count++;

        if(count == 5)
        {
            text_anim.SetActive(true);
            Invoke("Change", 1f);
        }
    }


    // 화면 전환
    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        // 캐릭터 삭제
        FindObjectOfType<PlayerCtrl>().DestroyPlayer();
    }
}
