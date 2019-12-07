using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
	[HideInInspector]
	public GameObject turretGo;//保存当前Cube上的炮台
	[HideInInspector]
	public bool isUpgrade = false;

	public GameObject buildEffect;

	private Renderer render;

	void Start()
	{
		render = GetComponent<Renderer>();
	}
	
	public void BuildTurret(GameObject turretPrefab)
	{
		isUpgrade = false;
		turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
		GameObject effect=GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1);
	}

	void OnMouseOver()
	{
		if(turretGo==null&&EventSystem.current.IsPointerOverGameObject()==false)
		{
			render.material.color = Color.red;
		}
	}

	void OnMouseExit()
	{
		render.material.color = Color.white;
	}
}
