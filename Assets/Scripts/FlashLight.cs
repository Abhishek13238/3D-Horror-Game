using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject Flash;
    bool isOn;

    void Start()
    {
        isOn = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                Flash.SetActive(false);
                isOn = false;
            }
            else
            {
                Flash.SetActive(true);
                isOn = true;
            }
        }
    }
}