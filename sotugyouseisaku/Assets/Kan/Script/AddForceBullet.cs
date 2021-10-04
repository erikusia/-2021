using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBullet : MonoBehaviour
{
    //　弾のゲームオブジェクト
    [SerializeField]
    private GameObject bulletPrefab;
    //　銃口
    [SerializeField]
    private Transform muzzle;
    //　弾を飛ばす力
    [SerializeField]
    private float bulletPower = 500f;
    //　弾数
    [SerializeField]
    private float maxBullets = 30f;
    public float bulletCount = 30f;

    private Vector3 rayCameraPos;
    private int count = 0;
    [SerializeField]
    private int rate = 20;

    //リロード時間
    public float reloadTime = 1.5f;
    public float rlTime = 0.0f;
    public bool isReload = false;
    void Start()
    {
        rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
        bulletCount = maxBullets;
    }

    void Update()
    {

#if UNITY_EDITOR
        rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
#endif

        Ray ray = Camera.main.ScreenPointToRay(rayCameraPos);

        //メインカメラからレイを出すデバッグ線
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        transform.rotation = Quaternion.LookRotation(ray.direction);
        RaycastHit hit;
        int layerMask = ~(1 << 6);
        if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
        {
            Vector3 distanc = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(distanc);
        }

        if (isReload)
        {
            if (rlTime >= reloadTime)
            {
                rlTime = 0;
                bulletCount = maxBullets;
                isReload = false;
            }
            rlTime += Time.deltaTime;
        }

        //リロード
        if (Input.GetButton("joystick X"))
        {
            isReload = true;
        }

        if (bulletCount <= 0 || isReload)
        {
            return;
        }

        //　マウスの左クリックで撃つ
        if (Input.GetButton("joystick R1"))
        {
            if (count % rate == 0)
            {
                Shot();
                bulletCount--;
            }
            count += 1;
        }
    }

    //　敵を撃つ
    void Shot()
    {
        var bulletInstance = Instantiate<GameObject>(bulletPrefab, muzzle.position, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
        Destroy(bulletInstance, 5f);
    }
}
