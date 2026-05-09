using UnityEngine;
using System.Collections;
public class Attack_Die : MonoBehaviour
{
    public Animator anim;

    public GameObject GameOverScreen;
    public GameObject GamePlayScreen;
    public AudioSource zombieAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.transform);
            anim.SetTrigger("Attack");
            StartCoroutine(GameOverDelay());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1f);
        zombieAttack.Play();
        yield return new WaitForSeconds(1.3f);
        GameOver();
        Time.timeScale = 0f;
    }
    void GameOver()
    {
        GameOverScreen.SetActive(true);
        GamePlayScreen.SetActive(false);
    }
}
