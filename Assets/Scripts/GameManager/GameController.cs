using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public GameWinScreen gameWinScreen;
    public ScoreManager scoreManager;
    private bool isGameOver = false; 
    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highestScoreText;
    public BallShooter ballShooter;
    private bool isCheckingPins = false;

    public void GameOver()
    {
        if (isGameOver) return; 
        isGameOver = true;

        gameOverScreen.Setup(scoreManager.GetScore());

        shotsText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        highestScoreText.gameObject.SetActive(false);
    }

    public void WinThisLevel()
    {
        gameWinScreen.Setup(scoreManager.GetScore());

        shotsText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        highestScoreText.gameObject.SetActive(false);
    }

    public void CheckForNextShot(){
        if (isCheckingPins) return;
        isCheckingPins = true;

        StartCoroutine(CheckPinsAfterDelay());
    }

    private IEnumerator CheckPinsAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        if(ballShooter.GetFallenPinsCount() > 8){
            WinThisLevel();
        } else{
            if (scoreManager.GetRemainingShots() == 0){
                GameOver();
            }
            else{
                ballShooter.isShot = false;
            }
        }

        isCheckingPins = false;
    }

    // public void Update(){
    //     if (!ballShooter.isShot && Input.GetKeyDown(KeyCode.R)){
    //         ballShooter.ResetForNextShot();
    //     }   
    // }
}
