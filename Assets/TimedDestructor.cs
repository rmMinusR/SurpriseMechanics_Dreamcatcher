using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestructor : MonoBehaviour
{
    public bool isRunning;

    public GameObject target;

    public float maxLife;
    private float remainingLife;

    // Start is called before the first frame update
    void Start()
    {
        remainingLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning) remainingLife -= Time.deltaTime;

        if (remainingLife <= 0) Destroy( (target==null ? gameObject : target) );
    }
}
