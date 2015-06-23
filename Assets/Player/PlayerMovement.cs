using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public static float gravity = -0.1f;

    public float speed = 20.0f;

    public float rotSpeedX = 240.0f;
    public float rotSpeedY = 80.0f;
    public float minRotY = -75.0f;
    public float maxRotY = 75.0f;

    private Vector3 movement;
    private Vector3 rotation;

	void Start () 
    {
	    
	}
	
	void Update () 
    {
        if (!GSM.CurrentStateIs(GSM.Playing)) return;

        HandleLooking();
        HandleMovement();
	}

    private void HandleLooking()
    {
        float lookX =  Input.GetAxis("Mouse X") * rotSpeedX * Time.deltaTime;
        float lookY = -Input.GetAxis("Mouse Y") * rotSpeedY * Time.deltaTime;

        rotation += new Vector3(lookY, lookX, 0.0f);
        rotation.x = Mathf.Clamp(rotation.x, minRotY, maxRotY);

        GetComponentInChildren<Camera>().transform.rotation = Quaternion.Euler(rotation);
    }

    private void HandleMovement()
    {
        movement.x = movement.z = 0.0f;
        if (IsGrounded()) movement.y = 0.0f;
        else movement.y += gravity * Time.deltaTime;

        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Vector3 forward = P(GetComponentInChildren<Camera>().transform.forward);
        Vector3 right =  P(GetComponentInChildren<Camera>().transform.right);

        movement += (axisX * speed * Time.deltaTime) * right;
        movement += (axisY * speed * Time.deltaTime) * forward;

        GetComponent<CharacterController>().Move(movement);
    }

    private bool IsGrounded()
    {
        return GetComponent<CharacterController>().isGrounded;
    }

    private Vector3 P(Vector3 v) { return new Vector3(v.x, 0.0f, v.z); }
}
