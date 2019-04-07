using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameManager GM;
	public Text Ammo;
	
	// Use this for initialization
	void Start () {
		SetProperties();
	}
	
	// Update is called once per frame
	void Update () {	
		SetProperties();
	}
	
	public void SetProperties()
	{
		Ammo.text = GM.PlayerAmmo.ToString();
	}
}
