using UnityEngine;

public class CogCtrl : MonoBehaviour {
    public float speedMultiplier;
    public int timer;

    public void FixedUpdate() {
        transform.localRotation = Quaternion.Euler(0, 0, 0.1f * timer * speedMultiplier);
        timer++;
    }
}
