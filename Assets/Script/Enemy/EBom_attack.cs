using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBom_attack : MonoBehaviour {

    Chara characs;
    BoxCollider2D Collider;

    // 最大の判定サイズ
    float max_x = 1.048337f;
    float max_y = 2.128664f;

    float count;
    float Bomattackspeed;

    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        count = 0;
        Bomattackspeed = 0.1f;
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= Bomattackspeed)
        {
            Collider.size = new Vector2(max_x, max_y);
            if (count >= Bomattackspeed + 0.2f)
            {
                Collider.size = new Vector2(0.0001f, 0.0001f);
                Bomattackspeed = Random.Range(1, 6);
                count = 0;
            }
        }
    }
}
