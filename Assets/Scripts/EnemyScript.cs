using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour, IKillable {
	
	public GameObject nextCheckpoint;
	private bool hasTarget;


	public float Speed = 1f;
	private Vector3 _playerPos;
	private GameObject _weaponObj;
	private Weapon _weapon;
	public GameObject StartingWeapon;
	public GameObject DefaultWeapon;
	private bool _hasWeaponEquipped;
	private Rigidbody2D _body;
	private Animator _animator;
	public Vector3 _target;
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
			
			Quaternion save = transform.rotation;
			transform.rotation = new Quaternion(0,0,0,0); // very degueulasse but i'm nul en maths so foutez moi la paix
			_weaponObj = Instantiate(StartingWeapon, transform.position + new Vector3(-0.2f, -0.2f, 0f),
				transform.rotation, transform);
			_weapon = _weaponObj.GetComponent<Weapon>();
			transform.rotation = save;
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

		if(nextCheckpoint){
			Move(nextCheckpoint);
		}
		hasTarget = false;

		Head.GetComponent<SpriteRenderer>().sprite = HeadSprites[Random.Range(0, HeadSprites.Length)];
		Body.GetComponent<SpriteRenderer>().sprite = BodySprites[Random.Range(0, BodySprites.Length)];
		_animator = GetComponentInChildren<Animator>();
		_playerPos = transform.position;
		_source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug((transform.position == nextCheckpoint.transform.position));
		if (nextCheckpoint && transform.position == nextCheckpoint.transform.position){
			nextCheckpoint = nextCheckpoint.GetComponent<Checkpoint>().nextCheckpoint;
			Move(nextCheckpoint);
		}
		//Debug.Log(nextCheckpoint = (nextCheckpoint && transform.position == nextCheckpoint.transform.position) ? nextCheckpoint.GetComponent<Checkpoint>().nextCheckpoint : nextCheckpoint);
		if (!hasTarget && nextCheckpoint)
		{
			//Move(nextCheckpoint);
			transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
			_animator.SetBool("walk", true);
		}
		if (_isMoving && hasTarget)
		{
			transform.position = Vector3.MoveTowards(transform.position, _playerPos, Speed * Time.deltaTime);
			_animator.SetBool("walk", true);
		}

		if (transform.position == _playerPos)
		{
			_isMoving = false;
			//if (!nextCheckpoint)
				_animator.SetBool("walk", false);
			//hasTarget = false;
			
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
	
	public void Move(GameObject target)
	{
		Vector2 targetPos;
		Vector3 objectPos = transform.position;

		if (target.tag == "Player" || target.tag == "Weapon"){
			hasTarget = true;
			_playerPos = target.transform.position;
			_playerPos.z = transform.position.z;
			targetPos = getTargetDir(_playerPos, objectPos);
		}
		else
		{
			_target = target.transform.position;
			_target.z = 0;
			targetPos = getTargetDir(_target, objectPos);
		}
		
		float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
		angle += 90;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		_isMoving = true;
		
	}

	private Vector2 getTargetDir(Vector2 targetPos, Vector2 objectPos){
		Vector2 targetDir;

		targetDir.x = targetPos.x - objectPos.x;
		targetDir.y = targetPos.y - objectPos.y;
		return targetDir;
	}

	private void OnDestroy()
	{

	}
}
