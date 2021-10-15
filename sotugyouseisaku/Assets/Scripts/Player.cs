using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Player : MonoBehaviour
{

    public float speed = 5.0f; //速度
    public float jump = 2.0f; //ジャンプ
    public float g = 9.8f; //重力
    private float moveS = 2.0f;
    private float x, z;
    private float L2;

    private CharacterController charaCon;//キャラクターコントローラー
    private Vector3 pos = Vector3.zero; //座標

    private Animator animator;//アニメーション

    private Vector3 currentRot = Vector3.zero;//現在の回転方向

    private float rotSpeed = 3.0f;

    private Quaternion charaRot;  //キャラクターの角度
    //マウス移動のスピード
    [SerializeField]
    private float mouseSpeed = 2.0f;

    //　キャラが回転中かどうか？
    private bool charaRotFlag;
    //　カメラの角度
    private Quaternion cameraRot;


    [SerializeField]
    private bool cameraRotForward = true;
    //　カメラの角度の初期値
    private Quaternion initCameraRot;

    //　キャラクター視点のカメラで回転出来る限度
    [SerializeField]
    private float cameraRotateLimit = 30f;

    [SerializeField]
    private Transform spine;

    //　キャラクター視点のカメラ
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
        myCamera = GetComponentInChildren<Camera>().transform;	//　キャラクター視点のカメラの取得
        initCameraRot = myCamera.localRotation;
        cameraRot = myCamera.localRotation;

        defaultFov = GetComponentInChildren<Camera>().fieldOfView;

        charaRotFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //　キャラクターの向きを変更する
        RotateChara();
        //　視点の向きを変える
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

        //ズーム
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
        //　腰のボーンの角度をカメラの向きにする
        spine.rotation = Quaternion.Euler(spine.eulerAngles.x, spine.eulerAngles.y, spine.eulerAngles.z + (-myCamera.localEulerAngles.x));
    }

    void LateUpdate()
    {
        //　ボーンをカメラの角度を向かせる
        RotateBone();
    }

    //キャラクターの角度を変更
    void RotateChara()
    {
        //横の回転値を計算
        float yRot = Input.GetAxis("Horizontal2")*0.1f;

        charaRot *= Quaternion.Euler(0f, yRot, 0f);

        //キャラクターが回転しているかどうか？

        if (yRot != 0f)
        {
            charaRotFlag = true;
        }
        else
        {
            charaRotFlag = false;
        }

        //　キャラクターの回転を実行
        transform.localRotation = Quaternion.Slerp(transform.localRotation, charaRot, rotSpeed);
    }

    //　カメラの角度を変更
    void RotateCamera()
    {

        float xRotate = Input.GetAxis("Vertical2")*0.1f;

        //　マウスを上に移動した時に上を向かせたいなら反対方向に角度を反転させる
        if (cameraRotForward)
        {
            xRotate *= -1;
        }
        //　一旦角度を計算する	
        cameraRot *= Quaternion.Euler(xRotate, 0f, 0f);
        //　カメラのX軸の角度が限界角度を超えたら限界角度に設定
        var resultYRot = Mathf.Clamp(Mathf.DeltaAngle(initCameraRot.eulerAngles.x, cameraRot.eulerAngles.x), -cameraRotateLimit, cameraRotateLimit);
        //　角度を再構築
        cameraRot = Quaternion.Euler(resultYRot, cameraRot.eulerAngles.y, cameraRot.eulerAngles.z);
        //　カメラの視点変更を実行
        myCamera.localRotation = Quaternion.Slerp(myCamera.localRotation, cameraRot, rotSpeed);
    }
}


