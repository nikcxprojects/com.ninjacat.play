using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyOffset;
    [SerializeField] private GameObject _destroyEffect;
    
    protected Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _speed);
        Behaviour();
        if(transform.position.y > DisplayManager.TopY + _destroyOffset) Destroy(gameObject);
    }

    protected abstract void Behaviour();

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.tag.Equals("Enemy")) return;
        Destroy(collider.gameObject);
        Instantiate(_destroyEffect, collider.transform.position, Quaternion.identity);
    }
}
