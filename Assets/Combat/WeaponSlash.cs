using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponSlash : MonoBehaviour
{
    public float timeOfAffect = 1f;
    private float timeRemaining;

    public float damage, knockback;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        timeRemaining = timeOfAffect;
        affectedEnemies = new List<Targettable>();
    }

    private List<Targettable> affectedEnemies;

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Targettable t = other.GetComponent<Targettable>();
        if(t != null)
        {
            if (!affectedEnemies.Contains(t))
            {
                affectedEnemies.Add(t);
                t.DoDamage(damage, knockback, FindObjectOfType<PlayerCombatController>().transform.position);
            }
        }
    }
}
