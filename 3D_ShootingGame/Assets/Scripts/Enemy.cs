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

    private void Start()
    {
        patternCount = 0;
        isPatternOn = false;
        patterns = new List<pattern>
        {
            Pattern01
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
        StartCoroutine(nameof(Pattern1Make));
    }

    IEnumerator Pattern1Make()
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
        StartCoroutine(nameof(Pattern1Make));
        yield break;
    }

    public void Hit()
    {
        print("Enemy Hit!");
    }
}
