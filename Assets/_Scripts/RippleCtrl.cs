using System;
using UnityEngine;

namespace _Scripts {
    public class RippleCtrl : MonoBehaviour {
        public SpriteRenderer sprRenderer;

        private void FixedUpdate() {
            transform.localScale =
                transform.localScale.ApproachValue(1.5f * Vector3.up + 2f * Vector3.right, 32f * Vector3.one);
            sprRenderer.color = sprRenderer.color.Fade(32f);

            if (sprRenderer.color.a.Equal(0f, 0.01f)) {
                RainManager.Manager.ripplePool.Release(this);
            }
        }
    }
}
