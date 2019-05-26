using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ChangePerspective perspective;
    public float CollisionRadius;
    public bool isHorizontal;

    private Transform player;
    private Vector3 pos;
    private float posX, posY;

    public Transform EnemyContainer;

    void Start()
    {
        player = gameObject.transform;
        posY = player.position.y;
    }

    void Update()
    {
        pos = new Vector3(posX, posY, player.position.z);
        player.position = pos;

        isHorizontal = perspective.isHorizontal;

        if (isHorizontal)
        {
            posX = 0;
        }

        if (!isHorizontal)
        {
            posY = 26;
        }
    }

    public void MoveLeft()
    {
        if (player.position.x >= -13f)
        {
            posX--;

            if (player.position.x < -13f)
            {
                posX = -13f;
            }
        }
    }

    public void MoveRight()
    {
        if (player.position.x <= 13f)
        {
            posX++;

            if (player.position.x > 13f)
            {
                posX = 13f;
            }
        }
    }

    public void MoveUp()
    {
        if (player.position.y <= 55f)
        {
            posY++;

            if (player.position.y > 55f)
            {
                posY = 55f;
            }
        }
    }

    public void MoveDown()
    {
        if (player.position.y >= 0)
        {
            posY--;

            if (player.position.y < 0)
            {
                posY = 0;
            }
        }
    }

    public Vector3 GetPos()
    {
        return pos;
    }

    public float GetRad()
    {
        return CollisionRadius;
    }
}