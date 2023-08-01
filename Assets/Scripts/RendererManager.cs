using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RendererManager : MonoBehaviour
{
    public List<SkinnedMeshRenderer> skinRenderers = new List<SkinnedMeshRenderer>();
    public List<GameObject> labelLines = new List<GameObject>();
    public List<Canvas> canvases = new List<Canvas>();

    public float animationDuration = 0.5f;
    public LeanTweenType animationEase = LeanTweenType.easeOutQuart;

    int i;

    public void closed_heart_section(bool isTrue)
    {
        //skin renderers
        skinRenderers[0].gameObject.SetActive(isTrue);

        //lines
        for (i = 0; i <= 12; i++)
        {
            labelLines[i].SetActive(isTrue);
        }
        labelLines[17].SetActive(isTrue);
        labelLines[20].SetActive(isTrue);
        labelLines[34].SetActive(isTrue);
        labelLines[35].SetActive(isTrue);

        //canvases
        for (i = 0; i <= 11; i++)
        {
            canvases[i].gameObject.SetActive(isTrue);
        }
        canvases[16].gameObject.SetActive(isTrue);
        canvases[19].gameObject.SetActive(isTrue);
        canvases[31].gameObject.SetActive(isTrue);

        //enable graphics of mitral and tricuspid
        for (i = 4; i <= 7; i++)
        {
            skinRenderers[6].gameObject.SetActive(isTrue);
        }

    }

    public void open_heart_section(bool isTrue)
    {
        //skin renderers
        skinRenderers[1].gameObject.SetActive(isTrue);

        skinRenderers[8].gameObject.SetActive(isTrue);
        skinRenderers[9].gameObject.SetActive(isTrue);
        skinRenderers[10].gameObject.SetActive(isTrue);
        skinRenderers[11].gameObject.SetActive(isTrue);

        //lines
        
        for(i=0; i <= 12; i++)
        {
            labelLines[i].SetActive(isTrue);
        }
        for (i = 22; i <= 34; i++)
        {
            labelLines[i].SetActive(isTrue);
        }

        //canvases
        for (i = 0; i <= 11; i++)
        {
            canvases[i].gameObject.SetActive(isTrue);
        }
        for (i = 21; i <= 31; i++)
        {
            canvases[i].gameObject.SetActive(isTrue);
        }
    }

    public void coronary_arteries(bool isTrue)
    {
        //skin renderers
        skinRenderers[2].gameObject.SetActive(isTrue);
        skinRenderers[3].gameObject.SetActive(isTrue);

        //lines
        for (i = 13; i <= 16; i++)
        {
            labelLines[i].SetActive(isTrue);
        }
        labelLines[18].SetActive(isTrue);
        labelLines[19].SetActive(isTrue);
        labelLines[21].SetActive(isTrue);

        //canvases
        for (i = 12; i <= 15; i++)
        {
            canvases[i].gameObject.SetActive(isTrue);
        }
        canvases[17].gameObject.SetActive(isTrue);
        canvases[18].gameObject.SetActive(isTrue);
        canvases[20].gameObject.SetActive(isTrue);
    }

    public void mitral_valve(bool isTrue)
    {
        skinRenderers[4].gameObject.SetActive(isTrue);
        skinRenderers[5].gameObject.SetActive(isTrue);

        labelLines[22].SetActive(isTrue);

        canvases[21].gameObject.SetActive(isTrue);


    }

    public void tricuspid_valve(bool isTrue)
    {
        skinRenderers[6].gameObject.SetActive(isTrue);
        skinRenderers[7].gameObject.SetActive(isTrue);

        labelLines[23].SetActive(isTrue);

        canvases[22].gameObject.SetActive(isTrue);
    }
    
}
