using UnityEngine;

namespace _Scripts {
    public class MineFragCtrl : MonoBehaviour {
        public Vector3 tarRot;
        public Vector3 curRot;
        public float tarRad;
        public float curRad;
        public float spdApp;

        public SpriteRenderer sprRenderer;
        public float alpha;

        private void FixedUpdate() {
            sprRenderer.color = sprRenderer.color.SetAlpha(alpha);
            curRad.ApproachRef(tarRad, spdApp);
            curRot.ApproachRef(tarRot, spdApp * Vector3.one);
            transform.localRotation = Quaternion.Euler(curRot);
        }
    }
}
