using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] Chara characs;
    GameObject boss;
    Player playercs;
    public GameObject magicattack;
    public float magiccount = 0;

    // 消滅カウント
    public float destroycount = 0;

    // スキルオブジェクト
    public GameObject laser;
    public float lasercount = 0;
    public GameObject haitu;
    public float haitucount = 0;

    // スキル範囲オブジェクト
    [SerializeField] GameObject laserrange;

    // 移動間隔
    private Vector3 MOVEX = new Vector3(1.8814f, 0, 0);
    private Vector3 MOVEY = new Vector3(0, 1.8683f, 0);
    // 移動速度
    public float speed = 5f;
    // マジックボール生成速度
    public float magicspeed;
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
        playercs = GameObject.Find("player").GetComponent<Player>();
        boss = GameObject.Find("Boss");

        // 本体と同期
        lasercount = playercs.lasercount;
        haitucount = playercs.haitucount;
    }

    void Update()
    {
        magiccount += Time.deltaTime;
        lasercount -= Time.deltaTime;
        haitucount -= Time.deltaTime;
        destroycount += Time.deltaTime;

        // 移動中かどうかの判定。移動中でなければ入力を受付
        if (transform.position == pos)
        {
            SetposPosition();
        }
        MoveKey();
        //MoveMouse();
        //Movetouch();
        //Movetouch2();

        // マジックボール連射
        if (Input.GetKey(KeyCode.U) || Input.GetButton("ButtonB"))
        {
            MagicAttack();
        }

        // スキル発動
        Skill();

    }

    // 入力に応じて移動後の位置を算出
    void SetposPosition()
    {

        prevPos = pos;

        if (!(pos.x >= 6.6823f || pos.x <= -6.6247f || pos.y >= 2.274f || pos.y <= -3.5239f))
        {
            if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal2") >= 0.5f)
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
            if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal2") <= -0.5f)
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
            if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical2") >= 0.5f)
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
            if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical2") <= -0.5f)
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

            if (characs.playerHP <= 0 || destroycount >= 15)
            {
                Destroy(gameObject);
            }
        }
    }

    // 目的地へ移動する
    void MoveKey()
    {
        if (pos.x >= 6.6823f || pos.x <= -6.6247f || pos.y >= 2.274f || pos.y <= -3.5239f || (boss.transform.position.x + 2.0020f > pos.x && boss.transform.position.x - 2.0020f < pos.x && boss.transform.position.y + 2.0020f > pos.y && boss.transform.position.y - 2.0020f < pos.y))
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

        if (Worldpos.x >= 6.5822f)
        {
            Worldpos.x = 6.5822f;
        }
        else if (Worldpos.x <= -6.5246f)
        {
            Worldpos.x = -6.5246f;
        }
        if (Worldpos.y >= 2.173f)
        {
            Worldpos.y = 2.173f;
        }
        else if (Worldpos.y <= -3.4238f)
        {
            Worldpos.y = -3.4238f;
        }

        transform.position = Vector3.MoveTowards(transform.position, Worldpos, speed * Time.deltaTime);
    }
    void Movetouch()
    {
        pos = transform.position;
        if (Input.touchCount > 0)
        {
            Touch mytouch = Input.touches[0];
            if (mytouch.phase == TouchPhase.Began)
            {
                touchOrigin = mytouch.position;
            }
            else if (mytouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                touchMove = mytouch.position;
                float x = touchMove.x - touchOrigin.x;
                float y = touchMove.y - touchOrigin.y;

                touchOrigin.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
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
            lasercount = playercs.laserDefaultTime;
        }

        if ((Input.GetKeyUp(KeyCode.O) || Input.GetButtonUp("ButtonX")) && haitucount <= 0)
        {
            Instantiate(haitu, haitu.transform.position, Quaternion.Euler(0, 0, 0));
            characs.haitu_SE();
            haitucount = playercs.haituDefaultTime;
        }
    }

    // 非ダメージ処理 + アイテム処理
    void OnTriggerEnter2D(Collider2D collider2D)
    {
      
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        
    }
}
