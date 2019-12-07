using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour {

	public float speed = 20;
	public float mouseSpeed = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float ms = Input.GetAxis("Mouse ScrollWheel");
		transform.Translate(new Vector3(h, -ms*mouseSpeed, v) * Time.deltaTime*speed,Space.World);
	}
}
