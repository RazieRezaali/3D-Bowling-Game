using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BallShooter : MonoBehaviour
{
    public Rigidbody ballRigidbody; 
    public GameObject arrow; 
    // public float maxPower = 20f; 
    // public float minPower = 5f; 
    // public float powerIncreaseRate = 10f; 

    // private float currentPower; 
    private float currentAngle; 
    public bool isShot = false; 

    private int fallenPins = 0;
    public Vector3 initialPosition;

    [SerializeField] private ScoreManager scoreManager; 

    void Start(){
    
        // currentPower = (maxPower + minPower) / 2;
        currentAngle = -2.5f;

        ballRigidbody.isKinematic = true;

        arrow.transform.localRotation = Quaternion.Euler(82.5f, -2.5f, 91.31f);
        arrow.SetActive(true);
        SetInitialPosition();
    }

    private void SetInitialPosition(){    
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Level 1":
            case "Level 4":
                initialPosition = new Vector3(-17.192f, 0.51f, -12.34f);
                break;
            case "Level 2":
            case "Level 5":
                initialPosition = new Vector3(-21.4f, 0.51f, -49.2f);
                break;
            case "Level 3":
                initialPosition = new Vector3(14.57f, 0.51f, -30.3f);
                break;                        
            default:
                initialPosition = new Vector3(-17.192f, 0.51f, -12.34f);
                break;
        }
    }

    void Update(){    
        if (isShot){
            arrow.SetActive(false);
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentAngle -= 0.1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentAngle += 0.1f;
        }

        currentAngle = Mathf.Clamp(currentAngle, -45f, 45f);
        arrow.transform.localRotation = Quaternion.Euler(82.5f, currentAngle, 91.31f);

        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     currentPower += powerIncreaseRate * Time.deltaTime;
        // }
        // if (Input.GetKey(KeyCode.DownArrow))
        // {
        //     currentPower -= powerIncreaseRate * Time.deltaTime;
        // }

        // currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
        // arrow.transform.localScale = new Vector3(1f, 1f, 1f + (currentPower - minPower) / (maxPower - minPower));

        if (Input.GetKeyDown(KeyCode.Space)){        
            Shoot();
        }
    }

    void Shoot()
    {
        if (isShot) return;

        isShot = true;
        ballRigidbody.isKinematic = false;

        Vector3 forceDirection = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward;

        ballRigidbody.AddForce(forceDirection * 30, ForceMode.Impulse);

        FindObjectOfType<BowlingCameraController>().ShootBall();
        GameObject.FindObjectOfType<ScoreManager>().DecreaseShots();

        StartCoroutine(ResetAfterDelay(5f));    
        
        GameObject.FindObjectOfType<GameController>().CheckForNextShot();
    }
    
    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  
    }

    public void FallenPins(){
        fallenPins ++;
    }

    public int GetFallenPinsCount(){
        return fallenPins;
    }

    public void ResetForNextShot(){
        // Debug.Log("Resetting ball for the next shot.");
        isShot = false;
        ballRigidbody.isKinematic = true;

        transform.position = initialPosition;
        // transform.position = initialPosition.position;
        transform.rotation = Quaternion.identity;

        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;

        arrow.SetActive(true);
        arrow.transform.localRotation = Quaternion.Euler(82.5f, -2.5f, 91.31f);

        // Debug.Log("Ball reset complete.");
    }
}
