using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Gm;
	public int PlayerAmmo;
	private Weapon _weapon;
	public GameObject Player;
	
	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (Gm == null)
			Gm = this;
	}
	
	// Use this for initialization
	void Start ()
	{
		PlayerAmmo = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateAmmo();
	}

	private void UpdateAmmo()
	{
		_weapon = Player.GetComponent<Weapon>();
		if (_weapon)
			PlayerAmmo = _weapon.Ammo;
		else
			PlayerAmmo = 0;
	}
}
