using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySp : MonoBehaviour
{

    public Chara characs;

    Vector3 pos;

    // プレイヤーの情報を格納
    [SerializeField] GameObject player;
    [SerializeField] Player playercs;
    Vector3 playerpos;
    float attackcount = 0; // 攻撃速度をカウント


    // ステータス
    public int MAXSpHP = 5000;
    public int HP;

    // 移動速度
    [SerializeField] float speed;

    // 点滅判定
    bool flashTF = false;

    void Start()
    {
        playerpos = player.transform.position;
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        // ステータスの初期化
        HP = MAXSpHP;
        speed = 4.5f;
    }

    void Update()
    {
        attackcount += Time.deltaTime;

        if (pos.x <= 6.6823f) // 自陣にたどり着くまで移動
        {
            Move();
        }

        // 消滅とアイテムドロップ
        if (HP <= 0)
        {
            int item = Random.Range(0, 20);
            switch (item)
            {
                case 0: { characs.Item_ASup(gameObject); } break;
                case 1: { characs.Item_MSup(gameObject); } break;
            }
            Destroy(gameObject);
        }

        if (flashTF)
        {
            StartCoroutine("Flashing");
        }
    }

    // 移動メゾット
    void Move()
    {
        pos = transform.position;
        transform.position = new Vector3(pos.x + speed * Time.deltaTime, pos.y, pos.z);
    }

    // プレイヤーからの攻撃判定メゾット
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (transform.position.x > -8.5f)
        {
            if (collider2D.gameObject.tag == "EnemyD")
            {
                HP -= characs.DamageProcess(gameObject, characs.playerAttack, characs.SpDefense, 0);
                characs.PA_SE();
            }
            if (collider2D.gameObject.tag == "EnemyD_fromLaser")
            {
                HP -= characs.DamageProcess(gameObject, characs.playerAttack, characs.SpDefense, 2);
            }
            if (collider2D.gameObject.tag == "EnemyD_fromhaitu")
            {
                HP -= characs.DamageProcess(gameObject, characs.playerAttack, characs.SpDefense, 3);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "EnemyD" || collider2D.gameObject.tag == "EnemyD_fromLaser")
        {
            flashTF = true;
        }
    }

    // 点滅処理
    IEnumerator Flashing()
    {
        for (int i = 1; i <= 2; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 55f / 255f);
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        flashTF = false;
    }
}
