using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	private List<GameObject> enemies = new List<GameObject>();
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

	//激光攻击
	public bool useLaser = false;
	public float laserDamageRate = 60;
	public LineRenderer laserRenderer;
	public GameObject laserEffect;

	void Start()
	{
		timer = attackRateTime;
	}
	void Update()
	{
		//炮塔指向敌人
		if (enemies.Count > 0 && enemies[0] != null)
		{
			Vector3 targetPosition = enemies[0].transform.position;
			targetPosition.y = head.position.y;
			head.LookAt(targetPosition);
		}
		if (useLaser == false)//子弹攻击
		{
			timer += Time.deltaTime;
			if (enemies.Count > 0 && timer >= attackRateTime)
			{
				timer = 0;
				Attack();
			}
		}
		else
		{
			if(enemies.Count>0)
			{
				LaserAttack();
			}
			else
			{
				laserRenderer.enabled = false;
				laserEffect.SetActive(false);
			}
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
		//发射子弹
		GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
		bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
	}

	void LaserAttack()
	{
		while (enemies[0] == null)
		{
			enemies.RemoveAt(0);
			if (enemies.Count == 0)
			{
				return;
			}
		}
		if (laserRenderer.enabled == false)
			laserRenderer.enabled = true;
		laserEffect.SetActive(true);
		laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemies[0].transform.position });
		enemies[0].GetComponent<Enemy>().TakeDamage(laserDamageRate*Time.deltaTime);
		laserEffect.transform.position = enemies[0].transform.position;
		Vector3 pos = transform.position;
		pos.y = enemies[0].transform.position.y;
		laserEffect.transform.LookAt(pos);
	}
}
