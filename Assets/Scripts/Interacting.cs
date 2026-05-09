using UnityEngine;
using TMPro;
public class Interacting : MonoBehaviour
{
    float interactDistance = 5f;
    public TextMeshProUGUI interactText;
    public AudioSource pickUp;
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Statue"))
            {
                interactText.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    StatueCounter.instance.AddStatue();
                    pickUp.Play();
                    Destroy(hit.collider.gameObject);
                    interactText.enabled = false;
                }
            }
        }
        else
        {
            interactText.enabled = false;
        }
    }


}
