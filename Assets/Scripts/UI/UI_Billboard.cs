using UnityEngine;

public class UI_Billboard : MonoBehaviour
{
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
