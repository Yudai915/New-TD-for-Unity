using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Haitugage : MonoBehaviour {

    Player playercs;
    public Image UIobj;
    public GameObject Eff;

    void Start()
    {
        playercs = GameObject.Find("player").GetComponent<Player>();
    }

    void Update()
    {
        UIobj.fillAmount = playercs.haitucount / playercs.haituDefaultTime;
        if (playercs.haitucount <= 0 && playercs.haitucount >= -10f)
        {
            Instantiate(Eff, new Vector3(-2.7f, 4.0f, 0), Quaternion.Euler(0f, 0f, 0f));
            playercs.haitucount = -15f;
        }
    }
}
