using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB : MonoBehaviour {
    GameObject mainCamera;
    GameObject Button;
    Camera main;

    Player player;
    Chara chara;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        Button = GameObject.Find("MB");
        player = GameObject.Find("player").GetComponent<Player>();
        chara = GameObject.Find("chara").GetComponent<Chara>();
    }
    void Update()
    {
        //カメラを取得
        main = mainCamera.GetComponent<Camera>();
        Vector3 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(mousePos);
        //タップ確認
        if (Input.GetMouseButtonDown(0))
        {
            if (col == Button.GetComponent<Collider2D>())
            {
                //タップされた時の処理
                player.MagicAttack();
            }
        }
    }
}
