using UnityEngine;

public class ScaleAndRotateOnHover : MonoBehaviour {
    private Vector2 _baseScale;
    private Quaternion _baseRotation;

    public void OnPointerEnter() {
        var euler = transform.localRotation.eulerAngles;
        var rotation = transform.localRotation;

        _baseScale = transform.localScale;
        _baseRotation = transform.localRotation;

        euler.z -= 10;
        rotation.eulerAngles = euler;

        transform.localScale = _baseScale * 1.1f;
        transform.localRotation = rotation;
    }

    public void OnPointerExit() {
        transform.localScale = _baseScale;
        transform.localRotation = _baseRotation;
    }
}
