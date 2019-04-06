using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private float _speed = 0.1f;
	private Weapon _weapon;
	
	// Use this for initialization
	void Start ()
	{
		gameObject.AddComponent<Pistol>();
		_weapon = gameObject.GetComponent<Weapon>();
		Debug.Log("Player spawned at: " + transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") * _speed, transform.position.y + Input.GetAxisRaw("Vertical") * _speed, 0);
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 0;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		angle += 90;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		
		
		if (Input.GetButton("Fire1"))
		{
			_weapon.Shoot(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}
	
}
