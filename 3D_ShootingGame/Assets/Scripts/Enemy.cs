using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
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

    public void Hit()
    {
        print("Enemy Hit!");
    }

    private void Start()
    {
        patternCount = 0;
        isPatternOn = false;
        patterns = new List<pattern>
        {
            Pattern01,
            Pattern02,
            Pattern03,
            Pattern04
            // Pattern05
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
        LoopPattern01();
    }
    public void LoopPattern01()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            return;
        }
        StartCoroutine(nameof(Pattern01MakeBullet));
        Invoke(nameof(LoopPattern01), 1f);
    }

    IEnumerator Pattern01MakeBullet()
    {
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

        patternCount++;
        yield break;
    }

    public void Pattern02()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 02");
        force = Vector3.right * Time.deltaTime * 2f;
        LoopPattern02();
    }

    public void LoopPattern02()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            return;
        }
        StartCoroutine(nameof(Pattern02MakeBullet));
        Invoke(nameof(LoopPattern02), 1f);
    }

    IEnumerator Pattern02MakeBullet()
    {
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

        patternCount++;   
        yield break;
    }

    public void Pattern03()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 03");
        force = Vector3.right * Time.deltaTime * 0.5f;
        LoopPattern03();
    }
    public void LoopPattern03()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            return;
        }
        StartCoroutine(nameof(Pattern03MakeBullet));
        Invoke(nameof(LoopPattern03), 0.5f);
    }

    IEnumerator Pattern03MakeBullet()
    {
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
        patternCount++;
        yield break;
    }

    public void Pattern04()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 03");
        force = Vector3.right * Time.deltaTime * 0.5f;
        LoopPattern04();
    }
    public void LoopPattern04()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            return;
        }
        if (patternCount % 3 == 0)
        {
            force = Vector3.right * Time.deltaTime * 1f;
            StartCoroutine(nameof(Pattern03MakeBullet));
        }
        else if (patternCount % 2 == 0)
        {
            force = Vector3.right * Time.deltaTime * 0.5f;
            StartCoroutine(nameof(Pattern02MakeBullet));
        }
        else
        {
            force = Vector3.right * Time.deltaTime * 2f;
            StartCoroutine(nameof(Pattern01MakeBullet));
        }

        Invoke(nameof(LoopPattern04), 0.5f);
    }

    public void Pattern05()
    {
        patternCount = 0;
        isPatternOn = true;
        print("call pattern 05");
        force = Vector3.right * Time.deltaTime * 0.5f;
        LoopPattern05();
    }
    public void LoopPattern05()
    {
        if (patternCount == 50)
        {
            isPatternOn = false;
            return;
        }
        StartCoroutine(nameof(Pattern05MakeBullet));
        Invoke(nameof(LoopPattern05), 0.5f);
    }

    IEnumerator Pattern05MakeBullet()
    {
        for (int i = 25; i <= 182; i += 10)
        {
            BulletManager.bulletManager.InstantiateEnemyBullet(
                
                transform.localPosition,

                Quaternion.Euler(
                    GetRoZ(transform.localPosition, player.transform.localPosition),
                    GetRoY(transform.localPosition, player.transform.localPosition),
                    GetRoX(transform.localPosition, player.transform.localPosition)
                )
            );

        }

        patternCount++;
        yield break;
    }

    private float GetRoX(Vector3 from, Vector3 to)
    {
        float angle = Mathf.Atan2(to.y = from.y, to.z - from.z) * 180 / Mathf.PI;
        return angle < 0 ? angle + 360 : angle;
    }
    private float GetRoY(Vector3 from, Vector3 to)
    {
        float angle = Mathf.Atan2(to.z = from.z, to.x - from.x) * 180 / Mathf.PI;
        return angle < 0 ? angle + 360 : angle;
    }
    private float GetRoZ(Vector3 from, Vector3 to)
    {
        float angle = Mathf.Atan2(to.y = from.y, to.x - from.x) * 180 / Mathf.PI;
        return angle < 0 ? angle + 360 : angle;
    }
}
