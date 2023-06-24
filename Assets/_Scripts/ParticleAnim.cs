using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleAnim : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    
    public Sprite zero;

    public bool isZero;
    public float initRot;
    public float randSpeed;

    public bool isSnow;
    public Color snowColor;

    private void Start() {
        initRot = Random.Range(-180f, 180f);
        randSpeed = Random.Range(32f, 40f);
        isZero = Random.value > 0.5f;

        if (isZero) spriteRenderer.sprite = zero;
        if (isSnow) spriteRenderer.color = snowColor;
        transform.rotation = Quaternion.Euler(0,0,initRot);
    }

    private void FixedUpdate() {
        transform.position += Time.fixedDeltaTime * initRot.Deg2Dir3();
        transform.localScale = transform.localScale.ApproachValue(Vector3.zero, randSpeed * Vector3.one);

        if (transform.localScale.x.Equal(0f, 0.01f)) {
            Destroy(gameObject);
        }
    }
}
