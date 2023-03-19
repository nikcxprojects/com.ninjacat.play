using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parallax
{
    public abstract class ParallaxController : MonoBehaviour
    {
        [SerializeField] internal List<ParallaxLayout> Layouts = new List<ParallaxLayout>();
        [SerializeField] internal Transform Target;

        [SerializeField] private float loopOffset;
        private Vector2 ScreenSize => new Vector2(DisplayManager.Width + loopOffset, DisplayManager.Height + loopOffset);

        internal Vector2 MoveLayout(ParallaxLayout layout)
        {
            var x = Target.position.x * layout.distance + layout.startPosition.x;
            var y = Target.position.y * layout.distance + layout.startPosition.y;
            VectorSwitch(layout, x, y);
            return new Vector2(x, y);
        }
        
        internal Vector2 SmoothMoveLayout(ParallaxLayout layout)
        {
            var targetX = Target.position.x * layout.distance + layout.startPosition.x;
            var targetY = Target.position.y * layout.distance + layout.startPosition.y;
            var refX = 0.0f;
            var refY = 0.0f;
            var x = Mathf.SmoothDamp(layout.transform.position.x, targetX,  ref refX, 0.1f);
            var y = Mathf.SmoothDamp(layout.transform.position.y, targetY,  ref refY, 0.1f);
            
            VectorSwitch(layout, x, y);
            
            return new Vector2(x, y);
        }

        internal void LoopMoveLayout(ParallaxLayout layout, bool smooth)
        {
            var xy = smooth ? SmoothMoveLayout(layout) : MoveLayout(layout);
            VectorSwitch(layout, xy.x, xy.y, true);
        }
        
        private void VectorSwitch(ParallaxLayout layout, float x, float y, bool loop = false)
        {
            switch (layout.vector)
            {
                case ParallaxLayout.VectorParallax.X:
                    VectorX(layout, x, loop);
                    break;
                case ParallaxLayout.VectorParallax.Y:
                    VectorY(layout, y, loop);
                    break;
                case ParallaxLayout.VectorParallax.XY:
                    VectorX(layout, x, loop);
                    VectorY(layout, y, loop);
                    break;
                default:
                    VectorX(layout, x, loop);
                    VectorY(layout, y, loop);
                    break;
            }
        }

        private void VectorX(ParallaxLayout layout, float x, bool loop)
        {
            layout.transform.position = new Vector3 (x, layout.transform.position.y, layout.transform.position.z);
            
            if(!loop) return;
            
            if (layout.transform.position.x - Target.position.x > ScreenSize.x)
                layout.startPosition.x -= ScreenSize.x * 2;
            else if (layout.transform.position.x - Target.position.x < -ScreenSize.x)
                layout.startPosition.x += ScreenSize.x * 2;   
        }

        private void VectorY(ParallaxLayout layout, float y, bool loop)
        {
            layout.transform.position = new Vector3 (layout.transform.position.x, y, layout.transform.position.z);
            
            if(!loop) return;
            
            if (layout.transform.position.y - Target.position.y > ScreenSize.y)
                layout.startPosition.y -= ScreenSize.y * 2;
            else if (layout.transform.position.y - Target.position.y < -ScreenSize.y)
                layout.startPosition.y += ScreenSize.y * 2;
        }
        
    }
}

