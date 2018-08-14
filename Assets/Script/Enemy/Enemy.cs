using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 出現させる敵を入れておく
    public GameObject[] enemys;
    // 子として保管
    private GameObject ChildEnemy;
    // 次に敵が出現するまでのおおよその時間
    [SerializeField] float appearNextTime;
    // 今何人の敵を出現させたか
    private int numberOfEnemys;
    // 待ち時間計測フィールド
    private float elapsedTime;
    // 出現する場所
    public GameObject[] spot;


    void Start()
    {
        numberOfEnemys = 0;
        elapsedTime = 0f;
    }

    void Update()
    {
        //　経過時間を足す
        elapsedTime += Time.deltaTime;
        //　経過時間が経ったら
        if (elapsedTime > appearNextTime)
        {
            elapsedTime = 0f;
            // 次に出現する時間をランダムで決定
            appearNextTime = Random.Range(3.0f, 7.0f);
            AppearEnemy();
        }
    }

    //　敵出現メソッド
    void AppearEnemy()
    {
        // 出現させる敵をランダムで決定
        var randomValue = Random.Range(0, enemys.Length);
        // 出現する場所をランダムに決定
        var randomspot = Random.Range(0, spot.Length);
        ChildEnemy = (GameObject)Instantiate(enemys[randomValue], spot[randomspot].transform.position, Quaternion.Euler(0f, 0f, 0f));
        ChildEnemy.transform.parent = this.transform;
        numberOfEnemys++;
        elapsedTime = 0f;
    }
}
