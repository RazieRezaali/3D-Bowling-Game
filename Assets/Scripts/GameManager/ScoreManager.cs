using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shotsText; 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highestScoreText; 
    [SerializeField] private int totalShots; 

    private int remainingShots; 
    private int score = 0;
    private int highestScore = 0;

    public int GetScore(){
        return score;
    }

    void Start()
    {
        remainingShots = totalShots;
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        UpdateHUD();
    }

    public void DecreaseShots()
    {
        remainingShots--;
        UpdateHUD();
    }

    public void AddScore(int pinsFallen)
    {
        score += pinsFallen;
        if(score > highestScore){
            PlayerPrefs.SetInt("HighestScore", score);
            highestScore = highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        }
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        shotsText.text = "Remaining Shots: " + remainingShots;
        scoreText.text = "Score: " + score;
        highestScoreText.text = "Highest Score: " + highestScore;
    }

    public int GetRemainingShots()
    {
        return remainingShots;
    }
}