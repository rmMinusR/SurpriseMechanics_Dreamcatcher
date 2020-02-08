using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targettable : MonoBehaviour
{
    public Sprite renderSpriteTargetted;
    public Sprite renderSpriteUntargetted;

    public SpriteRenderer renderTarget;

    //Measurement unit in screen diagonals
    public float maximumLockonRange = 0.1f;

    public Rigidbody knockbackRigidbody;
    public float health = 1;

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

    public void DoDamage(float damage)
    {
        DoDamage(damage, 0, Vector3.zero);
    }

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

    public void DoDie()
    {
        Destroy(gameObject);
    }
}
