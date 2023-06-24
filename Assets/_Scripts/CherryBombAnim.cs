using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class CherryBombAnim : MonoBehaviour {
        public Transform leftCore;
        public Transform rightCore;

        public int explodeTime;
        public int timer;

        public SpriteRenderer[] fadeSprites;

        private void FixedUpdate() {
            leftCore.transform.localPosition = Vector3.left + (Mathf.Abs(0.5f * Mathf.Sin(5f * timer * Mathf.Deg2Rad)) - 0.2f) * Vector3.up;
            rightCore.transform.localPosition =
                3f * Vector3.right + (3f - Mathf.Abs(0.5f * Mathf.Sin(5f * timer * Mathf.Deg2Rad) - 0.2f)) * Vector3.up;
            
            transform.localScale =
                Calc.ApproachValue(transform.localScale, 3f * Vector3.one, 24f * Vector3.one);
            float rand = 360f * Random.value;
            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < 2; j++) {
                    var par = ParticleManager.Manager.peaParPool.Get();
                    par.transform.position =
                        transform.position + Calc.Deg2Dir3(rand + 60f * i) * transform.localScale.x / (1.2f + j * 0.5f);
                    par.spriteRenderer.color = Color.red;
                }
            }

            foreach (var i in fadeSprites) {
                i.color = i.color.Fade(16f);
            }

            if (fadeSprites[0].color.a.Equal(0f, 0.1f)) {
                Destroy(gameObject);
            }
            timer++;
        }
    }
}
