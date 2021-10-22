using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderEnemyMove : MonoBehaviour
{
    enum EnemyState
    {
        PATROL,
        CHASE,
        DAMAGE,
        ATTACK
    }

    [SerializeField] EnemyState state = EnemyState.PATROL;
    [SerializeField] int hp = 100;
    [SerializeField] int speed = 15;

    //攻撃の判定
    [SerializeField] private GameObject attack;

    //巡回地点オブジェクトを格納する配列
    [SerializeField] public Transform[] points;
    //巡回地点のオブジェクト数（初期値=0）
    private int destPoint = 0;
    //NavMesh Agent コンポーネントを格納する変数
    private NavMeshAgent agent;

    private Animator animator;

    //プレイヤーの座標
    [SerializeField] private Transform player;
    private Vector3 playerPos;
    //発見距離
    [SerializeField] int chaseDistance = 50;
    //攻撃距離
    [SerializeField] int attackDistance = 5;
    //Rayが当たったオブジェクトの情報を入れる箱
    RaycastHit hit;

    bool isChase = true;
    bool isAttack = false;
    bool isAttackCoolTime = false;
    bool isLook = false;

    bool isCeiling = true;

    // ゲームスタート時の処理
    void Start()
    {
        attack.SetActive(false);
        // 変数"agent"に NavMesh Agent コンポーネントを格納
        agent = GetComponent<NavMeshAgent>();
        // 次の巡回地点の処理を実行
        GotoNextPoint();

        agent.speed = speed;
        animator = GetComponent<Animator>();
    }

    // 次の巡回地点を設定する処理
    void GotoNextPoint()
    {
        // 巡回地点が設定されていない場合
        if (points.Length == 0) return;
        // 現在選択されている配列の座標を巡回地点の座標に代入
        agent.destination = points[destPoint].position;
        // 配列の中から次の巡回地点を選択（必要に応じて繰り返し）
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Damage();
        }
    }

    void PlayerChase()
    {
        state = EnemyState.CHASE;
        animator.SetTrigger("chase");
        //プレイヤーを追いかける
        agent.destination = player.position;
    }

    void Patrol()
    {
        state = EnemyState.PATROL;
        animator.SetTrigger("patrol");
        // エージェントが現在の巡回地点に到達したら
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            // 次の巡回地点を設定する処理を実行
            GotoNextPoint();
    }

    void Damage()
    {
        state = EnemyState.DAMAGE;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            hp -= 5;
        }
        StartCoroutine("Colortimer", 0.1f);
    }

    //攻撃クールダウン
    IEnumerator Attacktimer(int time)
    {
        state = EnemyState.ATTACK;
        animator.SetTrigger("attack");
        attack.SetActive(true);
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            //mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(1f);
            Debug.Log(time);
            --time;
            isAttack = true;
            attack.SetActive(false);
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        agent.isStopped = false;
        isAttackCoolTime = false;
        isAttack = false;
    }

    //状態を表す点滅
    IEnumerator Colortimer(int time)
    {
        //Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            //mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(0.1f);
            --time;
        }
        //mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // ゲーム実行中の繰り返し処理
    void Update()
    {
        //向きをプレイヤーに変える
        //transform.rotation =Quaternion.LookRotation(player.position - transform.position);

        //ターゲット座標 + 位置調整
        playerPos = (player.position - transform.position) + new Vector3(0, 1, 0);
        //rayの生成
        Ray ray = new Ray(transform.position, playerPos);
        //デバッグ用
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        if (Physics.Raycast(ray, out hit, chaseDistance))
        {
            //プレイヤータグに当たっていたら
            if (hit.collider.tag == "Player")
            {
                if (Physics.Raycast(ray, out hit, attackDistance))
                {
                    if (isAttackCoolTime)
                    {
                        StartCoroutine("Attacktimer", 1);
                        isAttackCoolTime = true;
                    }
                }
                PlayerChase();
            }
            else Patrol();
        }
        else Patrol();

        if (isAttack)
        {
        }

        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage();
        }
    }
}
