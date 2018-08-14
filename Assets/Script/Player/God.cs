using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {
    private Chara characs;

    // 受けたダメージ判定用
    public bool fromSt = false;
    public bool fromSp = false;
    public bool fromDef = false;
    public bool fromBom = false;
    public bool fromBoss = false;
    // 点滅判定
    bool flashTF = false;

    void Start () {
        characs = GameObject.Find("Chara").GetComponent<Chara>();
	}
	
	void Update () {
        // 非ダメージ時点滅処理
        if (flashTF)
        {
            StartCoroutine("Flashing");
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "PlayerD_fromSt")
        {
            fromSt = true;
        }
        if (collider2D.gameObject.tag == "PlayerD_fromSp")
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
        if (collider2D.gameObject.tag == "PlayerD_fromBoss")
        {
            fromBoss = true;
        }
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "PlayerD_fromSt" || collider2D.gameObject.tag == "PlayerD_fromSp"
            ||collider2D.gameObject.tag == "PlayerD_fromDef" || collider2D.gameObject.tag == "PlayerD_fromBom")
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
            gameObject.GetComponent<SpriteRenderer>().color = new Color(35f / 255f, 0, 1f, 116f / 255f);
            yield return new WaitForSeconds(0.05f);
        }
        flashTF = false;
    }
}
