using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspective : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    private Vector3 verticalPos;
    private Quaternion verticalRot;

    private Vector3 horizontalPos;
    private Quaternion horizontalRot;

    void Start()
    {
        verticalPos = Camera.main.transform.position;
        verticalRot = Camera.main.transform.rotation;

        horizontalPos = new Vector3(48.5f, 25, 5);
        horizontalRot = Quaternion.Euler(0, -90, 0);
    }

    void Update()
    {
        if (player.isHorizontal)
        {
            Camera.main.transform.position = horizontalPos;
            Camera.main.transform.rotation = horizontalRot;
        }

        else
        {
            Camera.main.transform.position = verticalPos;
            Camera.main.transform.rotation = verticalRot;
        }
    }
}