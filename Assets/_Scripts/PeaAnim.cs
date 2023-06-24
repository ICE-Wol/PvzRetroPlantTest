using System;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class PeaAnim : MonoBehaviour {
        // 勾选is snow初值设定雪花豌豆
        public bool isSnow;

        public int timer;

        private void FixedUpdate() {
            if (timer % 10 == 0) {
                var randPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                ParticleAnim particle = ParticleManager.Manager.peaParPool.Get();
                particle.transform.position = transform.position + randPos;
                particle.spriteRenderer.color = isSnow ? Color.cyan : Color.green;
            }

            timer++;
        }

        private void OnDestroy() {
            for (int i = 0; i < 10; i++) {
                var par = ParticleManager.Manager.peaParPool.Get();
                par.transform.position = transform.position;
                par.spriteRenderer.color = isSnow ? Color.cyan : Color.green;
            }
        }
    }
}
