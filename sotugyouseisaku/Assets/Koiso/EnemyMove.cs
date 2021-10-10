using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //����n�_�I�u�W�F�N�g���i�[����z��
    public Transform[] points;
    //����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    //NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent agent;

    //�v���C���[�̍��W
    [SerializeField] private Transform Target;
    private Vector3 TargetPos;
    //��������
    [SerializeField] int Distance = 50;
    //Ray�����������I�u�W�F�N�g�̏������锠
    RaycastHit hit;

    // �Q�[���X�^�[�g���̏���
    void Start()
    {
        // �ϐ�"agent"�� NavMesh Agent �R���|�[�l���g���i�[
        agent = GetComponent<NavMeshAgent>();
        // ����n�_�Ԃ̈ړ����p�������邽�߂Ɏ����u���[�L�𖳌���
        //�i�G�[�W�F���g�͖ړI�n�_�ɋ߂Â��Ă��������Ȃ�)
        agent.autoBraking = false;
        // ���̏���n�_�̏��������s
        GotoNextPoint();
    }

    // ���̏���n�_��ݒ肷�鏈��
    void GotoNextPoint()
    {
        // ����n�_���ݒ肳��Ă��Ȃ����
        if (points.Length == 0)
            // ������Ԃ��܂�
            return;
        // ���ݑI������Ă���z��̍��W������n�_�̍��W�ɑ��
        agent.destination = points[destPoint].position;
        // �z��̒����玟�̏���n�_��I���i�K�v�ɉ����ČJ��Ԃ��j
        destPoint = (destPoint + 1) % points.Length;
    }

    // �Q�[�����s���̌J��Ԃ�����
    void Update()
    {
        // �G�[�W�F���g�����݂̏���n�_�ɓ��B������
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            // ���̏���n�_��ݒ肷�鏈�������s
            GotoNextPoint();

        //�������v���C���[�ɕς���
        //transform.rotation =
        //Quaternion.LookRotation(Target.position - transform.position);

        //�^�[�Q�b�g���W + �ʒu����
        TargetPos = (Target.position - transform.position) + new Vector3(0, 1, 0);
        //ray�̐���
        Ray ray = new Ray(transform.position, TargetPos);
        //�f�o�b�O�p
        Debug.DrawLine(ray.origin, hit.point, Color.red);
        //�����蔻�� 
        if (Physics.Raycast(ray, out hit, Distance))
        {
            //�v���C���[�^�O�ɓ������Ă�����
            if (hit.collider.tag == "Player")
            {
                Debug.Log("Ray��Player�ɓ�������");
                //�v���C���[��ǂ�������
                agent.destination = Target.position;
            }
            //�������Ă��Ȃ��ꍇ
            else
            {
                
            }
        }

        Debug.Log("����n�_��"+points[destPoint]);
    }
}
