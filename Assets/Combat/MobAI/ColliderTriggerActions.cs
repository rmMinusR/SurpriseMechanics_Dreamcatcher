using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderTriggerActions : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    void OnTriggerEnter(Collider collider)
    {
        onEnter.Invoke();
    }

    void OnTriggerExit(Collider collider)
    {
        onExit.Invoke();
    }
}
