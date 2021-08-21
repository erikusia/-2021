using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    // ����n�_�I�u�W�F�N�g���i�[����z��
    public Transform[] points;
    // ����n�_�̃I�u�W�F�N�g���i�����l=0�j
    private int destPoint = 0;
    // NavMesh Agent �R���|�[�l���g���i�[����ϐ�
    private NavMeshAgent agent;

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
    }
}
