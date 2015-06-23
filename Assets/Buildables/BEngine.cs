using UnityEngine;
using System.Collections;

public class BEngine : Accessory 
{
    public float force = 350.0f;
    public Vector3 rotationAxis = Vector3.up;
    public bool relativeRotationAxis = false;

    public override void Start()
    {
        base.Start();
	}

    public override void Update() 
    {
        base.Update();
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ApplyAccessoryEffect(Buildable b)
    {
        Rigidbody rb = b.GetComponent<Rigidbody>();
        if(rb && rotationAxis != Vector3.zero)
        {
            Vector3 axis = rotationAxis;
            if (relativeRotationAxis) axis = transform.rotation * axis;

            rb.AddTorque(axis * force * Time.deltaTime);
        }
    }

}
