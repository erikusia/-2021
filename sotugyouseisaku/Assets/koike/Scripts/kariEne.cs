using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariEne : MonoBehaviour
{
    public float EneHp;
    float hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = EneHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Bullet")
    //    {
    //        hp = hp - 10;
    //        Debug.Log("“GHP : " + hp);
    //    }
    //}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            hp = hp - 10;
            Debug.Log("“GHP : " + hp);
        }
    }
}
