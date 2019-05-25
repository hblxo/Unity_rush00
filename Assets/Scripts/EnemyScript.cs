using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour, IKillable {
	
	public float Speed = 1f;
	private Vector3 _playerPos;
	private GameObject _weaponObj;
	private Weapon _weapon;
	public GameObject StartingWeapon;
	public GameObject DefaultWeapon;
	private bool _hasWeaponEquipped;
	private Rigidbody2D _body;
	private Animator _animator;
	private Vector3 _target;
	private bool _isMoving;
	public LayerMask Mask;
	public GameObject Head;
	public GameObject Body;
	public GameObject Legs;
	public Sprite[] HeadSprites;
	public Sprite[] BodySprites;
	private float _soundBuffer;

	public AudioSource _source;
	public AudioClip[] DeathSounds;
	
	// Use this for initialization
	void Start ()
	{
		if (StartingWeapon)
		{
			_weaponObj = Instantiate(StartingWeapon, transform.position + new Vector3(-0.2f, -0.2f, 0f),
				transform.rotation, transform);
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
		}

		Head.GetComponent<SpriteRenderer>().sprite = HeadSprites[Random.Range(0, HeadSprites.Length)];
		Body.GetComponent<SpriteRenderer>().sprite = BodySprites[Random.Range(0, BodySprites.Length)];
		_animator = GetComponentInChildren<Animator>();
		_playerPos = transform.position;
		_source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (_isMoving)
		{
			transform.position = Vector3.MoveTowards(transform.position, _playerPos, Speed * Time.deltaTime);
			_animator.SetBool("walk", true);
		}

		if (transform.position == _playerPos)
		{
			_isMoving = false;
			_animator.SetBool("walk", false);
		}
	}

	private void OnTriggerStay2D(Collider2D charac)
	{
		if (charac.gameObject.CompareTag("Player"))
		{
			var hit = Physics2D.Linecast(transform.position, charac.transform.position, Mask);
			Debug.DrawRay(transform.position,
				hit.point - (Vector2) transform.position, Color.yellow);
			if (hit.rigidbody.gameObject.CompareTag("Player"))
			{
				Move(charac.gameObject);
				if (Vector3.Distance(gameObject.transform.position, charac.transform.position) < _weapon.Range)
				{
					_weapon.Shoot();
					if (_weapon.Ammo == 0)
					{
						_hasWeaponEquipped = false;
						_weapon.Drop();
					}
				}
			}
		}
		else if (charac.gameObject.CompareTag("Weapon") && !_hasWeaponEquipped)
		{
			Debug.Log(charac.gameObject);
			if (charac.GetComponentInParent<Weapon>().IsEquipped == false && charac.GetComponentInParent<Weapon>().Ammo > 0)
			{
				Move(charac.gameObject);
				if (Vector3.Distance(transform.position, charac.transform.position) < 0.2f)
				{
					var wep = charac.gameObject.transform.parent;
					//wep.gameObject.transform.parent = transform;
					_weaponObj = wep.gameObject;
					charac.GetComponentInParent<Weapon>().Equip(gameObject);
					//wep.gameObject.transform.parent = transform;
					_weapon = _weaponObj.GetComponent<Weapon>();
					_weapon.Equip(gameObject);
					_hasWeaponEquipped = true;
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		/*Weapon wep = other.gameObject.transform.parent.GetComponent<Weapon>();
		if (!wep) return;
		if (wep.IsEquipped) return;
		if (Mathf.Abs(other.transform.parent.GetComponent<Rigidbody2D>().velocity.x) >= 0.2f
		    || Mathf.Abs(other.transform.parent.GetComponent<Rigidbody2D>().velocity.y) >= 0.2f)
		{
			Debug.Log(other.gameObject);
			Damage();
		}*/
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
		if (Time.time != _soundBuffer)
			_source.PlayOneShot(DeathSounds[Random.Range(0, DeathSounds.Length)]);
		_soundBuffer = Time.time;
		if(_weapon)
			_weapon.Drop();
		Destroy(gameObject);
	}
	
	public void Move(GameObject player)
	{
		_playerPos = player.transform.position;
		_playerPos.z = transform.position.z;
		_target = player.transform.position;
		_target.z = 0;

		Vector3 objectPos = transform.position;
		_target.x = _target.x - objectPos.x;
		_target.y = _target.y - objectPos.y;
		float angle = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
		angle += 90;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		_isMoving = true;
	}

	private void OnDestroy()
	{

	}
}
