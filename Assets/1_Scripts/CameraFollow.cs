using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _yOffset;
    
    
    private void FixedUpdate()
    {
        var posY = Mathf.Clamp(transform.position.y + _yOffset,
            _target.position.y + _yOffset,
            _target.position.y + _yOffset);
        if(posY > transform.position.y) transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
