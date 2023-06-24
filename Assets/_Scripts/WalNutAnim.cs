using UnityEngine;
using UnityEngine.UIElements;

namespace _Scripts {
    public class WalNutAnim : MonoBehaviour {
        public Transform eye;
        public bool isBlinking;
        public int blinkFlag = 30;
        public Vector3 nextRandomPos;
        public float eyeYScale = 1f;
        public float speed = 1f;
    
        public Transform shield;
        public SpriteRenderer core;
        public int maxHp;//100
        public int curHp;//0

        public bool isTakingDamage;
        public Transform mouse;

        public int timer = 0;

        public void SetTakingDamageTrue() {
            isTakingDamage = true;
        }

        public void SetTakingDamageFalse() {
            isTakingDamage = false;
        }

        public Renderer shieldMesh;
        private void Start() {
            shieldMesh.sortingLayerName = "Plant";
        }
        

        private void FixedUpdate() {
            shield.transform.rotation = Quaternion.Euler(Time.time * -10f, Time.time * -20f, Time.time * 30f);
            core.transform.position = (2f + 0.5f * Mathf.Sin(100f * Time.time * Mathf.Deg2Rad)) * Vector3.up;
            if (isTakingDamage) {
                shield.transform.rotation = Quaternion.Euler(0,Time.time * -200f , 0);
                core.transform.position = (2f + 0.1f * Mathf.Sin(100f * Time.time * 5f * Mathf.Deg2Rad)) * Vector3.up;
            }
            core.color = Color.HSVToRGB((float)curHp / maxHp * 0.28f, 1f, 1f);

            EyeMovementAndBlinking();
            timer++;
        }
    
        private void EyeMovementAndBlinking() {
            if (timer == blinkFlag) isBlinking = true;

            if (isBlinking && !eyeYScale.Equal(0f, 0.1f)) {
                eyeYScale.ApproachRef(0f, 8f);
            }
            else if (isBlinking && eyeYScale.Equal(0f, 0.1f)) {
                isBlinking = false;
                blinkFlag += (Random.value >= 0.2) ? Random.Range(200, 400) : Random.Range(40, 60);
                nextRandomPos = new Vector3(Random.Range(1f, 2f), Random.Range(-0.5f, 0.5f), 0f);
            }
            else {
                eyeYScale.ApproachRef(1f, 8f);
            }

            if(isTakingDamage) mouse.localRotation = Quaternion.Euler(0f,50f,180f);
            else mouse.localRotation = Quaternion.Euler(0f,50f,0f);
            
            eye.localScale = Mathf.Sign(nextRandomPos.x) * (1.1f + 0.08f * Mathf.Sin(Mathf.Deg2Rad * timer * speed)) *
                Vector3.right + ((isTakingDamage) ? 0.6f : 1f) * eyeYScale * Vector3.up;
            eye.localPosition = eye.localPosition.ApproachValue(nextRandomPos, 16f * Vector3.one);
        }

    }
}
