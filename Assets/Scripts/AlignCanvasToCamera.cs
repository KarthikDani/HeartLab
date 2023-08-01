using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignCanvasToCamera : MonoBehaviour
{
    public RectTransform canvasRectTrasform;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void LateUpdate()
    {
        canvasRectTrasform.LookAt(canvasRectTrasform.position + transform.rotation * Vector3.forward, transform.rotation * Vector3.up);
    }
}