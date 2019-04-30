using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform player;

    Vector3 pos;

    float posX, posY;

    void Start()
    {
        player = gameObject.transform;

        posY = player.position.y;
    }

    void Update()
    {
        pos = new Vector3(posX, posY, player.position.z);

        player.position = pos;

        if (!ChangePerspective.cp.toggleHorizontal)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                posX -= 0.25f;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                posX += 0.25f;
            }
        }

        if (ChangePerspective.cp.toggleHorizontal)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                posY += 0.25f;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                posY -= 0.25f;
            }
        }
    }
}
