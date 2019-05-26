using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed = 0;
    public float CollisionRadius = 0.5f;
    public PlayerController player;
    public Transform enemyContainer;
    public float boundary = 40;
    bool IsPlayer;
    Vector3 direction;
    Vector3 target;
    float targetDelay;
    bool predictMovement = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        enemyContainer = player.EnemyContainer;
    }

    void Update()
    {
        if(targetDelay > 0)
        {
            targetDelay -= Time.deltaTime;
            if(targetDelay <= 0)
            {
                direction = Vector3.Normalize(target - transform.position);
                targetDelay = 100;
            }
        }
        if(direction != null && speed > 0) transform.Translate(direction * speed * Time.deltaTime);
        if (IsPlayer)
        {
            Transform enemy;
            for (int i = 0; i < enemyContainer.childCount; i++)
            {
                enemy = enemyContainer.GetChild(i);
                if (enemy.name == "Boss")
                {
                    for(int i1 = 0; i1 < enemy.childCount; i1++)
                    {
                        EnemyController ec = enemy.GetChild(i1).GetComponent<EnemyController>();
                        if (CheckCollision(transform.position, CollisionRadius, ec.GetPos(), ec.CollisionRadius, ec.SafeX, ec.SafeY))
                        {
                            Destroy(ec.gameObject);
                            Destroy(gameObject);
                        }
                    }
                }
                else
                {
                    EnemyController ec = enemy.GetComponent<EnemyController>();
                    if (CheckCollision(transform.position, CollisionRadius, ec.GetPos(), ec.CollisionRadius, ec.SafeX, ec.SafeY))
                    {
                        Destroy(ec.gameObject);
                        Destroy(gameObject);
                    }
                }
                
            }
        }
        else
        {
            if(CheckCollision(transform.position, CollisionRadius , player.GetPos(), player.GetRad()))
            {
                SceneManager.LoadScene(0);
            }
        }
        CheckBoundaries();
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetDirection(Vector3 target, bool predictMovement)
    {
        direction = Vector3.Normalize(target - transform.position);
        this.predictMovement = predictMovement;
    }

    public void SetDirection(Vector3 direction, Vector3 target, float delay, bool predictMovement)
    {
        this.direction = direction;
        this.target = target;
        targetDelay = delay;
        this.predictMovement = predictMovement;
    }

    public void SetPlayer(bool p)
    {
        IsPlayer = p;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


    public bool CheckCollision(Vector3 pos, float radius, Vector3 otherPos, float otherRadius)
    {
        if (pos.z - radius < otherPos.z + otherRadius && pos.z + radius > otherPos.z - otherRadius)
        {
            if (player.isHorizontal)
            {
                //only y
                if(pos.y - radius < otherPos.y + otherRadius && pos.y + radius > otherPos.y - otherRadius)
                {
                    pos.x = 0;
                    otherPos.x = 0;
                    if(Vector3.Distance(pos,otherPos) < radius + otherRadius)
                    {
                        return true;
                    }
                }
            }
            else
            {
                //only x
                if (pos.x - radius < otherPos.x + otherRadius && pos.x + radius > otherPos.x - otherRadius)
                {
                    pos.y = 0;
                    otherPos.y = 0;
                    if (Vector3.Distance(pos, otherPos) < radius + otherRadius)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool CheckCollision(Vector3 pos, float radius, Vector3 otherPos, float otherRadius, bool safeX, bool safeY)
    {
        if (pos.z - radius < otherPos.z + otherRadius && pos.z + radius > otherPos.z - otherRadius)
        {
            if (player.isHorizontal)
            {
                //only y
                if (!safeY && pos.y - radius < otherPos.y + otherRadius && pos.y + radius > otherPos.y - otherRadius)
                {
                    pos.x = 0;
                    otherPos.x = 0;
                    if (Vector3.Distance(pos, otherPos) < radius + otherRadius)
                    {
                        return true;
                    }
                }
            }
            else
            {
                //only x
                if (!safeX && pos.x - radius < otherPos.x + otherRadius && pos.x + radius > otherPos.x - otherRadius)
                {
                    pos.y = 0;
                    otherPos.y = 0;
                    if (Vector3.Distance(pos, otherPos) < radius + otherRadius)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void CheckBoundaries()
    {
        for(int i = 0; i < 3; i++)
        {
            float axis = 0;
            switch (i)
            {
                case 0:
                    axis = transform.position.x;
                    break;
                case 1:
                    axis = transform.position.y;
                    break;
                case 2:
                    axis = transform.position.z;
                    break;
            }
            if(axis < -boundary || axis > boundary)
            {
                Destroy(gameObject);
            }
        }
    }
}
