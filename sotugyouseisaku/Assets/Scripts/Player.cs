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

    float rotate_speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        charaCon = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        if (charaCon.isGrounded)
        {
            pos = new Vector3(x, 0, z);
            pos = transform.TransformDirection(pos);
            pos *= speed;

            animator.SetFloat("Forward", Input.GetAxis("Vertical"));
            animator.SetFloat("Lateral", Input.GetAxis("Horizontal"));

            //if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            //{
            //    this.animator.SetBool(key_isRun, true);
            //}
            //else
            //{
            //    this.animator.SetBool(key_isRun, false);
            //}

            if (Input.GetButton("Jump"))
            {
                pos.y = jump;
            }
        }

        pos.y -= g * Time.deltaTime;
        charaCon.Move(pos * Time.deltaTime);

    }

    protected void Rotate()
    {
        // 現在の移動ベクトルを取得
        Vector3 moveVelocity = charaCon.velocity;
        moveVelocity.y = 0;

        // 移動ベクトルが零ベクトル以外の場合は回転用ベクトルに設定
        if (moveVelocity != Vector3.zero)
        {
            currentRot = moveVelocity;
        }

        // 角度と回転方向を取得
        float value = Mathf.Min(1, rotate_speed * Time.deltaTime / Vector3.Angle(transform.forward, currentRot));
        Vector3 newForward = Vector3.Slerp(transform.forward, currentRot, value);

        if (newForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(newForward, transform.up);
        }
    }
}
