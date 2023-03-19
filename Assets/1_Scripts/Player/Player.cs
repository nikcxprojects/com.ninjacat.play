using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private float _forceJump;
    [SerializeField] private CharacterData _playerData;
    [SerializeField] private GameManager _gameManager;
    
    [Space]
    
    [Header("Control")]
    [SerializeField] private float _maxMoveSpeed = 10;
    [SerializeField] private float _smoothTime = 0.3f;
    
    [Space]
    
    [Header("Attack")] 
    [SerializeField] private float _attackTime;
    [SerializeField] private Slider _attackSlider;
    
    [Space]
    
    [Header("Effects")]
    [SerializeField] private GameObject _jumpEffect;
    [SerializeField] private GameObject _deathEffect;

    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody2D;

    private bool _attackAllow = true;

    private float _attackCounter;
    
    #region UnityEvents
    
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<PolygonCollider2D>();
        StartCoroutine(StartAnimation());
        Init();
    }

    private void Update()
    {
        if (_attackAllow) return;
        
        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
            _attackAllow = true;
    }
    
    private void FixedUpdate()
    {
        if(!_attackAllow) _attackSlider.value = 100 * _attackCounter / _attackTime;

        if (transform.position.y < DisplayManager.BottomY)
        {
            _gameManager.GameOver();
            Death();
        }

        if (Input.GetMouseButtonDown(0))
            Attack();
        else if (Input.GetMouseButton(0))
            Move(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Death();
            _gameManager.GameOver();
        }
        
        if (!(_rigidbody2D.velocity.y <= 0 && other.tag.Equals("Platform"))) return;
        
        Jump();
        _gameManager.GenerateNewPlatform();
    }
    
    #endregion

    #region Private Voids
    
    private void Init()
    {
        _sprite.sprite = _playerData.Sprite;
    }
    
    private void Jump()
    {
        _rigidbody2D.velocity = Vector2.up * _forceJump;
        Instantiate(_jumpEffect, transform.position, Quaternion.identity);
    }

    Vector2 currentVelocity;

    private void Move(float xPos)
    {
        var pos = new Vector2(xPos, transform.position.y);
        transform.position = Vector2.SmoothDamp(transform.position, pos, ref currentVelocity,
            _smoothTime, _maxMoveSpeed);
    }
    
    private void Death()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator StartAnimation()
    {
        float targetGravity = _rigidbody2D.gravityScale;
        float timeElapsed = 0;
        while (timeElapsed < 1)
        {
            var gravity = Mathf.Lerp(0, targetGravity, timeElapsed / 1);
            _rigidbody2D.gravityScale = gravity;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _rigidbody2D.gravityScale = targetGravity;
    }
    
    #endregion
    
    #region PublicVoids
    
    public void Attack()
    {
        if (!_attackAllow) return;
        
        var obj = Instantiate(_playerData.AttackPrefab, transform.position, Quaternion.identity);
        _attackAllow = false;
        _attackCounter = 0;
    }
    
    #endregion
}