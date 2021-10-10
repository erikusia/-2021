using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    //プレイヤーの座標
    [SerializeField] private Transform Target;
    private Vector3 TargetPos;

    [SerializeField] int Distance = 50;

    //Rayが当たったオブジェクトの情報を入れる箱
    RaycastHit hit;

    void Start()
    {
        
    }

    void Update()
    {
        //向きをプレイヤーに変える
        //transform.rotation =
        //Quaternion.LookRotation(Target.position - transform.position);

        //ターゲット座標 + 位置調整
        TargetPos = (Target.position - transform.position) + new Vector3 (0,1,0);

        //rayの生成
        Ray ray = new Ray(transform.position, TargetPos);

        //デバッグ用
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        //当たり判定 
        if (Physics.Raycast(ray, out hit, Distance))
        {

            //プレイヤータグに当たっていたら
            if (hit.collider.tag == "Player")
            {

                Debug.Log("RayがPlayerに当たった");

            }
        }

        //距離を測る
        //public GameObject cubeA;
        //public GameObject cubeB;

        //void Start()
        //{
        //    Vector3 posA = cubeA.transform.position;
        //    Vector3 posB = cubeB.transform.position;
        //    float dis = Vector3.Distance(posA, posB);
        //    Debug.Log("距離 : " + dis);
        //}
    }
}
