using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera cam;

    private void Awake()
    {
        Player.Loaded += SetFollowTarget;
        cam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    private void OnDestroy()
    {
        Player.Loaded -= SetFollowTarget;
    }

    private void SetFollowTarget(Transform targetTransform)
    {
        cam.Follow = targetTransform;
        cam.LookAt = targetTransform;
    }
}
