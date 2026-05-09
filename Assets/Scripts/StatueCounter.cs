using UnityEngine;
using TMPro;
using System.Collections;

public class StatueCounter : MonoBehaviour
{
    public static StatueCounter instance;
    public TextMeshProUGUI statueCount;
    int collected = 0;
    int total = 6;
    public GameObject openGate;
    public GameObject closeGate;
    public AudioSource masageSound;
    public GameObject gateOpenMessage;

    void Awake()
    {
        instance = this;
    }
    public void AddStatue()
    {
        collected++;
        statueCount.text = collected + "/" + total;

        if (collected == total)
        {
            gateOpenMessage.SetActive(true);
            openGate.SetActive(true);
            closeGate.SetActive(false);
            masageSound.Play();
            StartCoroutine(Message());
        }
    }
    IEnumerator Message()
    {
        yield return new WaitForSeconds(1.5f);
        gateOpenMessage.SetActive(false);
    }
}
