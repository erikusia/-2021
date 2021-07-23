using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f; //���x
    public float jump = 2.0f; //�W�����v
    public float g = 9.8f; //�d��
    private float x, z;

    private CharacterController charaCon;//�L�����N�^�[�R���g���[���[
    private Vector3 pos = Vector3.zero; //���W

    private Animator animator;//�A�j���[�V����

    private Vector3 currentRot = Vector3.zero;//���݂̉�]����

    float rotSpeed = 3.0f;

    private Quaternion charaRot;  //�L�����N�^�[�̊p�x
    //�}�E�X�ړ��̃X�s�[�h
    [SerializeField]
    private float mouseSpeed = 2f;
    //�@�L��������]�����ǂ����H
    private bool charaRotFlag = false;
    //�@�J�����̊p�x
    private Quaternion cameraRot;


    [SerializeField]
    private bool cameraRotForward = true;
    //�@�J�����̊p�x�̏����l
    private Quaternion initCameraRot;

    //�@�L�����N�^�[���_�̃J�����ŉ�]�o������x
    [SerializeField]
    private float cameraRotateLimit = 30f;

    //�@�L�����N�^�[���_�̃J����
    private Transform myCamera;

    // Start is called before the first frame update
    void Start()
    {
        charaCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        charaRot = transform.localRotation;
        myCamera = GetComponentInChildren<Camera>().transform;	//�@�L�����N�^�[���_�̃J�����̎擾
        initCameraRot = myCamera.localRotation;
        cameraRot = myCamera.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //�@�L�����N�^�[�̌�����ύX����
        RotateChara();
        //�@���_�̌�����ς���
        RotateCamera();

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        if (charaCon.isGrounded)
        {
            pos = new Vector3(x, 0, z);
            pos = transform.TransformDirection(pos);
            pos *= speed;

            animator.SetFloat("Forward", Input.GetAxis("Vertical"));
            animator.SetFloat("Lateral", Input.GetAxis("Horizontal"));


            if (Input.GetButton("Jump"))
            {
                pos.y = jump;
            }
        }

        pos.y -= g * Time.deltaTime;
        charaCon.Move(pos * Time.deltaTime);

    }
    #region ��]������
    //protected void Rotate()
    //{
    //    // ���݂̈ړ��x�N�g�����擾
    //    Vector3 moveVelocity = charaCon.velocity;
    //    moveVelocity.y = 0;

    //    // �ړ��x�N�g������x�N�g���ȊO�̏ꍇ�͉�]�p�x�N�g���ɐݒ�
    //    if (moveVelocity != Vector3.zero)
    //    {
    //        currentRot = moveVelocity;
    //    }

    //    // �p�x�Ɖ�]�������擾
    //    float value = Mathf.Min(1, rotate_speed * Time.deltaTime / Vector3.Angle(transform.forward, currentRot));
    //    Vector3 newForward = Vector3.Slerp(transform.forward, currentRot, value);

    //    if (newForward != Vector3.zero)
    //    {
    //        transform.rotation = Quaternion.LookRotation(newForward, transform.up);
    //    }
    //}
    #endregion

    //�L�����N�^�[�̊p�x��ύX
    void RotateChara()
    {
        //���̉�]�l���v�Z
        float yRot = Input.GetAxis("Mouse X") * mouseSpeed;

        charaRot *= Quaternion.Euler(0f, yRot, 0f);

        //�L�����N�^�[����]���Ă��邩�ǂ����H

        if (yRot != 0f)
        {
            charaRotFlag = true;
        }
        else
        {
            charaRotFlag = false;
        }

        //�@�L�����N�^�[�̉�]�����s
        transform.localRotation = Quaternion.Slerp(transform.localRotation, charaRot, rotSpeed * Time.deltaTime);
    }

    //�@�J�����̊p�x��ύX
    void RotateCamera()
    {

        float xRotate = Input.GetAxis("Mouse Y") * mouseSpeed;

        //�@�}�E�X����Ɉړ��������ɏ�������������Ȃ甽�Ε����Ɋp�x�𔽓]������
        if (cameraRotForward)
        {
            xRotate *= -1;
        }
        //�@��U�p�x���v�Z����	
        cameraRot *= Quaternion.Euler(xRotate, 0f, 0f);
        //�@�J������X���̊p�x�����E�p�x�𒴂�������E�p�x�ɐݒ�
        var resultYRot = Mathf.Clamp(Mathf.DeltaAngle(initCameraRot.eulerAngles.x, cameraRot.eulerAngles.x), -cameraRotateLimit, cameraRotateLimit);
        //�@�p�x���č\�z
        cameraRot = Quaternion.Euler(resultYRot, cameraRot.eulerAngles.y, cameraRot.eulerAngles.z);
        //�@�J�����̎��_�ύX�����s
        myCamera.localRotation = Quaternion.Slerp(myCamera.localRotation, cameraRot, rotSpeed * Time.deltaTime);
    }
}
