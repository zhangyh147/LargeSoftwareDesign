using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour {

	public static int countEnemyAlive = 0;
	public Wave[] waves;
	public Transform START;
	public float waveRate;
	// Use this for initialization
	void Start () {
		StartCoroutine(CreateEnemy());
	}
	
	IEnumerator CreateEnemy()
	{
		foreach(Wave wave in waves)
		{
			for(int i=0;i<wave.count;i++)
			{
				GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
				countEnemyAlive++;
				if (i != wave.count - 1)
					yield return new WaitForSeconds(wave.rate);
			}
			while(countEnemyAlive>0)
			{
				yield return 0;
			}
			yield return new WaitForSeconds(waveRate);
		}
	}
}
