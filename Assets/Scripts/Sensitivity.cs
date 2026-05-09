using UnityEngine;

public class Sensitivity : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 0.2f;
    public float minPitch = -80f;
    public float maxPitch = 80f;
    private float xRotation = 0f;
    public bool canLook = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (!canLook) return;
        Look();
    }
    void Look()
    {
        float deltaX = 0f;
        float deltaY = 0f;

        // Mobile touch
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                deltaX = t.deltaPosition.x * sensitivity;
                deltaY = t.deltaPosition.y * sensitivity;
            }
        }
        // PC mouse
        else
        {
            deltaX = Input.GetAxis("Mouse X") * sensitivity * 10f; // multiply for scale
            deltaY = Input.GetAxis("Mouse Y") * sensitivity * 10f;
        }

        // Horizontal → player yaw
        player.Rotate(Vector3.up * deltaX);

        // Vertical → camera pitch
        xRotation -= deltaY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}