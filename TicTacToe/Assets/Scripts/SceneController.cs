using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void testy2()
    {
        StartCoroutine(testy());
    }

    IEnumerator testy()
    {
        Debug.Log("waiting...");
        yield return new WaitForSeconds(.3f);
        LoadMenu();
    }
}
