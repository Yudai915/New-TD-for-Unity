using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_attack : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject p_attackEff;
    GameObject chara;
    Chara characs;
    BoxCollider2D Collider;

    // 最大の判定サイズ
    float max_x = 2.486096f;
    float max_y = 2.49283f;

    float count = 0;

	void Start () {
        Collider = GetComponent<BoxCollider2D>();
        chara = GameObject.Find("Chara");
        characs = chara.GetComponent<Chara>();
        StartCoroutine("Attacking");
	}
	
	void Update () {
        //transform.position = player.gameObject.transform.position;

        count += Time.deltaTime;
        if (count >= characs.attackspeed)
        {
  
            if (count >= characs.attackspeed + 0.2f)
            {
                
                count = 0;
            }
        }
    }

    IEnumerator Attacking()
    {
        while (true)
        {
            Collider.size = new Vector2(max_x, max_y);
            Instantiate(p_attackEff, player.transform.position, Quaternion.Euler(0f, 0f, 0f));
            yield return new WaitForSeconds(0.2f);
            Collider.size = new Vector2(0.0001f, 0.0001f);
            yield return new WaitForSeconds(characs.attackspeed);
        }
    }
}
