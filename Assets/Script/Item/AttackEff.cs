using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEff : MonoBehaviour
{

    // 回転速度
    float rotation = 1000;

    void Start()
    {
        StartCoroutine("Destroy");
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
