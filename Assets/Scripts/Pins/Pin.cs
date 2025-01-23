using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool hasFallen = false;
    public AudioSource hitWood1;
    public AudioSource hitWood2;

    void Update()
    {
        if (!hasFallen && transform.up.y < -0.2f) 
        {
            hasFallen = true;
            int randomInt = Random.Range(0, 2);
            if(randomInt == 0)
                hitWood1.Play();               
            else
                hitWood2.Play();

            GameObject.FindObjectOfType<ScoreManager>().AddScore(1);
            GameObject.FindObjectOfType<BallShooter>().FallenPins();            
        }        
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Ball")){
            int randomInt = Random.Range(0, 2);
            if(randomInt == 0)
                hitWood1.Play();               
            else
                hitWood2.Play();
            GameObject.FindObjectOfType<ScoreManager>().AddScore(2);                        
        }
    }
}