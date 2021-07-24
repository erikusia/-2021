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

	private Vector3 rayCameraPos;
	private int count = 0;
	[SerializeField]
	private int rate = 20;
	void Start()
	{
		rayCameraPos = new Vector3(Screen.width / 2, Screen.height / 2, 0.1f);
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
		

		//　マウスの左クリックで撃つ
		if (Input.GetButton("Fire1"))
		{
			if (count % rate == 0)
			{
				Shot();
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
