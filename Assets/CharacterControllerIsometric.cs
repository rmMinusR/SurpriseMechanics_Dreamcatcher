using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CharacterControllerIsometric : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float sprintModifier = 1.5f;

    public AnimationCurve accelerationCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public float accelerationTime = 0.1f;

    public float accelStatus = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate input
        Vector3 input = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        //Set input maximum magnitude to 1
        Vector3 nrmInput = input.normalized;// * Mathf.Min(1, input.magnitude);

        //Update curves
        if (input.magnitude > 0.1f)
        {
            accelStatus += Time.deltaTime / accelerationTime;
            if (accelStatus == float.NaN) accelStatus = 1;
        } else
        {
            accelStatus -= Time.deltaTime / accelerationTime;
            if (accelStatus == float.NaN) accelStatus = 0;
        }
        accelStatus = Mathf.Clamp(accelStatus, 0, 1);

        //Calculate movement
        Vector3 movement = (input.magnitude>0.1f ? nrmInput: GetComponent<Rigidbody>().velocity.normalized) * moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintModifier : 1);
        
        //Scale movement to the curve
        movement *= accelerationCurve.Evaluate(accelStatus);

        //Apply movement
        GetComponent<Rigidbody>().velocity = movement;
    }
}
