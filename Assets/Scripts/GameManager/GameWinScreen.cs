using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void NextLevelButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels available. Returning to main menu.");
            SceneManager.LoadScene("MainMenu"); 
        }
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}