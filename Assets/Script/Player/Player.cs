using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Chara characs;
    GameObject boss;
    [SerializeField] GameObject healEff;
    [SerializeField] GameObject statusUPEff;
    public GameObject magicattack;
    public float magiccount = 0;

    // スキルオブジェクト
    public GameObject laser;
    public float laserDefaultTime = 10;
    public float lasercount = 0;
    public GameObject haitu;
    public float haituDefaultTime = 20;
    public float haitucount = 0;
    public GameObject Clone;
    public float cloneDefaultTime = 30;
    public float Clonecount = 0;

    // スキル範囲オブジェクト
    [SerializeField] GameObject laserrange;
    [SerializeField] GameObject haiturange;

    // 移動間隔
    private Vector3 MOVEX = new Vector3(1.8814f, 0, 0);
    private Vector3 MOVEY = new Vector3(0, 1.8683f, 0);
    // 移動速度
    public float speed = 5f;
    // マジックボール生成速度
    public float magicspeed;
    // 受けたダメージ判定用
    public bool fromSt = false;
    public bool fromSp = false;
    public bool fromDef = false;
    public bool fromBom = false;
    public bool fromBominBom = false;
    public bool fromBoss = false;
    public bool fromBoss_circle = false;
    // 点滅判定
    bool flashTF = false;
    // 移動後の場所保存用
    Vector3 pos;
    // 移動できなかった場合の移動前の場所を格納用
    Vector3 prevPos;
    // マウス移動用
    Vector3 mousepos;
    Vector3 Worldpos;
    // タッチ移動用
    Vector3 touchpos;
    Vector2 touchOrigin = -Vector2.one;
    Vector2 touchMove = -Vector2.one;

    Animator animator;


    void Start()
    {
        pos = transform.position;
        animator = GetComponent("Animator") as Animator;
        characs = GameObject.Find("Chara").GetComponent<Chara>();
        boss = GameObject.Find("Boss");
    }

    void Update()
    {
        magiccount += Time.deltaTime;
        lasercount -= Time.deltaTime;
        haitucount -= Time.deltaTime;
        Clonecount -= Time.deltaTime;

        // 移動中かどうかの判定。移動中でなければ入力を受付
        if (transform.position == pos && characs.playerHP > 0)
        {
            SetposPosition();
        }
        if (characs.playerHP > 0)
        {
            MoveKey();
        }
        //MoveMouse();
        //Movetouch();
        //Movetouch2();
     
        // マジックボール連射
        if ((Input.GetKey(KeyCode.U) || Input.GetButton("ButtonB")) && characs.playerHP > 0)
        {
            MagicAttack();
        }

        // スキル発動
        if (characs.playerHP > 0)
        {
            Skill();
        }

        // 非ダメージ時、点滅処理
        if (flashTF)
        {
            StartCoroutine("Flashing");
        }

        // 色変化処理(HPに応じて)
        playercolor();

        // 死亡処理
        if (characs.playerHP <= 0)
        {
            transform.position = new Vector3(6.5507f, 7.7834f, 0);
        }
    }

    // 入力に応じて移動後の位置を算出
    void SetposPosition()
    {

        prevPos = pos;

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") >= 0.5f)
        {
            pos = transform.position + MOVEX;
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
            animator.SetBool("Up", false);
            animator.SetBool("down", false);
            return;
        }
        else
        {
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") <= -0.5f)
        {
            pos = transform.position - MOVEX;
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
            animator.SetBool("Up", false);
            animator.SetBool("down", false);
            return;
        }
        else
        {
            animator.SetBool("Left", false);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") >= 0.5f)
        {
            pos = transform.position + MOVEY;
            animator.SetBool("Up", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            animator.SetBool("down", false);
            return;
        }
        else
        {
            animator.SetBool("Up", false);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") <= -0.5f)
        {
            pos = transform.position - MOVEY;
            animator.SetBool("down", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            animator.SetBool("Up", false);
            return;
        }
        else
        {
            animator.SetBool("down", false);
        }
    }

    // 目的地へ移動する
    void MoveKey()
    {
        if (pos.x >= 6.6823f || pos.x <= -6.6247f || (pos.y >= 2.274f && !(transform.position.y == 3.13506f)) || pos.y <= -3.5239f || (boss.transform.position.x + 2.0020f > pos.x && boss.transform.position.x - 2.0020f < pos.x && boss.transform.position.y + 2.0020f > pos.y && boss.transform.position.y - 2.0020f < pos.y))
        {
            pos = prevPos;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        }
    }
    void MoveMouse()
    {
        if (Input.GetMouseButton(0))
        {
            mousepos = Input.mousePosition;
        }
        Worldpos = Camera.main.ScreenToWorldPoint(mousepos);

        if(Worldpos.x >= 6.5822f)
        {
            Worldpos.x = 6.5822f;
        }else if(Worldpos.x <= -6.5246f)
        {
            Worldpos.x = -6.5246f;
        }
        if (Worldpos.y >= 2.173f)
        {
            Worldpos.y = 2.173f;
        }else if(Worldpos.y <= -3.4238f)
        {
            Worldpos.y = -3.4238f;
        }

       transform.position = Vector3.MoveTowards(transform.position, Worldpos, speed * Time.deltaTime);
    }
    void Movetouch()
    {      
        pos = transform.position;
        if(Input.touchCount > 0)
        {
            Touch mytouch = Input.touches[0];
            if(mytouch.phase == TouchPhase.Began)
            {
                touchOrigin = mytouch.position;
            }
            else if(mytouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                touchMove = mytouch.position;
                float x = touchMove.x - touchOrigin.x;
                float y = touchMove.y - touchOrigin.y;

                touchOrigin.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if(x > 0)
                    {
                        pos = transform.position + MOVEX;
                    }
                    else
                    {
                        pos = transform.position - MOVEX;
                    }
                }
                else
                {
                    if (y > 0)
                    {
                        pos = transform.position + MOVEY;
                    }
                    else
                    {
                        pos = transform.position - MOVEY;
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
            }
        }
    }
    void Movetouch2()
    {
        Touch touch = Input.GetTouch(0);

        Vector3 vec = touch.position;
        vec.z = 10f;

        vec = Camera.main.ScreenToWorldPoint(vec);

        if (vec.x >= 6.5822f)
        {
            vec.x = 6.5822f;
        }
        else if (vec.x <= -6.5246f)
        {
            vec.x = -6.5246f;
        }
        if (vec.y >= 2.173f)
        {
            vec.y = 2.173f;
        }
        else if (vec.y <= -3.4238f)
        {
            vec.y = -3.4238f;
        }

        transform.position = Vector3.MoveTowards(transform.position, vec, speed * Time.deltaTime);
    }

    // マジックボール生成
    public void MagicAttack()
    {
        magicspeed = characs.attackspeed;
        if (magicspeed < 0.3f) { magicspeed = 0.3f; }
        if (magiccount >= magicspeed)
        {
            Instantiate(magicattack, transform.position, Quaternion.Euler(0, 0, 0));
            magiccount = 0;
        }
    }

    // スキル発動処理
    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("ButtonA"))
        {
            GameObject laserobj;
            laserobj = (GameObject)Instantiate(laserrange, transform.position, Quaternion.Euler(0f, 0f, 0f));
            laserobj.transform.parent = this.transform;
        }
        if ((Input.GetKeyUp(KeyCode.I) || Input.GetButtonUp("ButtonA")) && lasercount <= 0)
        {
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 0));
            characs.Laser_SE();
            lasercount = laserDefaultTime;
        }

        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("ButtonX"))
        {
            Instantiate(haiturange, haiturange.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        if ((Input.GetKeyUp(KeyCode.O) || Input.GetButtonUp("ButtonX")) && haitucount <= 0)
        {
            Instantiate(haitu, haitu.transform.position, Quaternion.Euler(0, 0, 0));
            characs.haitu_SE();
            haitucount = haituDefaultTime;
        }

        if ((Input.GetKeyUp(KeyCode.P) || Input.GetButtonUp("ButtonY")) && Clonecount <= 0)
        {
            Instantiate(Clone, Clone.transform.position, Quaternion.Euler(0, 0, 0));
            Clonecount = cloneDefaultTime;
        }
    }
    
    // 非ダメージ処理 + アイテム処理
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "PlayerD_fromSt")
        {
            fromSt = true;
        }
        if(collider2D.gameObject.tag == "PlayerD_fromSp")
        {
            fromSp = true;
        }
        if (collider2D.gameObject.tag == "PlayerD_fromDef")
        {
            fromDef = true;
        }
        if (collider2D.gameObject.tag == "PlayerD_fromBom")
        {
            fromBom = true;
        }
        if (collider2D.gameObject.tag == "PlayerD_fromBominBom")
        {
            fromBominBom = true;
        }
        if (collider2D.gameObject.tag == "PlayerD_fromBoss")
        {
            fromBoss = true;
        }
        if (collider2D.gameObject.tag == "PlayerD_fromcircle")
        {
            fromBoss_circle = true;
        }

        // 回復処理
        if (collider2D.gameObject.tag == "heart")
        {
            characs.Item_heart();
            Instantiate(healEff, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        // 攻撃速度アップ処理
        if(collider2D.gameObject.tag == "AttackSpeedUp")
        {
            characs.Item_ASUp_F();
            Instantiate(statusUPEff, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        // 移動速度アップ処理
        if (collider2D.gameObject.tag == "MoveSpeedUp")
        {
            characs.Item_MSUp_F();
            Instantiate(statusUPEff, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        // 攻撃力アップ処理
        if (collider2D.gameObject.tag == "AttackUp")
        {
            characs.Item_AUp_F();
            Instantiate(statusUPEff, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "PlayerD_fromSt" || collider2D.gameObject.tag == "PlayerD_fromSp" 
            || collider2D.gameObject.tag == "PlayerD_fromDef" || collider2D.gameObject.tag == "PlayerD_fromBom"
            || collider2D.gameObject.tag == "PlayerD_fromBominBom" || collider2D.gameObject.tag == "PlayerD_fromBoss"
            || collider2D.gameObject.tag == "PlayerD_fromcircle")
        {
            flashTF = true;
            characs.EA_SE();
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
    // 色変化処理
    void playercolor()
    {
        if(characs.playerHP < characs.MAXplayerHP / 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 120f / 255f, 0);
            if(characs.playerHP < characs.MAXplayerHP / 4)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            }
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
    }


    // ノックバック処理 引数...ノックバックの強度
    public void knockback(int str)
    {
        if (transform.position == pos)
        {
            switch (str)
            {
                case 1:
                    {
                        pos = transform.position + MOVEX;
                    }
                    break;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, 70 * Time.deltaTime);
    }

    public void repop()
    {
        transform.position = new Vector3(6.5507f, 3.13506f, 0);
        return;
    }
}
