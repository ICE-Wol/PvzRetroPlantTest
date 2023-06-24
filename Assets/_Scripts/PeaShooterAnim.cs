using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class PeaShooterAnim : MonoBehaviour {
        public Transform eye;
        public Transform body;
        public bool isBlinking;
        public int blinkFlag = 30;
        public Vector3 nextRandomPos;
        public Vector3 nextRandomRot;
        public Vector3 curRot;
        public float eyeYScale = 1f;
        public float speed = 1f;

        public bool shootFlag;
        public Transform mouse;
        public Transform tail;
        public int nextShootTime = 150; //136 - 150
        public float mouseYScale;
        public Vector3 mouseDefaultScale;
        public bool isShooting;

        public bool isDouble;
        public Transform tail2;

        public Vector3 eyeCenterPos;
        
        public Transform[] steams;
        public Vector3[] steamScales;

        public bool isFrozenPea;
        public Material glitchMat;
        public Material normalMat;
        public Material frozenGlitchMat;
        public Material frozenNormalMat;
        public MeshRenderer bodyMesh;
        public int timer;

        public int shootTimer;

        public void Start() {
            steamScales = new Vector3[4];
            for (int i = 0; i < 4; i++) {
                steamScales[i] = steams[i].localScale;
            }
        }

        public void SetShootingTrue() {
            isShooting = true;
        }

        public void SetShootingFalse() {
            isShooting = false;
            body.localScale = 4f * Vector3.one;
        }

        public void SetDamagedTrue() {
            bodyMesh.material = isFrozenPea ? frozenGlitchMat : glitchMat;
        }

        public void SetDamagedFalse() {
            bodyMesh.material = isFrozenPea ? frozenNormalMat : normalMat;
        }

        public void FixedUpdate() {
            body.localPosition = 0.5f * Mathf.Sin(Mathf.Deg2Rad * timer * speed) * Vector3.up;
            mouse.localPosition = 3f * Vector3.right + 0.5f * Mathf.Sin(Mathf.Deg2Rad * timer * speed + 1f) * Vector3.up;
            tail.localPosition = 3f * Vector3.left + 0.5f * Mathf.Sin(Mathf.Deg2Rad * timer * speed - 1f) * Vector3.up;
            eyeCenterPos = 0.5f * Mathf.Sin(Mathf.Deg2Rad * timer * speed) * Vector3.up;

            if (isDouble) {
                tail2.localPosition = 3f * Vector3.left + 1.5f * Mathf.Sin(-Mathf.Deg2Rad * timer * speed + 1f) * Vector3.up;
            }

            for (int i = 0; i < 4; i++) {
                steams[i].localPosition =
                    (-2.6f - i * 0.8f + 0.4f * Mathf.Sin(Mathf.Deg2Rad * timer * speed + 0.8f * i)) * Vector3.up;
            }
            
            timer++;
            EyeMovementAndBlinking();

            if (isShooting) {
                Shooting();
            }
            
        }
        
        private void EyeMovementAndBlinking() {
            if (timer == blinkFlag) isBlinking = true;

            if (isBlinking && !eyeYScale.Equal(0f, 0.1f)) {
                eyeYScale.ApproachRef(0f, 8f);
            }
            else if (isBlinking && eyeYScale.Equal(0f, 0.1f)) {
                isBlinking = false;
                blinkFlag += (Random.value >= 0.2) ? Random.Range(200, 400) : Random.Range(40, 60);
                nextRandomPos = new Vector3(Random.Range(-0.1f, 0.4f), Random.Range(-0.1f, 0.4f),
                    Random.Range(-0.1f, 0.4f));
                nextRandomRot += new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
            }
            else {
                eyeYScale.ApproachRef(1f, 8f);
            }

            eye.localScale = Mathf.Sign(nextRandomPos.x) * (1.1f + 0.08f * Mathf.Sin(Mathf.Deg2Rad * timer * speed)) *
                             Vector3.right + eyeYScale * Vector3.up;
            eye.localPosition = eye.localPosition.ApproachValue(eyeCenterPos + nextRandomPos, 16f * Vector3.one);
            
            if(isShooting) curRot.ApproachRef(Vector3.zero, 8f * Vector3.one);
            else curRot.ApproachRef(nextRandomRot, 16f * Vector3.one);
            body.rotation = Quaternion.Euler(curRot);
        }

        private void Shooting() {
            if (shootTimer == nextShootTime) shootFlag = true;
            
            if (shootFlag && !mouseYScale.Equal(0f, 0.3f)) {
                mouseYScale.ApproachRef(0f, 16f);
            } else if (shootFlag && mouseYScale.Equal(0f, 0.3f)) {
                shootFlag = false;
                nextShootTime += Random.Range(136, 150);
            } else {
                mouseYScale.ApproachRef(1f, 16f);
            }

            eye.localScale = new Vector3(eye.localScale.x, eyeYScale / 2f, 0);
            body.localScale = ((1f - mouseYScale) * 3f + 5f) * Vector3.one;
            mouse.localScale = mouseYScale * mouseDefaultScale;
            mouse.localPosition = (2.5f + mouseYScale * 1.2f) * Vector3.right;
            
            for (int i = 0; i < 4; i++) {
                steams[i].localScale = steamScales[i] * (1.1f - mouseYScale / 3f);
            }

            shootTimer++;

        }
    }
}
