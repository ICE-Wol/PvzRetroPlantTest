using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class SunflowerAnim : MonoBehaviour {
        public Transform bottom;
        public Transform sun;
        public Transform eye;
        public Transform mouse;
        public SpriteRenderer sunGlow;
        public SpriteRenderer sunBottom;

        public int timer = 0;
        public float speed = 0.5f;
        public float range = 0.03f;

        public float eyeYScale = 1f;
    
        public bool isBlinking;
        public int nextBlinkTime;
        public Vector3 nextRandomPos;

        public bool isProducing;
        public float maxAlpha;
        public float curAlpha;
        public Vector3 tarMouseScale = new Vector3(2f, -2f, 0f);

        public bool isTakingDamage;
        public Material damagedGlitchMaterial;
        public Material normalLineMaterial;
        private static readonly int GlitchDist = Shader.PropertyToID("_GlitchDist");
        
        public void SetProducingTrue() {
            isProducing = true;
            maxAlpha = 0.5f;
            curAlpha = 0f;
        }

        public void SetTakingDamageTrue() {
            isTakingDamage = true;
            sunBottom.material = damagedGlitchMaterial;
            sunBottom.color = sunBottom.color.SetAlpha(1f);
            sunBottom.sortingOrder = -1;
        }

        public void SetTakingDamageFalse() {
            isTakingDamage = false;
            sunBottom.material = normalLineMaterial;
        }

        private void Update() {
            if(Input.GetMouseButtonDown(0)) SetProducingTrue();
            if(Input.GetMouseButtonDown(1)) SetTakingDamageTrue();
        }

        private void FixedUpdate() {
            bottom.localScale = Vector3.one + range * Mathf.Sin(Mathf.Deg2Rad * timer * speed) *
                (Vector3.right + Vector3.down);
        
            sun.localPosition = (3f + 1f * Mathf.Sin(Mathf.Deg2Rad * timer * speed)) * Vector3.up;

            EyeMovementAndBlinking();

            ProducingSun();

            if (isTakingDamage) {
                eye.transform.localScale = new Vector3(eye.transform.localScale.x, eye.transform.localScale.y / 2f, 0);
                if (timer == nextBlinkTime) sunBottom.material.SetFloat(GlitchDist, Random.Range(-0.05f, 0.05f));
                sun.localPosition /= 1.2f;
            }
            timer++;
        }

        private void ProducingSun() {
            if (isProducing && !curAlpha.Equal(maxAlpha, 0.01f)) {
                Debug.Log(curAlpha);
                sun.transform.localScale =
                    Calc.ApproachValue(sun.transform.localScale, 1.3f * Vector3.one, 32f * Vector3.one);
                mouse.transform.localScale =
                    Calc.ApproachValue(mouse.transform.localScale, tarMouseScale, 32f * Vector3.one);
            } else if (isProducing && curAlpha.Equal(maxAlpha, 0.01f)) {
                isProducing = false;
            } else {
                maxAlpha = 0f;
                mouse.transform.localScale =
                    Calc.ApproachValue(mouse.transform.localScale, (Vector3)Vector2.one, 32f * Vector3.one);
                sun.transform.localScale =
                    Calc.ApproachValue(sun.transform.localScale, Vector3.one, 32f * Vector3.one);
            }
            curAlpha.ApproachRef(maxAlpha, 32f);
            if(curAlpha.Equal(sunGlow.color.a, 0.01f)) return;
            sunGlow.color = sunGlow.color.SetAlpha(curAlpha);
        }
    

        private void EyeMovementAndBlinking() {
            if (timer == nextBlinkTime) isBlinking = true;

            if (isBlinking && !eyeYScale.Equal(0f, 0.1f)) {
                eyeYScale.ApproachRef(0f, 12f);
            }
            else if (isBlinking && eyeYScale.Equal(0f, 0.1f)) {
                isBlinking = false;
                nextBlinkTime += (Random.value >= 0.2) ? Random.Range(300, 500) : Random.Range(50, 80);
                nextRandomPos = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.3f, 0.3f));
            }
            else {
                eyeYScale.ApproachRef(1f, 12f);
            }

            eye.localScale = Mathf.Sign(nextRandomPos.x) * (1.1f + 0.1f * Mathf.Sin(Mathf.Deg2Rad * timer * speed)) *
                             Vector3.right
                             + eyeYScale * Vector3.up;
            eye.localPosition = Calc.ApproachValue(eye.localPosition, nextRandomPos, 32f * Vector3.one);
        }
    }
}
