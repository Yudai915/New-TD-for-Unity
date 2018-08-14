using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEff : MonoBehaviour {

    GameObject player;
    Vector3 pos;

	void Start () {
        player = GameObject.Find("player");
        StartCoroutine("Delete");
    }

    void Update()
    {
        pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y - 0.9f, pos.z);
    }
	
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
