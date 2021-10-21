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

    //�U���̔���
    [SerializeField] private GameObject attack;

    //����n�_�I�u�W�F�N�g���i�[����z��
    [SerializeField] public Transform[] points;
    //����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    //NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent agent;

    //�v���C���[�̍��W
    [SerializeField] private Transform player;
    private Vector3 playerPos;
    //��������
    [SerializeField] int chaseDistance = 50;
    //�U������
    [SerializeField] int attackDistance = 5;
    //Ray�����������I�u�W�F�N�g�̏������锠
    RaycastHit hit;

    bool isChase = true;
    bool isAttack = true;
    bool isLook = false;

    // �Q�[���X�^�[�g���̏���
    void Start()
    {
        attack.SetActive(false);
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        // ���̏���n�_�̏��������s
        GotoNextPoint();

        agent.speed = speed;

    }

    // ���̏���n�_��ݒ肷�鏈��
    void GotoNextPoint()
    {
        // ����n�_���ݒ肳��Ă��Ȃ��ꍇ
        if (points.Length == 0) return;
        // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
        agent.destination = points[destPoint].position;
        // �z��̒����玟�̏���n�_��I���i�K�v�ɉ����ČJ��Ԃ��j
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Damage();
        }
    }

    // �Q�[�����s���̌J��Ԃ�����
    void Update()
    {
        //�������v���C���[�ɕς���
        //transform.rotation =
        //Quaternion.LookRotation(player.position - transform.position);

        //�^�[�Q�b�g���W + �ʒu����
        playerPos = (player.position - transform.position) + new Vector3(0, 1, 0);
        //ray�̐���
        Ray ray = new Ray(transform.position, playerPos);
        //�f�o�b�O�p
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        ////�A�j���[�V����
        //switch(EnemyState)
        //{
        //    case EnemyState.PATROL:
        //        break;
        //    case EnemyState.CHASE:
        //        break;
        //    case EnemyState.DAMAGE:
        //        break;
        //    case EnemyState.ATTACK:
        //        break;
        //}

        if (Physics.Raycast(ray, out hit, chaseDistance))
        {
            //�v���C���[�^�O�ɓ������Ă�����
            if (hit.collider.tag == "Player")
            {
                if (Physics.Raycast(ray, out hit, attackDistance))
                {
                    if (isAttack)
                    {
                        StartCoroutine("Attacktimer", 1);
                        isAttack = false;
                    }
                }
                PlayerChase();
            }
            else Patrol();
        }
        else Patrol();

        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage();
        }
    }

    void PlayerChase()
    {
        state = EnemyState.CHASE;
        //�v���C���[��ǂ�������
        agent.destination = player.position;
    }

    void Patrol()
    {
        state = EnemyState.PATROL;
        // �G�[�W�F���g�����݂̏���n�_�ɓ��B������
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            // ���̏���n�_��ݒ肷�鏈�������s
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

    //�U���N�[���_�E��
    IEnumerator Attacktimer(int time)
    {
        state = EnemyState.ATTACK;
        attack.SetActive(true);
        Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(1f);
            Debug.Log(time);
            --time;
            attack.SetActive(false);
        }
        mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        agent.isStopped = false;
        isAttack = true;
    }

    //��Ԃ�\���_��
    IEnumerator Colortimer(int time)
    {
        Material mat = this.GetComponent<Renderer>().material;
        while (time >= 0)
        {
            mat.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(0.1f);
            --time;
        }
        mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
