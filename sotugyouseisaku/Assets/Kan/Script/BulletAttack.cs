using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public Vector3 speed;
    void Start()
    {
        
    }
    private void Update()
    {
        //Debug.Log(speed);
        var v = transform.position;
        v += speed;
        transform.position = v;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Target")
        {
            Destroy(gameObject);//é©ï™ÅiíeÇè¡Ç∑Åj
        }
    }
}
