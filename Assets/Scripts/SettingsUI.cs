using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("space between menu items")]
    [SerializeField] Vector2 spacing;

    Button mainButton;

    SettingsUIChildImageButtons[] menuItems;
    bool isExpanded = false;
    Vector2 mainButtonPosition;
    int itemsCount;

    public float scaleDuration = 0.2f;
    public float rotationDuration = 0.4f;

    private LeanTweenType scaleEaseType = LeanTweenType.easeOutBack;
    private LeanTweenType rotationEaseType = LeanTweenType.easeOutElastic;

    public RectTransform buttonTransform;
    private Vector3 initialScale;

    void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsUIChildImageButtons[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsUIChildImageButtons>();
        }
        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();
        mainButtonPosition = mainButton.transform.position;

        initialScale = buttonTransform.localScale;

        if (mainButton != null)
        {
            mainButton.onClick.AddListener(AnimateButtonClick);
        }

        ResetPositions();
    }

    private void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].rectTrans.position = mainButtonPosition;
        }
    }

    private void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtonPosition + spacing * (i + 1);
                LeanTween.move(menuItems[i].rectTrans.gameObject, mainButtonPosition + spacing * (i + 1), 0.4f).setEase(LeanTweenType.easeOutExpo);
                LeanTween.alpha(menuItems[i].GetComponent<Image>().gameObject, 1f, 0.3f);


            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtonPosition;
                LeanTween.move(menuItems[i].transform.gameObject, mainButtonPosition, 0.3f).setEase(LeanTweenType.easeOutExpo);
                LeanTween.alpha(menuItems[i].GetComponent<Image>().gameObject, 0f, 0.3f);

            }

        }
    }

    public int OnItemClick(int index)
    {
        return index;
    }


    private void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }

    private void AnimateButtonClick()
    {
        // Scale animation
        LeanTween.scale(buttonTransform, initialScale * 1.2f, scaleDuration)
            .setEase(scaleEaseType)
            .setOnComplete(ResetScale);

        // Rotation animation
        LeanTween.rotateAroundLocal(buttonTransform.gameObject, Vector3.forward, 360f, rotationDuration)
            .setEase(rotationEaseType);
    }

    private void ResetScale()
    {
        LeanTween.scale(buttonTransform, initialScale, scaleDuration)
            .setEase(scaleEaseType);
    }
}