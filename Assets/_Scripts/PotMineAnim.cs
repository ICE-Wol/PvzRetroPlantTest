using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class PotMineAnim : MonoBehaviour {
        public SpriteRenderer heart;
        public MineCircleCtrl[] circles;

        public bool isReady;
        public bool isExploded;

        public void GetReady() {
            isReady = true;
            heart.color = Color.red;
            foreach (var circle in circles) {
                circle.isReady = true;
            }
        }

        public void Explode() {
            isExploded = true;
            foreach (var circle in circles) {
                circle.isExploded = true;
            }
        }
        
        private void Update() {
            if(Input.GetMouseButtonDown(0)) GetReady();
            if(Input.GetMouseButtonDown(1)) Explode();
        }

        public void FixedUpdate() {
            if (!isExploded) {
                if (!isReady) {
                    heart.transform.localScale
                        = (0.05f * Mathf.Sin(10f * Time.time) + 0.45f) * Vector3.one;
                }
                else {
                    heart.transform.localScale
                        = (Mathf.Abs(0.1f * Mathf.Sin(10f * Time.time)) + 0.5f) * Vector3.one;
                }
            } else {
                heart.transform.localScale =
                    Calc.ApproachValue(heart.transform.localScale, 3f * Vector3.one, 16f * Vector3.one);
                heart.color = heart.color.Fade(32f);
                if(!heart.transform.localScale.x.Equal(3f,0.1f)){
                    var par = ParticleManager.Manager.peaParPool.Get();
                    float rand = 360f * Random.value;
                    for (int i = 0; i < 36; i++) {
                        par.transform.position =
                            transform.position + Calc.Deg2Dir3(rand + 10f * i) * heart.transform.localScale.x * 1.5f;
                        par.spriteRenderer.color = Color.red;
                    }
                }
            }
        }
    }
}
