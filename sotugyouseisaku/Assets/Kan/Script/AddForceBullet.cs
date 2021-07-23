using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBullet : MonoBehaviour
{
	//�@�J�[�\���Ɏg�p����e�N�X�`��
	[SerializeField]
	private Texture2D cursor;
	//�@�e�̃Q�[���I�u�W�F�N�g
	[SerializeField]
	private GameObject bulletPrefab;
	//�@�e��
	[SerializeField]
	private Transform muzzle;
	//�@�e���΂���
	[SerializeField]
	private float bulletPower = 500f;

	void Start()
	{
		//�@�J�[�\�������O�̃J�[�\���ɕύX
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

		//�@�}�E�X�̍��N���b�N�Ō���
		if (Input.GetButtonDown("Fire1"))
		{
			Shot();
		}
	}

	//�@�G������
	void Shot()
	{
		var bulletInstance = Instantiate<GameObject>(bulletPrefab, muzzle.position, muzzle.rotation);
		bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
		Destroy(bulletInstance, 5f);
	}
}
