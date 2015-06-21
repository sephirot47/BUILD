using UnityEngine;
using System.Collections;

public class PlayerBuilding : MonoBehaviour 
{
    public float buildRange = 10.0f;
    public LayerMask buildGroundLayer;

    public Buildable currentBuildable;

	void Start () 
    {
	    
	}
	
	void Update () 
    {
        if(GC.inventory.IsOpen()) return;

        Vector3 buildPoint = GetBuildPoint();
        if(buildPoint != Vector3.zero)
        {
            if(Input.GetMouseButtonUp(0))
            {
                if(currentBuildable != null)
                {
                    GameObject.Instantiate(currentBuildable, buildPoint, Quaternion.identity);
                }
            }
        }
	}

    public Vector3 GetBuildPoint()
    {
        Vector3 forward = GetComponentInChildren<Camera>().transform.forward;
        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, buildRange, buildGroundLayer.value))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
