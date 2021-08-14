using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f; //速度
    public float jump = 2.0f; //ジャンプ
    public float g = 9.8f; //重力
    private float x, z;

    private CharacterController charaCon;//キャラクターコントローラー
    private Vector3 pos = Vector3.zero; //座標

    private Animator animator;//アニメーション

    private Vector3 currentRot = Vector3.zero;//現在の回転方向

    public float rotSpeed = 3.0f;

    private Quaternion charaRot;  //キャラクターの角度
    //マウス移動のスピード
    [SerializeField]
    private float mouseSpeed = 2.0f;
    //　キャラが回転中かどうか？
    private bool charaRotFlag = false;
    //　カメラの角度
    private Quaternion cameraRot;


    [SerializeField]
    private bool cameraRotForward = true;
    //　カメラの角度の初期値
    private Quaternion initCameraRot;

    //　キャラクター視点のカメラで回転出来る限度
    [SerializeField]
    private float cameraRotateLimit = 30f;

    //　キャラクター視点のカメラ
    private Transform myCamera;


    // Start is called before the first frame update
    void Start()
    {
        charaCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        charaRot = transform.localRotation;
        myCamera = GetComponentInChildren<Camera>().transform;	//　キャラクター視点のカメラの取得
        initCameraRot = myCamera.localRotation;
        cameraRot = myCamera.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //　キャラクターの向きを変更する
        RotateChara();
        //　視点の向きを変える
        RotateCamera();

        x = Input.GetAxis("Horizontal")*2.0f;
        z = Input.GetAxis("Vertical")*2.0f;
        
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
    #region 回転お試し
    //protected void Rotate()
    //{
    //    // 現在の移動ベクトルを取得
    //    Vector3 moveVelocity = charaCon.velocity;
    //    moveVelocity.y = 0;

    //    // 移動ベクトルが零ベクトル以外の場合は回転用ベクトルに設定
    //    if (moveVelocity != Vector3.zero)
    //    {
    //        currentRot = moveVelocity;
    //    }

    //    // 角度と回転方向を取得
    //    float value = Mathf.Min(1, rotate_speed * Time.deltaTime / Vector3.Angle(transform.forward, currentRot));
    //    Vector3 newForward = Vector3.Slerp(transform.forward, currentRot, value);

    //    if (newForward != Vector3.zero)
    //    {
    //        transform.rotation = Quaternion.LookRotation(newForward, transform.up);
    //    }
    //}
    #endregion

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
