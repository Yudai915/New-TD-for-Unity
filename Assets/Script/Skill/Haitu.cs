using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haitu : MonoBehaviour {

    Vector3 pos;
    float rotation = 500f;
    float speed = 30f;

	void Start () {
        StartCoroutine("Destroy");
    }

    void Update () {
        transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));

        pos = transform.position;
        transform.position = new Vector3(pos.x - speed * Time.deltaTime, pos.y, pos.z);
      
        if (pos.x <= -9.5f)
        {
            speed *= -1;
            transform.position = new Vector3(-9.4f, pos.y, pos.z);
        }
        if(pos.x >= 9.0f)
        {
            speed *= -1;
            transform.position = new Vector3(8.9f, pos.y, pos.z);

        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
