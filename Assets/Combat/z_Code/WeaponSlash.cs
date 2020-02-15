using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponSlash : MonoBehaviour
{
    [Tooltip("How long this hitbox persists, in seconds")]
    public float timeOfAffect = 1f;
    //Lifetime remaining, in seconds
    private float timeRemaining;

    [Tooltip("Damage to deal")]
    public float damage;
    [Tooltip("Knockback to deal")]
    public float knockback;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        timeRemaining = timeOfAffect;
        affectedEnemies = new List<Targettable>();
    }

    //Enemies which have already been affected by this hitbox
    private List<Targettable> affectedEnemies;

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) Destroy(gameObject);
    }

    /// <summary>
    /// When an entity enters the trigger, apply damage and knockback, and mark it as processed.
    /// </summary>
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
