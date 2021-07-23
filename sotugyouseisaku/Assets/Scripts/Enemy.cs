using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        WALK,
        RUN,
        ATTACK,
        JUMP,
    };

    private Animator animator;
    private const string key_work = "work";

    public float moveSpeed = 0.2f;
    public float rotateSpeed = 1;

    public EnemyState type;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.type = EnemyState.IDLE;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1 * rotateSpeed, 0);
            this.animator.SetBool(key_work, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1 * rotateSpeed, 0, 0);
            this.animator.SetBool(key_work, false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed;
            this.animator.SetBool(key_work, true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * moveSpeed * -1;
            this.animator.SetBool(key_work, true);
        }
    }
}
