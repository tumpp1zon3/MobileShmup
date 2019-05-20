using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0;

    Vector3 direction;
    Vector3 target;
    float targetDelay;
    bool predictMovement = false;

    void Update()
    {
        if(targetDelay > 0)
        {
            targetDelay -= Time.deltaTime;
            if(targetDelay <= 0)
            {
                direction = Vector3.Normalize(target - transform.position);
            }
        }
        if(direction != null && speed > 0) transform.Translate(direction * speed * Time.deltaTime);
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

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
