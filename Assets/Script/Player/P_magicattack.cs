using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_magicattack : MonoBehaviour {

    Vector3 pos;
    float speed = 5f;

	void Start () {
		
	}
	
	void Update () {
        pos = transform.position;
        transform.position = new Vector3(pos.x - speed * Time.deltaTime, pos.y, pos.z);

        if(transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
