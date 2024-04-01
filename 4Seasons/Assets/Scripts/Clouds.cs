using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float limit = 10.0f;
    [SerializeField] float direction = 1.0f;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);

        if (transform.position.x > limit || transform.position.x < -limit)
        {
            direction *= -1;
        }
    }
}
