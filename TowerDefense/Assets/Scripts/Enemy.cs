using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float speed = 10;//行动速度
	public float hp = 150;//当前生命值
	private float totalHp;//总生命值
	public Slider hpSlider;
	public GameObject explosionEffect;

	private Transform[] positions;
	private int index = 0;
	// Use this for initialization
	void Start () {
		positions = Waypoints.positions;
		totalHp = hp;
		hpSlider = GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move()
	{
		if (index > positions.Length - 1)
			return;
		transform.Translate((positions[index].position - transform.position).normalized*Time.deltaTime*speed);
		if(Vector3.Distance(positions[index].position,transform.position)<0.2f)
		{
			index++;
		}
		if (index > positions.Length - 1)
			ReachEnd();
	}

	void ReachEnd()
	{
		GameObject.Destroy(this.gameObject);
	}
	void OnDestroy()
	{
		EnemyCreater.countEnemyAlive--;
	}

	public void TakeDamage(float damage)
	{
		hp -= damage;
		hpSlider.value = hp / totalHp;
		if(hp<=0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(this.gameObject);
		GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
		Destroy(effect, 1.5f);
	}
}
