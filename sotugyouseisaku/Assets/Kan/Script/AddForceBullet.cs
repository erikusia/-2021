using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBullet : MonoBehaviour
{
	//　カーソルに使用するテクスチャ
	[SerializeField]
	private Texture2D cursor;
	//　弾のゲームオブジェクト
	[SerializeField]
	private GameObject bulletPrefab;
	//　銃口
	[SerializeField]
	private Transform muzzle;
	//　弾を飛ばす力
	[SerializeField]
	private float bulletPower = 500f;

	void Start()
	{
		//　カーソルを自前のカーソルに変更
		Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(ray.direction);

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1f, LayerMask.GetMask("Gun")))
		{
			Cursor.visible = false;
		}
		else
		{
			Cursor.visible = true;
		}

		//　マウスの左クリックで撃つ
		if (Input.GetButtonDown("Fire1"))
		{
			Shot();
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
