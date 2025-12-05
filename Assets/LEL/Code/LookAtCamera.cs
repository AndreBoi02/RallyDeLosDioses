using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    private Transform cam;

    void Start() {
        cam = Camera.main.transform;
    }

    void LateUpdate() {
        Vector3 direction = transform.position - cam.position;
        direction.y = 0;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * 5f
        );
    }
}
