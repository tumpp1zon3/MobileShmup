using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isHorizontal;

    private Transform player;

    private Vector3 pos;

    private float posX, posY;

    void Start()
    {
        player = gameObject.transform;

        posY = player.position.y;
    }

    void Update()
    {
        pos = new Vector3(posX, posY, player.position.z);

        player.position = pos;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHorizontal = !isHorizontal;
        }

        GetControls();
    }

    void GetControls()
    {
        if (!isHorizontal)
        {
            if (player.position.x >= -24f)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    posX -= 0.25f;
                }

                if (player.position.x < -24f)
                {
                    posX = -24f;
                }
            }

            if (player.position.x <= 24f)
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    posX += 0.25f;
                }

                if (player.position.x > 24f)
                {
                    posX = 24f;
                }
            }
        }

        if (isHorizontal)
        {
            if (player.position.y <= 38f)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    posY += 0.25f;
                }

                if (player.position.y > 38f)
                {
                    posY = 38f;
                }
            }

            if (player.position.y >= 12f)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    posY -= 0.25f;
                }

                if (player.position.y < 12f)
                {
                    posY = 12f;
                }
            }
        }
    }
}