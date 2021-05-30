using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager bulletManager;
    
    public Queue<GameObject> enemyBulletPool;
    public Queue<GameObject> playerBulletPool;

    public GameObject enemyBulletPrefeb;
    public GameObject PlayerBulletPrefeb;

    public bool isInstantiateEnd;

    void Start()
    {
        if (bulletManager == null)
        {
            bulletManager = this;
        }
        
        isInstantiateEnd = false;
        enemyBulletPool = new Queue<GameObject>();
        playerBulletPool = new Queue<GameObject>();

        for (int i = 0; i < 6000; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefeb);
            enemyBulletPool.Enqueue(bullet);
            bullet.SetActive(false);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject bullet = Instantiate(PlayerBulletPrefeb);
            playerBulletPool.Enqueue(bullet);
            bullet.SetActive(false);
        }

        isInstantiateEnd = true;
    }

    public GameObject InstantiatePlayerBullet()
    {
        GameObject returnObj = null;

        if (playerBulletPool.Count > 0)
            returnObj = playerBulletPool.Dequeue();

        if (returnObj == null)
            returnObj = Instantiate(PlayerBulletPrefeb);

        returnObj.SetActive(true);

        return returnObj;
    }

    public GameObject InstantiatePlayerBullet(Vector3 position, Quaternion rotation)
    {
        GameObject returnObj = null;

        if (playerBulletPool.Count > 0)
            returnObj = playerBulletPool.Dequeue();

        if (returnObj == null)
            returnObj = Instantiate(PlayerBulletPrefeb);

        returnObj.SetActive(true);

        returnObj.transform.position = position;
        returnObj.transform.rotation = rotation;

        return returnObj;
    }
    public GameObject InstantiateEnemyBullet()
    {
        GameObject returnObj = null;

        if (enemyBulletPool.Count > 0)
            returnObj = enemyBulletPool.Dequeue();

        if (returnObj == null)
            returnObj = Instantiate(enemyBulletPrefeb);

        returnObj.SetActive(true);

        returnObj.GetComponent<EnemyBullet>().bulletSpeed = 10f;

        return returnObj;
    }

    public GameObject InstantiateEnemyBullet(Vector3 position, Quaternion rotation)
    {
        GameObject returnObj = null;

        if (enemyBulletPool.Count > 0)
            returnObj = enemyBulletPool.Dequeue();

        if (returnObj == null)
            returnObj = Instantiate(enemyBulletPrefeb);

        returnObj.SetActive(true);

        returnObj.transform.position = position;
        returnObj.transform.rotation = rotation;
        returnObj.GetComponent<EnemyBullet>().bulletSpeed = 10f;
        return returnObj;
    }

    public void DestroyPalyerBullet(GameObject bullet)
    {
        bullet.GetComponent<PlayerBullet>().moveVector = Vector3.zero;
        bullet.transform.Translate(Vector3.zero);
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;

        playerBulletPool.Enqueue(bullet);
        bullet.SetActive(false);
    }

    public void DestroyEnemyBullet(GameObject bullet)
    {
        bullet.GetComponent<EnemyBullet>().bulletSpeed = 0;
        bullet.transform.Translate(Vector3.zero);
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;

        enemyBulletPool.Enqueue(bullet);
        bullet.SetActive(false);
    }
}
