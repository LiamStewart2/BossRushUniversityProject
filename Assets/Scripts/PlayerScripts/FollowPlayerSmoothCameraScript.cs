using UnityEngine;

public class FollowPlayerSmoothCameraScript : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 targetOffset;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 targetPosition = target.transform.position + targetOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
