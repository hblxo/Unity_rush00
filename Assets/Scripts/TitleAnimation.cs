﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class TitleAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Awake () 
	{
		transform.Rotate(Vector3.forward, -12);
		StartCoroutine("PlayText");
	}

	private IEnumerator PlayText()
	{
		while (true)
		{
			for (var i = 0; i < 6; i++)
			{
				transform.Rotate(Vector3.forward,3);
				transform.position += Vector3.down * 0.06f;
				yield return new WaitForSeconds (0.12f);
			}
			for (var i = 0; i < 6; i++)
			{
				transform.Rotate(Vector3.forward,-3);
				transform.position += Vector3.up * 0.06f;
				yield return new WaitForSeconds (0.12f);
			}
		}
	}
	
}
