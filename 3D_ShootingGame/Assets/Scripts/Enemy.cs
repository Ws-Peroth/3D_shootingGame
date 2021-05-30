using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isPatternOn;
    public delegate void pattern();
    public List<pattern> patterns;
    public GameObject enemyBullet;
    public int patternCount;
    Vector3 force;

    readonly float fieldSizeMinX = -340f;
    readonly float fieldSizeMaxX = 57f;

    readonly float fieldSizeMinY = 25f;
    readonly float fieldSizeMaxY = 182f;

    readonly float fieldSizeMinZ = -68f;
    readonly float fieldSizeMaxZ = 55;

    // field Size
    // x : -340 ~ 57
    // y : 25 ~ 182
    // z : -68 ~ 55;
    private void Start()
    {
        patternCount = 0;
        isPatternOn = false;
        patterns = new List<pattern>
        {
            Pattern01,
            Pattern02,
            Pattern03
        };
    }

    private void Update()
    {
            if (BulletManager.bulletManager.isInstantiateEnd && !isPatternOn)
            {
                CallPattern();
            }
    }

    public void CallPattern()
    {
        int selectPattern = Random.Range(0, patterns.Count);
        patterns[selectPattern]();
    }

    public void Pattern01()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 01");
        force = Vector3.right * Time.deltaTime * 1f;
        StartCoroutine(nameof(Pattern01MakeBullet));
    }

    IEnumerator Pattern01MakeBullet()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            yield break;
        }

        for (float j = 0 + patternCount + Random.Range(-90.0f, 90.0f) * 3.6f; j < 360; j += 36)
        {
            for (float i = 0 + patternCount + Random.Range(-90.0f, 90.0f) * 3.6f; i < 360; i += 36)
            {
                BulletManager.bulletManager.InstantiateEnemyBullet(
                        transform.position + new Vector3(0, 5, 0),
                        Quaternion.Euler(j, i, 0)
                    );
            }
        }

        yield return new WaitForSeconds(0.5f);
        patternCount++;
        StartCoroutine(nameof(Pattern01MakeBullet));
        yield break;
    }

    public void Pattern02()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 02");
        force = Vector3.right * Time.deltaTime * 2f;
        StartCoroutine(nameof(Pattern02MakeBullet));
    }

    IEnumerator Pattern02MakeBullet()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            yield break;
        }

        for (int i = 25; i <= 182; i+=10)
        {
            for(int j = -68; j <=55; j+=10)
            {
                BulletManager.bulletManager.InstantiateEnemyBullet(
                    new Vector3(transform.localPosition.x, i, j),
                    Quaternion.Euler(0, 90, 0)
                );
            }
        }

        yield return new WaitForSeconds(1f);
        patternCount++;
        StartCoroutine(nameof(Pattern02MakeBullet));
        yield break;
    }

    public void Pattern03()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 03");
        force = Vector3.right * Time.deltaTime * 0.5f;
        StartCoroutine(nameof(Pattern03MakeBullet));
    }

    IEnumerator Pattern03MakeBullet()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            yield break;
        }

        float positionX = Random.Range(fieldSizeMinX, fieldSizeMaxX);
        float positionY = Random.Range(fieldSizeMinY, fieldSizeMaxY);
        float positionZ = Random.Range(fieldSizeMinZ, fieldSizeMaxZ);

        for (float j = 0 + patternCount * 3.6f; j < 360; j += 36)
        {
            for (float i = 0 + patternCount * 3.6f; i < 360; i += 36)
            {
                BulletManager.bulletManager.InstantiateEnemyBullet(
                        new Vector3(positionX, positionY, positionZ),
                        Quaternion.Euler(j, i, 0)
                    );
            }
        }

        yield return new WaitForSeconds(0.5f);
        patternCount++;
        StartCoroutine(nameof(Pattern03MakeBullet));
        yield break;
    }

    public void Hit()
    {
        print("Enemy Hit!");
    }


}
