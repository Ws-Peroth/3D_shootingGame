using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector3 moveVector = Vector3.zero;
    float moveSpeed = 50f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) return;
        if (other.gameObject.layer == 9) return;
        if (other.gameObject.layer == 10) other.gameObject.GetComponent<Enemy>().Hit();

        BulletManager.bulletManager.DestroyPalyerBullet(gameObject);
        // Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime);
    }
}
