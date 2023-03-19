using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : Attack
{

    [SerializeField] private float _speedRotation;
    
    protected override void Behaviour()
    {
        _rigidbody.rotation += _speedRotation;
    }
}
