using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BallController ball;
    private float _offset;

    private void Awake()
    {
        _offset = transform.position.y - ball.transform.position.y;
    }

    private void Update()
    {
        FollowTheBall(_offset);
    }

    private void FollowTheBall(float offset)
    {
        var newCameraPosition =
            new Vector3(transform.position.x, ball.transform.position.y + _offset, transform.position.z);
        transform.position = newCameraPosition;
    }
}