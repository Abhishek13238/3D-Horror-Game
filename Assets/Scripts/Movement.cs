using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform EnemyHead;
    Rigidbody rb;
    public Transform cam;
    public GameObject escapeText;
    public AudioSource levelComplete;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        // Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camForward * v + camRight * h) * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Camera.main.GetComponent<Sensitivity>().canLook = false;
            Camera.main.transform.LookAt(EnemyHead);
            this.enabled = false;
        }
        if (other.CompareTag("Escape"))
        {
            levelComplete.Play();
            escapeText.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}