using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public Camera playerCamera;

    public Weapon[] weaponPrototypes;
    [Tooltip("Current weapon object in world")]
    public Weapon currentWeapon;

    private float screenDiagonal { get { return Mathf.Sqrt(Screen.width * Screen.height); } }

    public Transform attackParent;
    public Transform attackRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Apply targetting
        foreach (Targettable t in FindObjectsOfType<Targettable>()) t.isTargetted = false;
        Targettable currentTarget = GetTarget();
        if(currentTarget != null) currentTarget.isTargetted = true;

        //Set player rotation
        if(currentTarget != null)
        {
            //Point towards target
            Vector3 targetPos = currentTarget.transform.position;
            targetPos.y = gameObject.transform.position.y;
            gameObject.transform.rotation = Quaternion.LookRotation((targetPos-gameObject.transform.position).normalized);
        } else
        {
            //Point towards mouse
            Ray r = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r.origin, r.direction, out hit))
            {
                Vector3 targetPos = hit.point;
                targetPos.y = gameObject.transform.position.y;
                gameObject.transform.rotation = Quaternion.LookRotation((targetPos - gameObject.transform.position).normalized);
            }
        }

        //Try to input an attack into the state machine
        if(Input.GetButtonDown("Fire1")) TryInputAttack();
    }

    public Targettable GetTarget()
    {
        Targettable[] possibleTargets = FindObjectsOfType<Targettable>();
        if (possibleTargets.Length < 1) return null;

        Vector3 mousePos = Input.mousePosition;

        //Find closest targettable
        Targettable closest = possibleTargets[0];
        Vector3 closestOnScreen = playerCamera.WorldToScreenPoint(closest.transform.position); closestOnScreen.z = 0;
        foreach(Targettable t in possibleTargets)
        {
            Vector3 onScreen = playerCamera.WorldToScreenPoint(t.transform.position);
            onScreen.z = 0;

            //If closer than output, and within lockon range
            if(Vector3.Distance(onScreen, mousePos) < Vector3.Distance(closestOnScreen, mousePos) && Vector3.Distance(onScreen, mousePos) <= t.maximumLockonRange * screenDiagonal)
            {
                closest = t;
                closestOnScreen = onScreen;
            }
        }
        
        //If output is within lockon range, return it
        if (Vector3.Distance(closestOnScreen, mousePos) <= closest.maximumLockonRange * screenDiagonal)
        {
            return closest;
        } else
        {
            return null;
        }
    }

    public bool TryInputAttack()
    {
        if (currentWeapon == null) return false;
        if (!currentWeapon.GetComponent<Animator>().GetBool("o_canAttack")) return false;
        currentWeapon.GetComponent<Animator>().SetTrigger("attackInput");
        return true;
    }

    public bool TryCancelAttack()
    {
        if (currentWeapon == null) return false;
        if (!currentWeapon.GetComponent<Animator>().GetBool("o_canAttack")) return false;
        currentWeapon.GetComponent<Animator>().ResetTrigger("attackInput");
        return true;
    }
}
