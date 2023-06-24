using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Scripts;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class RainManager : MonoBehaviour {
    public SpriteRenderer rainPrefab;
    public RippleCtrl ripplePrefab;
    public int timer;

    public List<SpriteRenderer> rains;
    public ObjectPool<RippleCtrl> ripplePool;
    
    public static RainManager Manager;

    private void Awake() {
        if (!Manager) {
            Manager = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void Start() {
        ripplePool = new ObjectPool<RippleCtrl>(() => {
            return Instantiate(ripplePrefab); 
        }, p => {
            p.gameObject.SetActive(true);
            p.transform.localScale = Vector3.zero;
            p.sprRenderer.color = p.sprRenderer.color.SetAlpha(1f);
        }, p => {
            p.gameObject.SetActive(false);
        }, p => {
            Destroy(p.gameObject);
        }, false, 50, 200);
    }
    
    public void FixedUpdate() {
        if (timer % 10 == 0 && timer <= 1000) {
            var ins = Instantiate(rainPrefab, transform);
            ins.transform.position = Random.Range(-10f, 10f) * Vector3.right + 10f * Vector3.up;
            ins.transform.localScale = Random.Range(0.5f, 1.5f) * (Vector3.right + Vector3.up);
            ins.color = ins.color.SetAlpha(Random.Range(0.3f, 0.6f));
            rains.Add(ins);
        }
            
        //0.3 0.6 -4.5 5
        foreach (var drop in rains) {
            drop.transform.position += (drop.color.a + 0.2f) * 20f * Time.fixedDeltaTime * Vector3.down;
            if (drop.transform.position.y <= -(drop.color.a - 0.45f) * 30f) {
                drop.color = drop.color.Fade(16f);
                if (drop.color.a.Equal(0f, 0.1f)) {
                    var ripple = ripplePool.Get();
                    ripple.transform.position = drop.transform.position;
                    drop.transform.position = Random.Range(-10f, 10f) * Vector3.right + 10f * Vector3.up;
                    drop.transform.localScale = Random.Range(0.5f, 1.5f) * (Vector3.right + Vector3.up);
                    drop.color = drop.color.SetAlpha(Random.Range(0.3f, 0.6f));
                }
            }
        }
        
        timer++;
    }
}
