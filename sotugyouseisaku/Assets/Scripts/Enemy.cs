using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

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

    private Vector3 targetpos;
    private Animator animator;
    private const string key_work = "work";

    [SerializeField] EnemyState type;
    [SerializeField] float MaxHP=100;
    float HP;
    [SerializeField] float BulletDamage = 20;

    [SerializeField] GameObject target;

    [SerializeField] float angle = 50;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.type = EnemyState.IDLE;
        HP = MaxHP;
        targetpos = transform.position;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag=="Bullet")
        {
            HP -= BulletDamage;
            Debug.Log("É_ÉÅÅ[ÉWéÛÇØÇΩ");
            if(HP<=0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        this.animator.SetBool("walk", true);

        transform.RotateAround(
            target.transform.position,
            Vector3.up,
            angle * Time.deltaTime
            );
    }
}
