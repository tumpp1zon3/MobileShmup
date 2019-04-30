using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Vector3 speed = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 direction = new Vector3(0, 0, 0);

    private bool isLinkedToCamera = false;

    private bool isLooping = true;

    [SerializeField]
    private float timer;

    private float defaultTimer;

    private List<SpriteRenderer> backgroundPart;

    void Start()
    {
        ScrollStart();
    }

    void Update()
    {
        ScrollUpdate();
    }

    void ScrollStart()
    {
        if (isLooping)
        {
            backgroundPart = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                if (r != null)
                {
                    backgroundPart.Add(r);
                }
            }

            backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x).ToList();
        }
    }

    void ScrollUpdate()
    {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, speed.z * direction.z);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping)
        {
            timer = timer - Time.deltaTime;

            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                if (firstChild.transform.position.x < cam.transform.position.x)
                {
                    if (firstChild.IsVisibleFrom(cam) == false && timer < 0)
                    {
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);

                        timer = defaultTimer;
                    }
                }
            }
        }
    }
}