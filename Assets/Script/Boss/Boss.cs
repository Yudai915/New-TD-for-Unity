using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Chara characs;

    Vector3 pos;

    float timecount = 0;

    // 使用スキル
    float defaultskillcount = 8;
    float skillcount;
    public GameObject circlerange;
    public GameObject circleAttack;
    public GameObject squarerange;
    public GameObject squareAttack;
    public GameObject squarerange2;
    public GameObject squareAttack2;
    int phase = 0;
    float pace = 3;

    // プレイヤーの情報を格納
    [SerializeField] GameObject player;
    [SerializeField] Player playercs;
    Vector3 playerpos;
    float attackcount = 0; // 攻撃速度をカウント

    // 受けたダメージ判定用
    public bool fromPlayer = false;
    public bool fromLaser = false;
    public bool fromhaitu = false;

    // 移動速度
    [SerializeField] float speed;

    // 点滅処理
    bool flashTF = false;

    void Start()
    {
        playerpos = player.transform.position;
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        // ステータスの初期化
        speed = 0.45f;
        skillcount = defaultskillcount;
    }

    void Update()
    {
        attackcount += Time.deltaTime;
        skillcount -= Time.deltaTime;
        timecount += Time.deltaTime;

        if (pos.x <= 6.6823f) // 自陣にたどり着くまで移動
        {
            Move();
        }

        // スキル発動処理
        Skill();

        // 消滅とアイテムドロップ
        if (characs.BossHP <= 0)
        {
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
        if(transform.position.x <= -9.13687f && timecount >= 10f)
        {
            transform.position = new Vector3(-9.13686f, pos.y, pos.z);
        }
    }

    // プレイヤーからの攻撃判定メゾット
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (transform.position.x > -9.13687f)
        {
            if (collider2D.gameObject.tag == "EnemyD")
            {
                fromPlayer = true;
                transform.position = new Vector3(pos.x - 15 * Time.deltaTime, pos.y, pos.z);
                characs.PA_SE();
            }
            if (collider2D.gameObject.tag == "EnemyD_fromLaser")
            {
                fromLaser = true;
                transform.position = new Vector3(pos.x - 40 * Time.deltaTime, pos.y, pos.z);
            }
            if (collider2D.gameObject.tag == "EnemyD_fromhaitu")
            {
                fromhaitu = true;
                transform.position = new Vector3(-8.0f, pos.y, pos.z);
            }
        }
    }

    // スキル発動メゾット
    void Skill()
    {
        if (skillcount <= 0 && skillcount > -512)
        {
            int RandomSkill = Random.Range(0, 4);
            if(characs.BossHP <= characs.MAXBossHP / 2)
            {
                RandomSkill = Random.Range(0, 7);
                pace = 2.5f;
            }
            if (characs.BossHP <= characs.MAXBossHP / 4)
            {
                RandomSkill = Random.Range(4, 7);
                pace = 2f;
            }
            skillcount = -1024;
            switch (RandomSkill) {
                case 1:
                    {
                        StartCoroutine("circleCoroutine");
                    }
                    break;
                case 2:
                    {
                        StartCoroutine("SquareCoroutine");
                    }
                    break;
                case 3:
                    {
                        StartCoroutine("SquareCoroutine2");
                    }
                    break;
                case 4:
                    {
                        StartCoroutine("SquareCoroutine");
                        StartCoroutine("SquareCoroutine2");
                    }
                    break;
                case 5:
                    {
                        StartCoroutine("SquareCoroutine2");
                        StartCoroutine("circleCoroutine");
                    }
                    break;
                case 6:
                    {
                        StartCoroutine("circleCoroutine");
                        StartCoroutine("SquareCoroutine");
                    }
                    break;
                default:
                    {
                        skillcount = defaultskillcount / 2;
                    }
                    break;
            }
        }
        if(characs.BossHP <= characs.MAXBossHP / 2 && phase == 0)
        {
            defaultskillcount /= 2;
            phase++;
        }
        if(characs.BossHP <= characs.MAXBossHP / 4 && phase == 1)
        {
            defaultskillcount /= 4;
            phase++;
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
            gameObject.GetComponent<SpriteRenderer>().color = new Color(138f / 255f, 0, 1f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        flashTF = false;
    }

    // スキル発動処理
       IEnumerator circleCoroutine()
    {
        GameObject Child;
        GameObject Child2;
        int circleRandom = Random.Range(2,15);
        int R = Random.Range(0, 2);
        float posY = 0;
        switch (R)
        {
            case 0: { posY = 0.345f; } break;
            case 1: { posY = -1.59f; } break;
        }
        Child = (GameObject)Instantiate(circlerange, new Vector3(pos.x + circleRandom, posY, pos.z), Quaternion.Euler(0f, 0f, 0f));
        Child.transform.parent = this.transform;
        yield return new WaitForSeconds(pace);
        Child2 = (GameObject)Instantiate(circleAttack, Child.transform.position, Quaternion.Euler(0f, 0f, 0f));
        Child2.transform.parent = Child.transform;
        yield return new WaitForSeconds(0.02f);
        Destroy(Child.gameObject);
        skillcount = defaultskillcount;
    }
    IEnumerator SquareCoroutine()
    {
        GameObject Child;
        GameObject Child2;
        int R = Random.Range(0,5);
        float posX = 0;
        switch (R)
        {
            case 0: { posX = 3.7386f; } break;
            case 1: { posX = -3.72f; } break;
            case 2: { posX = -1.86f; } break;
            case 3: { posX = 0.04f; } break;
            case 4: { posX = 1.86f; } break;
        }
        Child = (GameObject)Instantiate(squarerange, new Vector3(posX, -0.613f, pos.z), Quaternion.Euler(0f, 0f, 0f));
        //Child.transform.parent = this.transform;
        yield return new WaitForSeconds(pace);
        Child2 = (GameObject)Instantiate(squareAttack, Child.transform.position, Quaternion.Euler(0f, 0f, 0f));
        Child2.transform.parent = Child.transform;
        yield return new WaitForSeconds(0.02f);
        Destroy(Child.gameObject);
        skillcount = defaultskillcount;
    }
    IEnumerator SquareCoroutine2()
    {
        GameObject Child;
        GameObject Child2;
        int R = Random.Range(0, 3);
        float posY = 0;
        switch (R)
        {
            case 0: { posY = 1.2537f; } break;
            case 1: { posY = -2.48f; } break;
            case 2: { posY = -0.63f; } break;
        }
        Child = (GameObject)Instantiate(squarerange2, new Vector3(0.0064f, posY, pos.z), Quaternion.Euler(0f, 0f, 0f));
        //Child.transform.parent = this.transform;
        yield return new WaitForSeconds(pace);
        Child2 = (GameObject)Instantiate(squareAttack2, Child.transform.position, Quaternion.Euler(0f, 0f, 0f));
        Child2.transform.parent = Child.transform;
        yield return new WaitForSeconds(0.02f);
        Destroy(Child.gameObject);
        skillcount = defaultskillcount;
    }
}
