﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	private float h = 0.0f;
	private float v = 0.0f;

	private Transform tr;
	public float moveSpeed = 10.0f;
	public float rotSpeed = 100.0f;

	// Use this for initialization
	void Start () {
		tr = GetComponent <Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		Debug.Log ("H=" + h.ToString());
		Debug.Log ("V=" + v.ToString());

		Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

		tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);

		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
	}
}
