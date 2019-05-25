using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Gm;
	[HideInInspector]public int PlayerAmmo;
	private Weapon _weapon;
	[HideInInspector] public GameObject[] _enemies;
	public GameObject Player;
	public bool IsDead = false;
	
	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (Gm == null)
			Gm = this;
	}
	
	// Use this for initialization
	void Start ()
	{
		_enemies = GameObject.FindGameObjectsWithTag("Enemy");
		_weapon = Player.GetComponent<PlayerScript>()._weapon;
		PlayerAmmo = _weapon ? _weapon.Ammo : 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateAmmo();
	}	

	private void UpdateAmmo()
	{
		_weapon = Player.GetComponent<PlayerScript>()._weapon;
		PlayerAmmo = _weapon ? _weapon.Ammo : 0;
	}
}
