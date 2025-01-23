using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    private Vector3 offset;
    private Vector3 newPosition;
    [SerializeField][Range(0, 3)] private float lerpValue;

    [SerializeField] private Transform animationStartPoint;
    [SerializeField] private Transform animationEndPoint;
    [SerializeField] private float animationDuration = 5f;

    private bool isAnimating = true;
    private float animationTimer = 0f;

    void Start()
    {
        offset = transform.position - ballTransform.position;    
        transform.position = animationStartPoint.position;
    }

    void LateUpdate()
    {
        if (isAnimating){        
            AnimateCameraIntro();
        }
        else{        
            SetCameraSmoothFollow();
        }
    }

    private void SetCameraSmoothFollow()
    {
        newPosition = Vector3.Lerp(
            transform.position,
            ballTransform.position + offset,
            lerpValue * Time.deltaTime
        );
        transform.position = newPosition;
    }

    private void AnimateCameraIntro()
    {
        if (animationStartPoint == null || animationEndPoint == null)
        {
            isAnimating = false;
            return;
        }

        animationTimer += Time.deltaTime;

        transform.position = Vector3.Lerp(
            animationStartPoint.position,
            animationEndPoint.position,
            animationTimer / animationDuration
        );

        transform.rotation = Quaternion.Lerp(
            animationStartPoint.rotation,
            animationEndPoint.rotation,
            animationTimer / animationDuration
        );

        if (animationTimer >= animationDuration)
        {
            isAnimating = false;
        }
    }
}