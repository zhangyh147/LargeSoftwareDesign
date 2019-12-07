using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public List<GameObject> enemies = new List<GameObject>();
	void OnTriggerEnter(Collider col)
	{
		if(col.tag=="Enemy")
		{
			enemies.Add(col.gameObject);
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Enemy")
		{
			enemies.Remove(col.gameObject);
		}
	}

	public float attackRateTime = 1;//攻击间隔时间
	private float timer = 0;//计时器

	public GameObject bulletPrefab;//子弹
	public Transform firePosition;
	public Transform head;

	void Start()
	{
		timer = attackRateTime;
	}
	void Update()
	{
		timer += Time.deltaTime;
		if(enemies.Count>0&&timer>=attackRateTime)
		{
			timer = 0;
			Attack();
		}
	}
	void Attack()
	{
		while(enemies[0]==null)
		{
			enemies.RemoveAt(0);
			if (enemies.Count == 0)
				return;
		}
		//炮塔指向敌人
		Vector3 targetPosition = enemies[0].transform.position;
		targetPosition.y = head.position.y;
		head.LookAt(targetPosition);
		//发射子弹
		GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
		bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
	}
}
