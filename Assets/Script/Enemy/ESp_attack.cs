using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESp_attack : MonoBehaviour
{

    Chara characs;
    BoxCollider2D Collider;

    // 最大の判定サイズ
    float max_x = 3.27358f;
    float max_y = 6.627959f;

    float count;
    float Spattackspeed;

    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        count = 0;
        Spattackspeed = 0.1f;
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= Spattackspeed)
        {
            Collider.size = new Vector2(max_x, max_y);
            if (count >= Spattackspeed + 0.2f)
            {
                Collider.size = new Vector2(0.0001f, 0.0001f);
                Spattackspeed = Random.Range(1, 6);
                count = 0;
            }
        }
    }
}