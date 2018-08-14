using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodHPbar : MonoBehaviour {
    // インスペクター上からアタッチ
    [SerializeField] Slider slider;
    [SerializeField] Chara chara;

    float count = 0;

    void Start()
    {
        // 初期化の処理などが必要であれば追加してください。
        // slider = GameObject.Find("Slider").GetComponent<Slider>();
        chara = GameObject.Find("Chara").GetComponent<Chara>();
        StartCoroutine("PlusHP");
    }


    void Update()
    {
        count += Time.deltaTime;
        if (count > 5.0f)
        {
            slider.value = chara.GodHP;
        }
    }

    IEnumerator PlusHP()
    {
        while (slider.value < chara.MAXGodHP)
        {
            yield return new WaitForSeconds(0.005f);
            slider.value += 812;
        }
    }
}
