using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource chaseMusic;

    bool isPlaying = false;

    public void StartChase()
    {
        if (!isPlaying)
        {
            chaseMusic.Play();
            isPlaying = true;
        }
    }

    public void StopChase()
    {
        if (isPlaying)
        {
            chaseMusic.Stop();
            isPlaying = false;
        }
    }
}