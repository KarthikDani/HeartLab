using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartInteraction : MonoBehaviour
{
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector2 initialFingerPosition;
    private float initialDistance;

    private bool isScaling;
    private bool isRotating;
    private bool isTranslating;

    private void Start()
    {
        // Store the initial scale, position, and rotation of the heart model
        initialScale = transform.localScale;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // Check for touch input
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Check for touch phase
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if touch position is over the heart model
                    if (IsTouchOverHeart(touch.position))
                    {
                        // Store initial finger position and set translating flag
                        initialFingerPosition = touch.position;
                        isTranslating = true;
                    }
                    break;

                case TouchPhase.Moved:
                    // If translating, move the heart model based on finger movement
                    if (isTranslating)
                    {
                        Vector2 fingerDelta = touch.position - initialFingerPosition;
                        transform.position = initialPosition + new Vector3(fingerDelta.x, fingerDelta.y, 0f) * 0.01f;
                    }
                    break;

                case TouchPhase.Ended:
                    // Reset translating flag
                    isTranslating = false;
                    break;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calculate the distance and direction between the two fingers
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;
            float prevDistance = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentDistance = (touch1.position - touch2.position).magnitude;
            float deltaDistance = currentDistance - prevDistance;

            // Check for touch phase
            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    // Check if both touches are over the heart model
                    if (IsTouchOverHeart(touch1.position) && IsTouchOverHeart(touch2.position))
                    {
                        // Store initial scale and set scaling flag
                        initialScale = transform.localScale;
                        initialDistance = currentDistance;
                        isScaling = true;
                    }
                    break;

                case TouchPhase.Moved:
                    // If scaling, change the scale of the heart model based on finger movement
                    if (isScaling)
                    {
                        float scaleFactor = deltaDistance * 0.01f;
                        transform.localScale = initialScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
                    }
                    break;

                case TouchPhase.Ended:
                    // Reset scaling flag
                    isScaling = false;
                    break;
            }
        }
    }

    private bool IsTouchOverHeart(Vector2 touchPosition)
    {
        // Perform a raycast to check if the touch position is over the heart model
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    public void ResetHeartModel()
    {
        // Reset the scale, position, and rotation of the heart model
        transform.localScale = initialScale;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}

