using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public Camera playerCamera;

    public Weapon[] weaponPrototypes;
    public int selectedWeapon = 0;
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

    /// <summary>
    /// Get the entity that the player is currently targetting (cursor). If player is not targetting anything, returns null.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Try to send an attack-input signal to the state machine
    /// </summary>
    /// <returns></returns>
    public bool TryInputAttack()
    {
        if (currentWeapon == null) return false;
        if (!currentWeapon.GetComponent<Animator>().GetBool("o_canAttack")) return false;
        currentWeapon.GetComponent<Animator>().SetTrigger("attackInput");
        return true;
    }

    /// <summary>
    /// Try to cancel an attack-input signal
    /// </summary>
    public bool TryCancelAttack()
    {
        if (currentWeapon == null) return false;
        if (!currentWeapon.GetComponent<Animator>().GetBool("o_canAttack")) return false;
        currentWeapon.GetComponent<Animator>().ResetTrigger("attackInput");
        return true;
    }

    /// <summary>
    /// Try to swap to a weapon
    /// </summary>
    /// <param name="weapon">Weapon index</param>
    public bool SwapWeapon(int weapon)
    {
        //Assert that $weapon is within bounds of array
        if (weapon >= weaponPrototypes.Length) return false;

        //Destroy the old $currentWeapon, if it exists
        if (currentWeapon != null) Destroy(currentWeapon.gameObject);

        //Instantiate the new $currentWeapon
        selectedWeapon = weapon;
        
        GameObject o = Instantiate(weaponPrototypes[selectedWeapon].gameObject, transform);
        o.transform.rotation = weaponPrototypes[selectedWeapon].transform.rotation * o.transform.rotation;
        o.transform.position =  o.transform.right     * weaponPrototypes[selectedWeapon].transform.position.x +
                                o.transform.up        * weaponPrototypes[selectedWeapon].transform.position.y +
                                o.transform.forward   * weaponPrototypes[selectedWeapon].transform.position.z +
                                transform.position;

        //Link it to the CombatController
        currentWeapon = o.GetComponent<Weapon>();

        return true;
    }

    /// <summary>
    /// Void version of SwapWeapon. Try to swap to a weapon by index.
    /// </summary>
    /// <param name="weapon">Weapon index</param>
    public void v_SwapWeapon(int weapon)
    {
        SwapWeapon(weapon);
    }

}
