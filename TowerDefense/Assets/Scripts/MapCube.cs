﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
	[HideInInspector]
	public GameObject turretGo;//保存当前Cube上的炮台
	[HideInInspector]
	public TurretData turretData;
	[HideInInspector]
	public bool isUpgrade = false;

	public GameObject buildEffect;

	private Renderer render;

	void Start()
	{
		render = GetComponent<Renderer>();
	}
	
	public void BuildTurret(TurretData turretData)
	{
		this.turretData = turretData;
		isUpgrade = false;
		turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
		GameObject effect=GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1);
	}

	//升级
	public  void UpgradeTurret()
	{
		if (isUpgrade == true)
			return;
		Destroy(turretGo);
		isUpgrade = true;
		turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
	}

	//拆除
	public void DestroyTurret()
	{
		Destroy(turretGo);
		isUpgrade = false;
		turretGo = null;
		turretData = null;
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
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
