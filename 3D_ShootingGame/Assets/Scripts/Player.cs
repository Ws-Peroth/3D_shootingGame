using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject mainCamera;
    public float moveSpeed;
    public float bulletSpeed;
    public float AttakDelay;
    public bool isAttack;
    public int level;

    private void Start()
    {
        level = 1;
        moveSpeed = 10f;
        bulletSpeed = 70f;
        AttakDelay = 0.1f;
        isAttack = false;
        gameObject.transform.rotation = Quaternion.identity;
        mainCamera.transform.rotation = Quaternion.identity;
        mainCamera.transform.Rotate(new Vector3(15, -90, 0));
    }
    void Update()
    {
        IsDash();
        Move();
        Attack();
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    public void IsDash()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = 30f;
        }
        else
        {
            moveSpeed = 10f;
        }
    }

    public void Attack()
    {
        if (Input.GetKey(KeyCode.F) && !isAttack)
        {
            isAttack = true;
            InstantiateBulletByLevel();            
            Invoke(nameof(ResetAttackDelay), AttakDelay);
        }
    }

    public void InstantiateBulletByLevel()
    {
        if(level == 1)
        {
            GameObject bullet1 = 
                BulletManager.bulletManager.InstantiatePlayerBullet(
                    transform.position + new Vector3(0, -1, 0),
                    Quaternion.identity
                );
            bullet1.GetComponent<PlayerBullet>().moveVector = Vector3.left;
        }
        if (level == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject bullet =
                    BulletManager.bulletManager.InstantiatePlayerBullet(
                        transform.position + new Vector3(0, -1, i - 1),
                        Quaternion.identity
                    );
                bullet.GetComponent<PlayerBullet>().moveVector = Vector3.left;
            }

        }
        if (level == 3)
        {
            for(int i = 0; i < 5; i++)
            {
                GameObject bullet =
                BulletManager.bulletManager.InstantiatePlayerBullet(
                    transform.position + new Vector3(0, -1, i - 2),
                    Quaternion.identity
                );
                bullet.GetComponent<PlayerBullet>().moveVector = Vector3.left;
            }
        }
    }

    public void ResetAttackDelay()
    {
        isAttack = false;
    }

    public void Hit()
    {
        print("Palyer Hit!");
    }
}
