using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIChildImageButtons : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public RectTransform rectTrans;

    //SettingsMenu reference
    SettingsUI settingsMenu;
    public RendererManager rendererManager;

    //item button
    Button button;
    Color color;

    //index of the item in the hierarchy
    int index;

    bool isTrue = true;

    void Awake()
    {
        img = GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();

        settingsMenu = rectTrans.parent.GetComponent<SettingsUI>();

        //-1 to ignore the main button
        index = rectTrans.GetSiblingIndex() - 1;
        //add click listener
        button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClick);
    }

    private void Start()
    {
        rendererManager = FindAnyObjectByType<RendererManager>();

        rendererManager.open_heart_section(false);
        rendererManager.mitral_valve(false);
        rendererManager.tricuspid_valve(false);
        rendererManager.closed_heart_section(true);
    }

    void OnItemClick()
    {

        setColor();
        switch (index)
        {
            case 0:
                //first button
                rendererManager.closed_heart_section(isTrue);
                break;
            case 1:
                //second button
                rendererManager.open_heart_section(isTrue);
                break;
            case 2:
                //third button
                rendererManager.coronary_arteries(isTrue);
                break;
            case 3:
                //third button
                rendererManager.mitral_valve(isTrue);
                break;
            case 4:
                //third button
                rendererManager.tricuspid_valve(isTrue);
                break;
    }

    }

    void OnDestroy()
    {
        //remove click listener to avoid memory leaks
        button.onClick.RemoveListener(OnItemClick);
    }

    void setColor()
    {
        color = img.color;
        if (color == Color.black)
        {
            color = Color.white;
            isTrue = true;
        }
        else
        {
            color = Color.black;
            isTrue = false;
        }
        
        img.color = color;
        
        //settingsMenu.OnItemClick(index);
    }
}
