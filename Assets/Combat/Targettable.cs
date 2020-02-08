using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targettable : MonoBehaviour
{
    [Tooltip("Sprite when not targetted")]
    public Sprite renderSpriteTargetted;
    [Tooltip("Sprite when targetted")]
    public Sprite renderSpriteUntargetted;

    [Tooltip("Render target for targetting UI")]
    public SpriteRenderer renderTarget;

    [Tooltip("Measurement unit in screen diagonals")]
    public float maximumLockonRange = 0.1f;

    [Tooltip("The rigidbody to be knocked back")]
    public Rigidbody knockbackRigidbody;

    [Tooltip("Health pool of this entity")]
    public float health = 1;

    //Is this entity targetted by the player?
    public bool isTargetted {
        get {
            return renderTarget.sprite == renderSpriteUntargetted;
        }

        set {
            renderTarget.sprite = value ? renderSpriteTargetted : renderSpriteUntargetted;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderTarget.sprite = renderSpriteUntargetted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Deal damage to this entity. Delegates to DoDamage(float, float, Vector3)
    /// </summary>
    /// <param name="damage">Damage to deal</param>
    public void DoDamage(float damage)
    {
        DoDamage(damage, 0, Vector3.zero);
    }
    
    /// <summary>
    /// Deal damage to this entity. Can also deal knockback.
    /// </summary>
    /// <param name="damage">Damage to deal</param>
    /// <param name="knockback">Knockback to apply. Can be 0 or negative, but won't be applied.</param>
    /// <param name="knockbackSource">Where the knockback is coming from. Set to null and it won't be applied.</param>
    public void DoDamage(float damage, float knockback, Vector3 knockbackSource)
    {
        //TODO hurt anim
        health -= damage;

        if (knockback > 0 && knockbackSource != null)
        {
            Debug.Log((transform.position - knockbackSource).normalized * knockback);
            knockbackRigidbody.velocity = ((transform.position-knockbackSource).normalized * knockback);
        }

        if (health <= 0) DoDie();
    }

    /// <summary>
    /// Kills this entity. TODO: Switch to AnimatorController state machine.
    /// </summary>
    public void DoDie()
    {
        Destroy(gameObject);
    }
}
