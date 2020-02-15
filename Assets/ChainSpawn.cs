using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpawn : MonoBehaviour
{
    public GameObject prototype;

    void Start()
    {
        GameObject o = Instantiate(prototype, transform.position, transform.rotation, transform.parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
