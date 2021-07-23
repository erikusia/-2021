using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Target")
        {
            Destroy(gameObject);//自分（弾を消す）
        }
    }
}
