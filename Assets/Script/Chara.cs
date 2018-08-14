using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chara : MonoBehaviour
{
    GameObject playerobj;
    public GameObject playerpopobj;
    Player playercs;
    GameObject godobj;
    God godcs;
    GameObject bossobj;
    Boss bosscs;
    public GameObject damageobj;
    public GameObject Enemy;

    // SE
    private AudioSource p_attackSE;
    private AudioSource criticalSE;
    private AudioSource e_attackSE;
    private AudioSource HPhealSE;
    private AudioSource laserSE;
    private AudioSource haituSE;
    private AudioSource statusUPSE;

    // プレイヤーのステータス
    public int MAXplayerHP = 40000;
    public int playerHP;
    public int playerAttack;
    public int playerDefense;
    public float attackspeed;
    private float deltaAS;
    private float defaultAS;
    private float deltaMS;
    private float defaultMS;
    private int deltaA;
    private int defaultA;
    public int deathcount = 0;
    private float playerpopcount = 0;


    // 自陣のステータス
    public int MAXGodHP = 50000;
    public int GodHP;
    public int GodDefense;

    // Bossのステータス
    public int MAXBossHP = 3000000;
    public int BossHP;
    public int BossAttack;
    public int BossDefense;
    private int bossDefphase = 0;

    // 敵Stのステータス
    public int StAttack;
    public int StDefense;

    // 敵Spのステータス
    public int SpAttack;
    public int SpDefense;

    // 敵Defのステータス
    public int DefAttack;
    public int DefDefense;

    // 敵Bomのステータス
    public int BomAttack;
    public int BomDefense;

    // アイテム
    [SerializeField] GameObject heart;
    [SerializeField] GameObject AttackSpeedUp;
    [SerializeField] GameObject MoveSpeedUp;
    [SerializeField] GameObject AttackUp;

    // クリアエフェクト
    public GameObject clear;
    private bool clearTF = true;

    // 全滅エフェクト
    public GameObject burst;
    private bool GodDeadTF = true;

    void Start()
    {
        playerobj = GameObject.Find("player");
        playercs = GameObject.Find("player").GetComponent<Player>();
        godobj = GameObject.Find("God");
        godcs = GameObject.Find("God").GetComponent<God>();
        bossobj = GameObject.Find("Boss");
        bosscs = GameObject.Find("Boss").GetComponent<Boss>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        p_attackSE = audioSources[0];
        criticalSE = audioSources[1];
        e_attackSE = audioSources[2];
        HPhealSE = audioSources[3];
        laserSE = audioSources[4];
        haituSE = audioSources[5];
        statusUPSE = audioSources[6];

        StartCoroutine("Enemypop");
        StartCoroutine("EnemypopBoss");
        StartCoroutine("EnemypopBoss2");

        // プレイヤーのステータス初期化
        playerHP = MAXplayerHP;
        playerAttack = 13600;
        playerDefense = 1900;
        attackspeed = 0.75f;
        defaultAS = attackspeed;
        defaultMS = playercs.speed;
        defaultA = playerAttack;

        // 自陣のステータス初期化
        GodHP = MAXGodHP;
        GodDefense = 500;

        // Bossのステータス初期化
        BossHP = MAXBossHP;
        BossAttack = 5000;
        BossDefense = 1900;

        // 敵Stのステータス初期化
        StAttack = 2500;
        StDefense = 3600;

        // 敵Spのステータス初期化
        SpAttack = 1450;
        SpDefense = 1850;

        // 敵Defのステータス初期化
        DefAttack = 1450;
        DefDefense = 19300;

        // 敵Bomのステータス初期化
        BomAttack = 2500;
        BomDefense = 3600;
    }

    void Update()
    {
        // 敵を識別しプレイヤーがダメージを受ける
        if (playercs.fromSt)
        {
            P_DamageProcess(1);
            playercs.fromSt = false;
        }
        if (playercs.fromSp)
        {
            P_DamageProcess(2);
            playercs.fromSp = false;
        }
        if (playercs.fromDef)
        {
            P_DamageProcess(3);
            playercs.fromDef = false;
        }
        if (playercs.fromBom)
        {
            P_DamageProcess(4);
            playercs.fromBom = false;
        }
        if (playercs.fromBoss)
        {
            P_DamageProcess(5);
            playercs.fromBoss = false;
        }
        if (playercs.fromBominBom)
        {
            P_DamageProcess(6);
            playercs.fromBominBom = false;
        }
        if (playercs.fromBoss_circle)
        {
            P_DamageProcess(7);
            playercs.fromBoss_circle = false;
        }

        // 敵を識別し自陣がダメージを受ける 
        if (godcs.fromSt)
        {
            G_DamageProcess(1);
            godcs.fromSt = false;
        }
        if (godcs.fromSp)
        {
            G_DamageProcess(2);
            godcs.fromSp = false;
        }
        if (godcs.fromDef)
        {
            G_DamageProcess(3);
            godcs.fromDef = false;
        }
        if (godcs.fromBom)
        {
            G_DamageProcess(4);
            godcs.fromBom = false;
        }
        if (godcs.fromBoss)
        {
            G_DamageProcess(5);
            godcs.fromBoss = false;
        }

        // ボスが受けるダメージを識別して処理
        if (bosscs.fromPlayer)
        {
            B_DamageProcess(1);
            bosscs.fromPlayer = false;
        }
        if (bosscs.fromLaser)
        {
            B_DamageProcess(2);
            bosscs.fromLaser = false;
        }
        if (bosscs.fromhaitu)
        {
            B_DamageProcess(3);
            bosscs.fromhaitu = false;
        }

        if (BossHP <= MAXBossHP / 2 && bossDefphase == 0)
        {
            BossDefense *= 2;
            bossDefphase++;
        }
        if (BossHP <= MAXBossHP / 4 && bossDefphase == 1)
        {
            BossDefense *= 5;
            bossDefphase++;
        }

        // プレイヤー復活メゾット
        playerpop();

        // クリア演出
        if (BossHP <= 0 && clearTF)
        {
            clearTF = false;
            StartCoroutine("ClearEff");
        }

        // 全滅演出
        if (GodHP <= 0)
        {
            Time.timeScale -= 0.005f;
            if (Time.timeScale <= 0.005f)
            {
                Time.timeScale = 0.005f;
                if (Input.anyKey)
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene("title");
                }
            }
        }
        if (GodHP <= 0 && GodDeadTF)
        {
            GodDeadTF = false;
            StartCoroutine("DeadEff");
        }
    }

    public int damage;

    // ダメージ計算式 + 表示　第四引数{0...通常ダメージ | 1...スキル[誘爆]ダメージ
    //                                 2...スキル[レーザー]ダメージ | 3...スキル[ワザリングハイツ]ダメージ
    //                                 4...ボススキル[サークル]ダメージ}
    public int DamageProcess(GameObject obj, int attack, int defense, int skill)
    {
        int critical;
        string damagestring;
        switch (skill)
        {
            case 0:
                {
                    critical = Random.Range(0, 6); // 0が出たらcritical
                    switch (critical)
                    {
                        case 0:
                            {
                                damage = ((attack / 2) - (defense / 4)) + (((attack / 2) - (defense / 4)) / 2) + Random.Range(-100, 101);
                                criticalSE.PlayOneShot(criticalSE.clip);
                                damageobj.GetComponent<TextMesh>().color = Color.yellow;
                                if (obj.transform.name == "player")
                                {
                                    playercs.knockback(1);
                                }
                            }
                            break;
                        default:
                            {
                                damage = ((attack / 2) - (defense / 4)) + Random.Range(-100, 101);
                                damageobj.GetComponent<TextMesh>().color = Color.white;
                                if (obj.transform.name == "player" || obj.transform.name == "God")
                                {
                                    damageobj.GetComponent<TextMesh>().color = Color.red;
                                }
                            }
                            break;
                    }
                }
                break;
            case 1:
                {
                    damage = playerHP / 4;
                    if (playerHP - damage < 0)
                    {
                        damage = 0;
                    }
                    damageobj.GetComponent<TextMesh>().color = Color.red;
                }
                break;
            case 2:
                {
                    damage = ((attack / 2) - (defense / 4)) + Random.Range(-100, 101);
                    damage *= 7;
                    damageobj.GetComponent<TextMesh>().color = Color.white;
                }
                break;
            case 3:
                {
                    damage = ((attack / 2) - (defense / 4)) + Random.Range(-100, 101);
                    damage *= 3;
                    damageobj.GetComponent<TextMesh>().color = Color.white;
                }
                break;
            case 4:
                {
                    damage = ((attack / 2) - (defense / 4)) + Random.Range(-100, 101);
                    damage *= 13;
                    damageobj.GetComponent<TextMesh>().color = Color.red;
                }
                break;
        }

        if (damage < 0)
        {
            damage = 0;
        }

        Vector3 objpos = obj.transform.position;
        damagestring = damage.ToString();
        damageobj.GetComponent<TextMesh>().text = damagestring;
        if (obj.transform.name == "God")
        {
            Instantiate(damageobj, new Vector3(objpos.x - 1.5f, objpos.y, objpos.z), Quaternion.Euler(0f, 0f, 0f));
        }
        else
        {
            Instantiate(damageobj, obj.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        return damage;
    }

    // プレイヤーが受けたダメージ処理
    public void P_DamageProcess(int enemynum)
    {
        int attack = 0;
        // 1...EnemySt | 2...EnemySp | 3...EnemyDef | 4...EnemyBom | 5...Boss
        // 6...EnemyBomの誘爆 | 7...Boss_circle
        if (enemynum <= 5)
        {
            switch (enemynum)
            {
                case 1: { attack = StAttack; } break;
                case 2: { attack = SpAttack; } break;
                case 3: { attack = DefAttack; } break;
                case 4: { attack = BomAttack; } break;
                case 5: { attack = BossAttack; } break;
            }
            playerHP -= DamageProcess(playerobj, attack, playerDefense, 0);
        }
        else
        {
            attack = BossAttack;
            switch (enemynum)
            {
                case 6: { playerHP -= DamageProcess(playerobj, attack, playerDefense, 1); } break;
                case 7: { playerHP -= DamageProcess(playerobj, attack, playerDefense, 4); } break;
            }
        }
    }

    // 自陣が受けたダメージ処理
    public void G_DamageProcess(int enemynum)
    {
        int attack = 0;
        // 1...EnemySt | 2...EnemySp | 3...EnemyDef | 4...EnemyBom | 5...Boss
        switch (enemynum)
        {
            case 1: { attack = StAttack; } break;
            case 2: { attack = SpAttack; } break;
            case 3: { attack = DefAttack; } break;
            case 4: { attack = BomAttack; } break;
            case 5: { attack = BossAttack; } break;
        }
        GodHP -= DamageProcess(godobj, attack, GodDefense, 0);
    }

    // Bossが受けたダメージ処理 引数{0...通常ダメージ | 2...スキル[レーザー] | 3...スキル[ワザリングハイツ]}
    public void B_DamageProcess(int skillnum)
    {
        int B_damage = 0;
        switch (skillnum)
        {
            case 1: { B_damage = DamageProcess(bossobj, playerAttack, BossDefense, 0); break; }
            case 2: { B_damage = DamageProcess(bossobj, playerAttack, BossDefense, 2); break; }
            case 3: { B_damage = DamageProcess(bossobj, playerAttack, BossDefense, 3); break; }
        }
        BossHP -= B_damage;
    }

    // SE
    public void PA_SE()
    {
        p_attackSE.PlayOneShot(p_attackSE.clip);
    }
    public void EA_SE()
    {
        e_attackSE.PlayOneShot(e_attackSE.clip);
    }
    public void Laser_SE()
    {
        laserSE.PlayOneShot(laserSE.clip);
    }
    public void haitu_SE()
    {
        haituSE.PlayOneShot(haituSE.clip);
    }


    // アイテムドロップ処理
    public void Item(GameObject Item)
    {
        Instantiate(heart, Item.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    public void Item_ASup(GameObject Item)
    {
        Instantiate(AttackSpeedUp, Item.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    public void Item_MSup(GameObject Item)
    {
        Instantiate(MoveSpeedUp, Item.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    public void Item_Aup(GameObject Item)
    {
        Instantiate(AttackUp, Item.transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    // アイテム効果処理
    public void Item_heart()
    {
        int heal;
        heal = (MAXplayerHP - playerHP) / 2;
        if (playerHP + heal > MAXplayerHP)
        {
            playerHP = MAXplayerHP;
        }
        else
        {
            playerHP += heal;
        }
        HPhealSE.PlayOneShot(HPhealSE.clip);
    }
    public void Item_ASUp_F()
    {
        StartCoroutine("Item_ASUp");
        statusUPSE.PlayOneShot(statusUPSE.clip);
    }
    IEnumerator Item_ASUp()
    {
        deltaAS = defaultAS * 0.25f;
        attackspeed -= deltaAS;
        if (attackspeed < 0.05f) { attackspeed = 0.05f; }
        yield return new WaitForSeconds(15f);
        attackspeed += deltaAS;
        if (attackspeed > defaultAS) { attackspeed = defaultAS; }
    }
    public void Item_MSUp_F()
    {
        StartCoroutine("Item_MSUp");
        statusUPSE.PlayOneShot(statusUPSE.clip);
    }
    IEnumerator Item_MSUp()
    {
        deltaMS = defaultMS * 0.3f;
        playercs.speed += deltaMS;
        if (playercs.speed > 15f) { playercs.speed = 15f; }
        yield return new WaitForSeconds(15f);
        playercs.speed -= deltaMS;
        if (playercs.speed < defaultMS) { playercs.speed = defaultMS; }
    }
    public void Item_AUp_F()
    {
        StartCoroutine("Item_AUp");
        statusUPSE.PlayOneShot(statusUPSE.clip);
    }
    IEnumerator Item_AUp()
    {
        deltaA = (int)(defaultA * 0.3f);
        playerAttack += deltaA;
        if (playerAttack > defaultA * 3) { playerAttack = defaultA * 3; }
        yield return new WaitForSeconds(15f);
        playerAttack -= deltaA;
        if (playerAttack < defaultA) { playerAttack = defaultA; }
    }

    // 増殖
    IEnumerator Enemypop()
    {
        yield return new WaitForSeconds(5);
        Instantiate(Enemy, Enemy.transform.position, Quaternion.Euler(0f, 0f, 0f));
        yield return new WaitForSeconds(10);
        Instantiate(Enemy, Enemy.transform.position, Quaternion.Euler(0f, 0f, 0f));
        yield return new WaitForSeconds(15);
        Instantiate(Enemy, Enemy.transform.position, Quaternion.Euler(0f, 0f, 0f));
        while (true)
        {
            yield return new WaitForSeconds(30);
            Instantiate(Enemy, Enemy.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
    IEnumerator EnemypopBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            if (BossHP <= MAXBossHP / 2)
            {
                Instantiate(Enemy, Enemy.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }
    IEnumerator EnemypopBoss2()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (BossHP <= MAXBossHP / 4)
            {
                Instantiate(Enemy, Enemy.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }

    // プレイヤー復活メゾット
    private void playerpop()
    {
        if (playerHP <= 0)
        {
            if (!(playerHP == -999999999))
            {
                deathcount++;
                playerHP = -999999999;
            }
            playerpopcount += Time.deltaTime;
            if (deathcount * 5 <= playerpopcount)
            {
                playerHP = MAXplayerHP;
                playercs.repop();
                //playerobj.transform.position = Vector3.MoveTowards(playerobj.transform.position, new Vector3(playerobj.transform.position.x, 2.1785f, 0), 15 * Time.deltaTime);
                playerpopcount = 0;
            }
        }
    }

    IEnumerator Playerrepop()
    {
        yield return new WaitForSeconds(1.0f);
        playercs = GameObject.Find("player").GetComponent<Player>();
    }

    IEnumerator ClearEff()
    {
        GameObject clearobj;
        yield return new WaitForSeconds(3f);
        clearobj = (GameObject)Instantiate(clear, clear.transform.position, Quaternion.Euler(0f, 0f, 0f));
        yield return new WaitForSeconds(1.2f);
        Destroy(clearobj.gameObject);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("title");
    }

    IEnumerator DeadEff()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            Instantiate(burst, burst.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
