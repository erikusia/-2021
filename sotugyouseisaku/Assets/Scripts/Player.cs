using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5.0f; //速度
    public float jump = 5.0f; //ジャンプ
    public float g = 9.8f; //重力
    private float x, z;

    private CharacterController charaCon;
    private Vector3 pos = Vector3.zero; //座標

    private Animator animator;

    private const string key_isRun = "isRun";

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

            if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                this.animator.SetBool(key_isRun, true);
            }
            else
            {
                this.animator.SetBool(key_isRun, false);
            }

            if (Input.GetButton("Jump"))
            {
                pos.y = jump;
            }
        }

        pos.y -= g * Time.deltaTime;
        charaCon.Move(pos * Time.deltaTime);

    }
}
