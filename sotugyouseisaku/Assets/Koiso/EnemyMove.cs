using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //巡回地点オブジェクトを格納する配列
    public Transform[] points;
    //巡回地点のオブジェクト数（初期値=0）
    private int destPoint = 0;
    //NavMesh Agent コンポーネントを格納する変数
    private NavMeshAgent agent;

    //プレイヤーの座標
    [SerializeField] private Transform Target;
    private Vector3 TargetPos;
    //発見距離
    [SerializeField] int Distance = 50;
    //Rayが当たったオブジェクトの情報を入れる箱
    RaycastHit hit;

    // ゲームスタート時の処理
    void Start()
    {
        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
        // 巡回地点間の移動を継続させるために自動ブレーキを無効化
        //（エージェントは目的地点に近づいても減速しない)
        agent.autoBraking = false;
        // 次の巡回地点の処理を実行
        GotoNextPoint();
    }

    // 次の巡回地点を設定する処理
    void GotoNextPoint()
    {
        // 巡回地点が設定されていなければ
        if (points.Length == 0)
            // 処理を返します
            return;
        // 現在選択されている配列の座標を巡回地点の座標に代入
        agent.destination = points[destPoint].position;
        // 配列の中から次の巡回地点を選択（必要に応じて繰り返し）
        destPoint = (destPoint + 1) % points.Length;
    }

    // ゲーム実行中の繰り返し処理
    void Update()
    {
        // エージェントが現在の巡回地点に到達したら
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            // 次の巡回地点を設定する処理を実行
            GotoNextPoint();

        //向きをプレイヤーに変える
        //transform.rotation =
        //Quaternion.LookRotation(Target.position - transform.position);

        //ターゲット座標 + 位置調整
        TargetPos = (Target.position - transform.position) + new Vector3(0, 1, 0);
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
                //プレイヤーを追いかける
                agent.destination = Target.position;
            }
            //発見していない場合
            else
            {
                
            }
        }

        Debug.Log("巡回地点は"+points[destPoint]);
    }
}
