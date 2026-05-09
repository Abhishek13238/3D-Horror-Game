using UnityEngine;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public AudioSource buttonClick;

    public void Restart()
    {
        buttonClick.Play();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void Menu()
    {
        buttonClick.Play();
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void Play()
    {
        buttonClick.Play();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        buttonClick.Play();
        Application.Quit();
    }
}
