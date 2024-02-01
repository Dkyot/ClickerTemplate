using UnityEngine;

namespace Clicker.UI {
    public class ScaleAndRotate : MonoBehaviour {
        private Vector2 _startScale;
        private float _startRotationZ;

        private void Awake() {
            _startScale = transform.localScale;
            _startRotationZ = transform.localRotation.eulerAngles.z;
        }

        public void Enter() {
            LeanTween.cancel(gameObject);

            transform.LeanScale(_startScale * 1.06f, 0.1f).setEaseInCubic();
            transform.LeanRotateZ(_startRotationZ - 10, 0.1f).setEaseInCubic();
        }

        public void Exit() {
            LeanTween.cancel(gameObject);

            transform.LeanScale(_startScale, 0.1f).setEaseInCubic();
            transform.LeanRotateZ(_startRotationZ, 0.1f).setEaseInCubic();
        }
    }
}
