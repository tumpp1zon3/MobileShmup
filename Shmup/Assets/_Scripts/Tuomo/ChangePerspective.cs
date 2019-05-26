using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePerspective : MonoBehaviour
{
    public bool isHorizontal;

    [SerializeField]
    private Camera vertical;

    [SerializeField]
    private Canvas verticalCanvas;

    [SerializeField]
    private Camera horizontal;

    [SerializeField]
    private Canvas horizontalCanvas;

    void Start()
    {
        Camera.main.transform.position = vertical.transform.position;
        Camera.main.transform.rotation = vertical.transform.rotation;

        verticalCanvas.enabled = true;
        horizontalCanvas.enabled = false;

        Screen.autorotateToPortrait = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            ChangeToVertical();
        }

        if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            ChangeToHorizontal();
        }
    }

    public void ChangeToVertical()
    {
        isHorizontal = false;
        Camera.main.transform.position = vertical.transform.position;
        Camera.main.transform.rotation = vertical.transform.rotation;
        verticalCanvas.enabled = true;
        horizontalCanvas.enabled = false;

    }

    public void ChangeToHorizontal()
    {
        isHorizontal = true;
        Camera.main.transform.position = horizontal.transform.position;
        Camera.main.transform.rotation = horizontal.transform.rotation;
        horizontalCanvas.enabled = true;
        verticalCanvas.enabled = false;
    }
}