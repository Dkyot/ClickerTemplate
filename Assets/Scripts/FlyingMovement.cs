using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private bool rotateInDirection = true;
    
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;
    [SerializeField] private float amplitudeOffset;

    [SerializeField] private float duration = 20f;
    private float timer = 0;

    private float angle;

    private void Start() {
        transform.position = pointA.position;

        angle = Mathf.Atan2(pointB.position.y - pointA.position.y, pointB.position.x - pointA.position.x) * Mathf.Rad2Deg;
        
        if (rotateInDirection)
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void Update() {
        var ySinOffset = new Vector3(0, Mathf.Sin(Time.time * speed) * amplitude + amplitudeOffset, 0);
        var rotatedYSinOffset = Quaternion.AngleAxis(angle, Vector3.forward) * ySinOffset;

        timer += Time.deltaTime;
        var LerpT = timer / duration;

        var pos = Vector3.Lerp(
            pointA.position + rotatedYSinOffset, 
            pointB.position + rotatedYSinOffset, 
            LerpT
        );

        transform.position = pos;
    }
}