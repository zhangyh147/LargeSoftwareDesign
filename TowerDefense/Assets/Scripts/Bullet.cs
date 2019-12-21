using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float damage;
	public float speed;
	public GameObject explosionEffectPrefab;
	private Transform target;

	public void SetTarget(Transform _target)
	{
		this.target = _target;
	}

	void Update()
	{
		if(target==null)
		{
			Die();
			return;
		}
		transform.LookAt(target.position);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag=="Enemy")
		{
			col.GetComponent<Enemy>().TakeDamage(damage);//敌人掉血
			Die();
		}
	}

	void Die()
	{
		GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);//爆炸特效
		Destroy(effect, 1);
		Destroy(this.gameObject);
	}
}
