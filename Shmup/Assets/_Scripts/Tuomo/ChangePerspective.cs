using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspective : MonoBehaviour
{
    public bool isHorizontal;

    [SerializeField]
    private Camera vertical;

    [SerializeField]
    private Camera horizontal;

    void Start()
    {
        //vertical.enabled = true;
        //horizontal.enabled = false;
    }

    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            //isHorizontal = false;
            //horizontal.enabled = false;
            //vertical.enabled = true;
        }
        /*
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            isHorizontal = true;
            vertical.enabled = false;
            horizontal.enabled = true;
        }*/
    }
}