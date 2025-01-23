using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BowlingCameraController : MonoBehaviour
{
    public Camera mainCamera;       
    public Camera pinsCamera;         
    public GameObject replayScreen;  
    public float delayBeforeReplay = 1f;

    private RenderTexture recordedTexture; 

    private void Start()
    {
        replayScreen.SetActive(false);  
        pinsCamera.gameObject.SetActive(false);

        int width = mainCamera.pixelWidth;  
        int height = mainCamera.pixelHeight;

        recordedTexture = new RenderTexture(width, height, 24);  
        pinsCamera.targetTexture = recordedTexture;  
        replayScreen.GetComponent<RawImage>().texture = recordedTexture;  

        SetReplayCameraPosition();
    }

    public void ShootBall()
    {
        StartCoroutine(StartReplaySequence());
    }

    IEnumerator StartReplaySequence()
    {
        yield return new WaitForSeconds(delayBeforeReplay);

        SetReplayCameraPosition();  
        pinsCamera.gameObject.SetActive(true);
   
        yield return new WaitForSeconds(4f);  

        replayScreen.SetActive(true);
        pinsCamera.gameObject.SetActive(false);

        yield return new WaitForSeconds(4f); 
        replayScreen.SetActive(false);

        ResetBall();
    }

    void SetReplayCameraPosition()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        pinsCamera.transform.rotation = Quaternion.identity;

        switch (sceneName)
        {
            case "Level 1":
            case "Level 4":
                pinsCamera.transform.position = new Vector3(-17.15092f, 1.645415f, 5.483051f);
                break;

            case "Level 2":
            case "Level 5":
                pinsCamera.transform.position = new Vector3(-21.39416f, 1.485425f, -25.57762f);
                break;

            case "Level 3":
                pinsCamera.transform.position = new Vector3(14.68653f, 1.278719f, -15.60432f);
                break;

            default:
                pinsCamera.transform.position = new Vector3(0f, 5f, -15f);  
                pinsCamera.transform.rotation = Quaternion.Euler(15f, 0f, 0f); 
                break;
        }
    }

    void ResetBall()
    {
        FindObjectOfType<BallShooter>().ResetForNextShot();
    }
}
