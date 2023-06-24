using System;
using UnityEngine;

namespace _Scripts {
    public class MineCircleCtrl : MonoBehaviour {
        public bool isReady;
        public bool isExploded;

        public int fragNum;
        public MineFragCtrl fragPrefab;
        public MineFragCtrl[] frags;

        public float tarRad;
        
        public float spdMultiplier;
        public int timer;

        private void RefreshFrags() {
            for (int i = 0; i < fragNum; i++) {
                if (!isExploded) {
                    frags[i].alpha = (0.3f * Mathf.Sin(
                    ((i - timer / 10f) / fragNum * 3f * 360f - spdMultiplier * timer) *
                    Mathf.Deg2Rad) + 0.7f);
                    
                    if (isReady) {
                        frags[i].tarRad = tarRad;
                        frags[i].transform.localPosition
                            = frags[i].curRad
                              * ((float)i / fragNum * 360f
                                 + 10f * Mathf.Sin(((float)i / fragNum * 3f * 360f + timer) * Mathf.Deg2Rad)
                                 + spdMultiplier * timer).Deg2Dir3();
                        frags[i].transform.localScale =
                            (0.6f * Mathf.Sin(((i - timer / 10f) / fragNum * 3f * 360f - spdMultiplier * timer) *
                                              Mathf.Deg2Rad) + 1f) *
                            Vector3.right + Vector3.up;
                        frags[i].tarRot =
                            ((float)i / fragNum * 360f + 90f
                                                       + 10f * Mathf.Sin(((float)i / fragNum * 3f * 360f + timer) *
                                                                         Mathf.Deg2Rad)) * Vector3.forward;
                    } else {
                        frags[i].tarRad = tarRad * 0.75f;
                        frags[i].transform.localPosition
                            = frags[i].curRad
                              * ((float)i / fragNum * 360f
                                 + 10f * Mathf.Sin(((float)i / fragNum * 3f * 360f + timer) * Mathf.Deg2Rad)
                                 + spdMultiplier / 5f * timer).Deg2Dir3();
                        frags[i].transform.localScale = (0.2f) * Vector3.right + Vector3.up;
                        frags[i].tarRot =
                            ((float)i / fragNum * 360f + 90f
                                                       + 10f * Mathf.Sin(((float)i / fragNum * 3f * 360f + timer) *
                                                                         Mathf.Deg2Rad)) * Vector3.forward;
                    }
                } else {
                    frags[i].alpha -= 0.03f;
                    frags[i].transform.localPosition += ((float)i / fragNum * 360f + 90f
                        + 10f * Mathf.Sin(((float)i / fragNum * 3f * 360f + timer) *
                                          Mathf.Deg2Rad)).Deg2Dir3();
                }
            }
            
        }
        
        private void Start() {
            frags = new MineFragCtrl[fragNum];
            for (int i = 0; i < fragNum; i++) {
                frags[i] = Instantiate(fragPrefab, this.transform);
            }
        }

        private void FixedUpdate() {
            RefreshFrags();
            timer++;
        }
    }
}
