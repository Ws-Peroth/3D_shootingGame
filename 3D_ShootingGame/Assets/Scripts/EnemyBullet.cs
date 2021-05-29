using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) return;
        if (other.gameObject.name == "Enemy") return;

        Destroy(gameObject);
    }
}
