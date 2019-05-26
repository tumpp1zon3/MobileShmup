using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject BulletPrefab;

    //Wait time between shots
    public float Interval = 0;
    private float IntervalTimer = 0;
    private float InrervalError = 0;
    //Speed of Bullets
    public float BulletSpeed = 0;

    //How many Bullets can be spawned between reloads 0 = infinite
    public int ClipShotCount = 0;
    private int ShotCount = 0;

    //Wait time after ShotCount
    public float ReloadTime = 0;
    private float ReloadTimer = 0;
    //How many Reloads can be used 0 = infinite (after clips can still be manually started again)
    public int ClipCount = 0;
    private int ReloadCount = 0;

    //How many bullet is shot simultaneusly
    public int SpreadAmount = 1;
    //Total angle around BulletDirection that spreadshot shoots bullets
    public float SpreadAngle = 0;

    //Direction of Bullets
    public Vector3 BulletDirection;
    
    public bool useTarget;
    //Bullet target
    public GameObject Target;
    //Deley in seconds before Bullet Changes direction to target (best for targeted Spread Shots)
    public float TargetDelay = 0;
    //Delay in seconds before Shooting
    public float StartDelay = 0;

    //For Manual Start change true in editor and call StartShooting() or change false to Start Shooting 
    public bool ManualStart = false;

    //Boolean for collision so bullets can not frindly fire enemies or player shoot itself
    public bool IsPlayer = false;

    private void Start()
    {
        if (useTarget)
        {
            Target = FindObjectOfType<PlayerController>().gameObject;
        }
    }

    void Update()
    {
        if (!ManualStart)
        {
            if(CheckTimer(ref StartDelay))
            {
                if(CheckTimer(ref ReloadTimer))
                {
                    if(CheckTimer(ref IntervalTimer))
                    {
                        if(ClipShotCount != 0 && ShotCount == 0 && ReloadCount == 0 && IntervalTimer == 0)
                        {
                            InrervalError = StartDelay * -1;
                        }
                        else if(ShotCount == 0)
                        {
                            InrervalError = ReloadTimer * -1;
                        }
                        else
                        {
                            InrervalError = IntervalTimer * -1;
                        }
                        float StartAngle = SpreadAngle/2*-1;
                        for(int i = 0; i < SpreadAmount; i++)
                        {
                            if(SpreadAmount != 1)
                            {
                                Vector3 SpreadDirection = Quaternion.Euler(StartAngle + i * (SpreadAngle / (SpreadAmount-1)), StartAngle + i * (SpreadAngle / (SpreadAmount - 1)), 0) * BulletDirection;
                                SpawnBullet(transform.position + (SpreadDirection * InrervalError), transform.rotation , SpreadDirection);
                            }
                            else
                            {
                                SpawnBullet(transform.position + (BulletDirection * InrervalError), transform.rotation, BulletDirection);
                            }
                        }
                        ShotCount++;
                        if (ClipShotCount != 0 && ShotCount == ClipShotCount)
                        {
                            ReloadCount++;
                            if (ClipCount != 0 && ReloadCount == ClipCount)
                            {
                                ManualStart = true;
                                ReloadCount = 0;
                                ShotCount = 0;
                            }
                            else
                            {
                                ReloadTimer = ReloadTime;
                                ShotCount = 0;
                            }
                        }
                        else
                        {
                            IntervalTimer = Interval;
                        }
                    }
                }
            }
        }
    }
    
    private bool CheckTimer(ref float timer)
    {
        if(timer < 0)
        {
            return true;
        }
        else
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation, Vector3 direction)
    {
        GameObject bullet = Instantiate(BulletPrefab, position, rotation);
        Bullet b = bullet.GetComponent<Bullet>();
        if (Target != null)
        {
            if (TargetDelay != 0)
            {
                b.SetDirection(direction, Target.transform.position, TargetDelay, false);
            }
            else
            {
                b.SetDirection(Target.transform.position, false);
            }
        }
        else
        {
            b.SetDirection(direction);
        }
        b.SetSpeed(BulletSpeed);
        b.SetPlayer(IsPlayer);
    }

    public void StartShooting()
    {
        ManualStart = false;
    }
}
