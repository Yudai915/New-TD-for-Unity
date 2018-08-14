using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_attack : MonoBehaviour {

    Chara characs;
    BoxCollider2D Collider;

    // 最大の判定サイズ
    float max_x = 0.1760569f;
    float max_y = 0.4762753f;

    float count;
    float Bossattackspeed;

    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        count = 0;
        Bossattackspeed = 0.1f;
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= Bossattackspeed)
        {
            Collider.size = new Vector2(max_x, max_y);
            if (count >= Bossattackspeed + 0.2f)
            {
                Collider.size = new Vector2(0.0001f, 0.0001f);
                Bossattackspeed = Random.Range(1, 3);
                count = 0;
            }
        }
    }
}
