using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESt_attack : MonoBehaviour {

    Chara characs;
    BoxCollider2D Collider;

    // 最大の判定サイズ
    float max_x = 0.8338003f;
    float max_y = 1.590788f;

    float count;
    float Stattackspeed;

    void Start () {
        Collider = GetComponent<BoxCollider2D>();
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        count = 0;
        Stattackspeed = 0.1f;
	}
	
	void Update () {
        count += Time.deltaTime;
        if (count >= Stattackspeed)
        {
            Collider.size = new Vector2(max_x, max_y);
            if (count >= Stattackspeed + 0.2f)
            {
                Collider.size = new Vector2(0.0001f, 0.0001f);
                Stattackspeed = Random.Range(1, 6);
                count = 0;
            }
        }
    }
}
