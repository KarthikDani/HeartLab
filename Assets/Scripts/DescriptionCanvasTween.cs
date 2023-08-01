using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionCanvasTween : MonoBehaviour
{

    public Canvas descriptionCanvas;
    public Image panelImage;
    public TextMeshPro descriptionText;

    string originalText;
    int currentIndex;
    string currentText;
    private bool isTyping = false;
    private LTDescr typingTween;

    public float animationDuration = 1f;
    public float slideDistance;
    public Vector3 finalScale = Vector3.one;

    private bool isDescriptionVisible = false;
    private bool isBreakNeccessary = false;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    Vector3 fixedTargetPosition = new Vector3(1.64f, 0.125f, 0.5f);

    private float typingSpeed = 0.02f;

    private JSONReader jsonReader;

    // Example usage
    public string labelToSearch;

    private void Start()
    {

        // Store the initial position and rotation of the canvas
        initialPosition = descriptionCanvas.transform.position;
        initialRotation = descriptionCanvas.transform.rotation;

        initialScale = descriptionCanvas.transform.localScale;
        descriptionCanvas.gameObject.SetActive(false);

        jsonReader = FindObjectOfType<JSONReader>();
    }

    private void LateUpdate()
    {
        if (isTyping)
        {
            if (currentIndex < originalText.Length)
            {
                currentText += originalText[currentIndex];
                descriptionText.text = currentText;
                currentIndex++;
            }
            else
            {
                isTyping = false;
            }
        }
    }

    public void ToggleDescription()
    {
        if (isDescriptionVisible)
        {
            // If the description is already visible, animate its disappearance and move it back to its initial position
            panelImage.CrossFadeAlpha(0f, animationDuration, true);

            descriptionCanvas.transform.position = initialPosition;
            descriptionCanvas.transform.rotation = initialRotation;
            descriptionCanvas.transform.localScale = initialScale;
            isBreakNeccessary = true;
            descriptionText.text = string.Empty;
            originalText = string.Empty;

            descriptionCanvas.gameObject.SetActive(false);


        }
        else
        {
            // If the description is hidden, animate its appearance and move it to a slightly offset position
            descriptionCanvas.gameObject.SetActive(true);
            descriptionText.enabled = false;

            panelImage.canvasRenderer.SetAlpha(0f);
            panelImage.CrossFadeAlpha(1f, animationDuration, true);
            descriptionCanvas.transform.localScale = Vector3.zero;

            Vector3 buttonPosition = gameObject.transform.position;
            descriptionCanvas.transform.position = buttonPosition;

            Vector3 slideOffset = new Vector3(slideDistance, 0f, 0f);
            Vector3 targetPosition = fixedTargetPosition;

            LeanTween.move(descriptionCanvas.gameObject, targetPosition, animationDuration)
                .setEase(LeanTweenType.easeInOutQuad);

            descriptionCanvas.transform.rotation = initialRotation;

            LeanTween.scale(descriptionCanvas.gameObject, finalScale, animationDuration)
                .setOnComplete(() =>
                {
                    // Start the typewriting effect when the scaling animation completes
                    originalText = labelToSearch + "\n\n" + jsonReader.GetDescriptionByLabel(labelToSearch);
                    currentText = string.Empty;
                    currentIndex = 0;
                    StopAllCoroutines();
                    //StartCoroutine(TypewritingEffect());
                    StartTyping();
           
                });
        }

        // Toggle the visibility flag
        isDescriptionVisible = !isDescriptionVisible;
    }

    private IEnumerator TypewritingEffect()
    {
        descriptionText.enabled = true;
        while (currentIndex < originalText.Length)
        {
            if (isBreakNeccessary) StopCoroutine(TypewritingEffect());
            currentText += originalText[currentIndex];
            descriptionText.text = currentText;
            currentIndex++;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void StartTyping()
    {
        currentIndex = 0;
        currentText = string.Empty;
        isTyping = true;
        descriptionText.enabled = true;
        InvokeRepeating("TypeNextCharacter", typingSpeed, typingSpeed);
    }

    private void TypeNextCharacter()
    {
        if (currentIndex < originalText.Length)
        {
            currentText += originalText[currentIndex];
            descriptionText.text = currentText;
            currentIndex++;
        }
        else
        {
            isTyping = false;
            CancelInvoke("TypeNextCharacter");
        }
    }

}
