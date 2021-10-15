using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Player : MonoBehaviour
{

    public float speed = 5.0f; //���x
    public float jump = 2.0f; //�W�����v
    public float g = 9.8f; //�d��
    private float moveS = 2.0f;
    private float x, z;
    private float L2;

    private CharacterController charaCon;//�L�����N�^�[�R���g���[���[
    private Vector3 pos = Vector3.zero; //���W

    private Animator animator;//�A�j���[�V����

    private Vector3 currentRot = Vector3.zero;//���݂̉�]����

    private float rotSpeed = 3.0f;

    private Quaternion charaRot;  //�L�����N�^�[�̊p�x
    //�}�E�X�ړ��̃X�s�[�h
    [SerializeField]
    private float mouseSpeed = 2.0f;

    //�@�L��������]�����ǂ����H
    private bool charaRotFlag;
    //�@�J�����̊p�x
    private Quaternion cameraRot;


    [SerializeField]
    private bool cameraRotForward = true;
    //�@�J�����̊p�x�̏����l
    private Quaternion initCameraRot;

    //�@�L�����N�^�[���_�̃J�����ŉ�]�o������x
    [SerializeField]
    private float cameraRotateLimit = 30f;

    [SerializeField]
    private Transform spine;

    //�@�L�����N�^�[���_�̃J����
    private Transform myCamera;


    float defaultFov;
    float zoom = 2.0f;
    float waitTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        charaCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        charaRot = transform.localRotation;
        myCamera = GetComponentInChildren<Camera>().transform;	//�@�L�����N�^�[���_�̃J�����̎擾
        initCameraRot = myCamera.localRotation;
        cameraRot = myCamera.localRotation;

        defaultFov = GetComponentInChildren<Camera>().fieldOfView;

        charaRotFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�@�L�����N�^�[�̌�����ύX����
        RotateChara();
        //�@���_�̌�����ς���
        RotateCamera();

        x = Input.GetAxis("Horizontal")*moveS;
        z = Input.GetAxis("Vertical")*moveS;
        
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

        //�Y�[��
        if (Input.GetButton("joystick L1"))
        {
            rotSpeed = 2.0f;
            moveS = 1.5f;
            System.Console.WriteLine("L2");
            DOTween.To(() => Camera.main.fieldOfView,
                fov => Camera.main.fieldOfView = fov,
                defaultFov / zoom, 
                waitTime);
        }
        else
        {
            rotSpeed = 3.0f;
            moveS = 2.0f;
            DOTween.To(() => Camera.main.fieldOfView,
                fov => Camera.main.fieldOfView = fov,
                defaultFov / 1,
                waitTime);
        }
    }

    void RotateBone()
    {
        //�@���̃{�[���̊p�x���J�����̌����ɂ���
        spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spine.eulerAngles.z + (-myCamera.localEulerAngles.x));
    }

    void LateUpdate()
    {
        //�@�{�[�����J�����̊p�x����������
        RotateBone();
    }

    //�L�����N�^�[�̊p�x��ύX
    void RotateChara()
    {
        //���̉�]�l���v�Z
        float yRot = Input.GetAxis("Horizontal2")*0.1f;

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
        transform.localRotation = Quaternion.Slerp(transform.localRotation, charaRot, rotSpeed);
    }

    //�@�J�����̊p�x��ύX
    void RotateCamera()
    {

        float xRotate = Input.GetAxis("Vertical2")*0.1f;

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
        myCamera.localRotation = Quaternion.Slerp(myCamera.localRotation, cameraRot, rotSpeed);
    }
}


