using UnityEngine;

public class AlignArrowToWorldObjects : MonoBehaviour
{
    public RectTransform descriptionCanvas; // Reference to the description canvas's RectTransform component
    public Transform arrowModel; // Reference to the arrow model's Transform component

    private void LateUpdate()
    {
        // Check if the description canvas is visible to the camera
        bool isDescriptionCanvasVisible = IsCanvasVisible();

        // Enable or disable the arrow model based on visibility
        arrowModel.gameObject.SetActive(isDescriptionCanvasVisible);

        if (isDescriptionCanvasVisible)
        {
            // Calculate the center position of the description canvas in world space
            Vector3 canvasCenter = descriptionCanvas.position;

            // Calculate the direction vector from the arrow to the description canvas center
            Vector3 direction = (canvasCenter - arrowModel.position).normalized;

            // Calculate the rotation to align the arrow with the canvas direction, including a 90-degree rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 90f, 0f);

            // Smoothly rotate the arrow towards the target rotation
            arrowModel.rotation = Quaternion.Slerp(arrowModel.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private bool IsCanvasVisible()
    {
        // Implement your visibility detection logic here
        // You can use techniques like object tracking or image recognition to determine visibility

        // Return true if the description canvas is currently visible, or false if it is not visible
        // You can replace this with your own visibility check based on your implementation
        return descriptionCanvas.gameObject.activeSelf;
    }
}
