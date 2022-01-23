using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float destroyBound = -10.0f;
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (transform.position.z < destroyBound )
            Destroy(gameObject);
    }
}