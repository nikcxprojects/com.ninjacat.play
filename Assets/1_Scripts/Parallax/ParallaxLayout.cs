using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parallax
{
    public class ParallaxLayout : MonoBehaviour
    {
        [SerializeField] internal float distance;
        [SerializeField] internal VectorParallax vector;

        internal Vector2 spawPosition;
        internal Vector2 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        internal enum VectorParallax
        {
            X = 0,
            Y = 1,
            XY = 2
        }
    }
}
