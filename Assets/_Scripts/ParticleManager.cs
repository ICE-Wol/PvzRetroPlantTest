using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class ParticleManager : MonoBehaviour {
        public ParticleAnim peaParPrefab;
        public int timer;
        
        public ObjectPool<ParticleAnim> peaParPool;

        public static ParticleManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        public void Start() {

            peaParPool = new ObjectPool<ParticleAnim>(() => Instantiate(peaParPrefab,this.transform), 
                p => {
                    p.gameObject.SetActive(true);
                    p.transform.localScale = 0.3f * Vector3.one;
                }, p => {
                    p.gameObject.SetActive(false);
                }, p => {
                    Destroy(p.gameObject);
                }, false, 100, 2000);
        }
    }
}
