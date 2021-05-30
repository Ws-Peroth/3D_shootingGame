﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    private void Start()
    {
        bulletSpeed = 10f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) return;
        if (other.gameObject.layer == 9) other.gameObject.GetComponent<Player>().Hit();
        if (other.gameObject.layer == 10) return;

        BulletManager.bulletManager.DestroyEnemyBullet(gameObject);
        // Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
    }
}
