using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IKillable
{
	private float _speed = 5f;
	private GameObject _weaponObj;
	public Weapon _weapon;
	private Animator _animator;
	public GameObject StartingWeapon;
	public GameObject DefaultWeapon;
	public GameObject Manager;
	private AudioSource _source;
	public AudioClip[] DeathSounds;
	private GameObject _defaultWeaponObj;
	public GameObject EndCheckPoint;
	private Rigidbody2D _body;
	private float _horizontal;
	private float _vertical;
	private bool _hasWeaponEquipped;
	
	// Use this for initialization
	void Start ()
	{
		_animator = gameObject.GetComponentInChildren<Animator>();
		_defaultWeaponObj = Instantiate(DefaultWeapon, transform.position + -transform.up * 0.2f, transform.rotation,
			transform);
		_weaponObj = Instantiate(StartingWeapon, transform.position + new Vector3(-0.2f, -0.2f, 0f), transform.rotation, transform);
		_weapon = _weaponObj.GetComponent<Weapon>();
		if (_weapon.GetComponent<SpriteRenderer>())
			_weaponObj.GetComponent<SpriteRenderer>().sprite = _weapon.ViewModel;
		_weapon.IsEquipped = true;
		if (_weapon.GetComponent<Rigidbody2D>())
			_weapon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		if (GetComponent<Rigidbody2D>())
			_body = GetComponent<Rigidbody2D>();
		if (_weaponObj != DefaultWeapon)
			_hasWeaponEquipped = true;
		_source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
		Debug.Log("Player spawned at: " + transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.timeScale == 0)
			return;
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
			_weapon.Shoot();
		}

		if (Input.GetButtonDown("Fire2"))
		{
			if (_hasWeaponEquipped)
			{
				_weapon.Drop();
				_weaponObj = _defaultWeaponObj;
				_weapon = _weaponObj.GetComponent<Weapon>();
				//_weapon.Equip();
				_hasWeaponEquipped = false;
			}
		}
	}

	private void FixedUpdate()
	{
		_body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _speed, Input.GetAxisRaw("Vertical") * _speed);
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (Input.GetKeyDown("e") && col.gameObject.CompareTag("Weapon") && !_hasWeaponEquipped)
		{
			var wep = col.gameObject.transform.parent;
			//wep.gameObject.transform.parent = transform;
			_weaponObj = wep.gameObject;
			_weapon = _weaponObj.GetComponent<Weapon>();
			_weapon.Equip(gameObject);
			_hasWeaponEquipped = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Finish"))
			GameManager.Gm.Win = true;
	}

	/*private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Lethal"))
		{
			Destroy(gameObject);
		}
			
	}*/

	public void Damage()
	{
		Manager.GetComponent<GameManager>().IsDead = true;
		_source.PlayOneShot(DeathSounds[Random.Range(0, DeathSounds.Length)]);
//		Destroy(gameObject);
	}
	
}
