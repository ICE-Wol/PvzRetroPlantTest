using UnityEngine;

namespace _Scripts {
    public class CloudCtrl : MonoBehaviour {
        public Vector3 oriPos;
        public float xRange;
        public float yRange;
        public float xMultiplier;
        public float yMultiplier;

        public int timer;

        public void FixedUpdate() {
            transform.localPosition =
                oriPos + xRange * Mathf.Sin(Mathf.Deg2Rad * timer * 0.01f * xMultiplier) * Vector3.right +
                yRange * Mathf.Cos(Mathf.Deg2Rad * timer * 0.01f * yMultiplier) * Vector3.up;
            timer++;
        }

    }
}
