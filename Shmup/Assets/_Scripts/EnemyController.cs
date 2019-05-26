using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool SafeX = false;
    public bool SafeY = false;

    public float speed = 3;
    public float CollisionRadius = 0.5f;

    public float SinPos = 0;
    public float WaveHeight = 1;
    public float WavesPerSecond = 1;
    public float StartPosIn = 0;

    public float AutoDestroyTime = 15;

    public float ReverseMovementTimer = 0;

    public float StopMovementTimer = 0;
    public float StopLength = 1;

    private BulletSpawner[] bulletSpawners;

    private float t;

    private Vector3 startPos;

    private bool stopShoot = false;

    private void Start()
    {
        startPos = transform.position;
        bulletSpawners = transform.GetComponentsInChildren<BulletSpawner>();
    }

    private void Update()
    {
        t += Time.deltaTime;
        Move();
        if(AutoDestroyTime != 0 && t > AutoDestroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        SinPos = WaveHeight * Mathf.Sin(2*Mathf.PI* WavesPerSecond * t+ StartPosIn);
        if(StopMovementTimer == 0 || (StopMovementTimer > t || StopMovementTimer + StopLength < t))
        {
            if(ReverseMovementTimer != 0 && ReverseMovementTimer < t)
            {
                transform.position = new Vector3(startPos.x + SinPos, startPos.y + SinPos, transform.position.z + Time.deltaTime * speed);
            }
            else
            {
                transform.position = new Vector3(startPos.x + SinPos, startPos.y + SinPos, transform.position.z + Time.deltaTime * -speed);
            }
        }
        else
        {
            if (!stopShoot)
            {
                bulletSpawners[0].ManualStart = true;
                bulletSpawners[1].ManualStart = false;
                stopShoot = true;
            }
        }
        
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
