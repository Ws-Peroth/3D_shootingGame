using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isPatternOn;
    public delegate void pattern();
    public List<pattern> patterns;

    private void Start()
    {
        isPatternOn = false;
        patterns = new List<pattern>
        {
            Pattern01
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isPatternOn)
            {
                CallPattern();
            }
        }
    }

    public void CallPattern()
    {
        int selectPattern = Random.Range(0, patterns.Count);
        patterns[selectPattern]();
    }

    public void Pattern01()
    {
        isPatternOn = true;
        print("call pattern 01");
    }
}
