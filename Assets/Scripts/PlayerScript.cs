using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private float _speed = 5f;
	private GameObject _weaponObj;
	private Weapon _weapon;
	private Animator _animator;
	public GameObject StartingWeapon;
	private Rigidbody2D _playerBody;
	private float _horizontal;
	private float _vertical;
	
	// Use this for initialization
	void Start ()
	{
		_animator = gameObject.GetComponentInChildren<Animator>();
		_weaponObj = Instantiate(StartingWeapon, transform.position + new Vector3(-0.2f, -0.2f, 0f), transform.rotation, transform);
		_weapon = _weaponObj.GetComponent<Weapon>();
		_weaponObj.GetComponent<SpriteRenderer>().sprite = _weapon.ViewModel;
		_weapon.IsEquipped = true;
		_playerBody = GetComponent<Rigidbody2D>();
		Debug.Log("Player spawned at: " + transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");
		if (_horizontal != 0|| _vertical != 0)
		{
			_animator.SetBool("walk", true);
		}
		else
		{
			_animator.SetBool("walk", false);
		}
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
			_weapon = _weaponObj.GetComponent<Weapon>();
			_weapon.Shoot(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}

		if (Input.GetButtonDown("Fire2"))
		{
			_weapon.Drop(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			_weaponObj = null;
			_weapon = null;
		}
	}

	private void FixedUpdate()
	{
		_playerBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _speed, Input.GetAxisRaw("Vertical") * _speed);
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (Input.GetKeyDown("e") && col.gameObject.CompareTag("Weapon") && !_weaponObj)
		{
			col.gameObject.transform.parent = transform;
			_weaponObj = col.gameObject;
			_weapon = _weaponObj.GetComponent<Weapon>();
			_weapon.Equip();
		}
	}
	
}
