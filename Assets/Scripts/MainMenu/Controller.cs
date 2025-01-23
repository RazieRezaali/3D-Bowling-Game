using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void ExitButton(){
        Application.Quit();
        Debug.Log("game got closed");
    }

    public void StartGame(){
        SceneManager.LoadScene("Level 1");
    }
}
