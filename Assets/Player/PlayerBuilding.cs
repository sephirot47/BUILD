using UnityEngine;
using System.Collections;

public class PlayerBuilding : MonoBehaviour 
{
    public float buildRange = 10.0f;
    public static int buildLayerMask;

    public Buildable currentBuildablePrefab;
    private Buildable currentBuildable;
    private bool recentlyStartedBuilding;

	void Start () 
    {
        buildLayerMask = ( 1 << LayerMask.NameToLayer("BuildZone") );
	}
	
	void Update () 
    {
        if( !GSM.CurrentStateIs(GSM.Playing) ) return;

        if (Input.GetKeyUp(KeyCode.Backslash) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            DeselectCurrentBuildable();
        }

        Vector3 buildPoint = GetVisionRayPoint(); 
        if (buildPoint != Vector3.zero)
        {
            if(currentBuildable)
            {
                currentBuildable.SetInvisible(false);
                if(Input.GetMouseButtonUp(0) && !recentlyStartedBuilding)
                {
                    if (currentBuildable) currentBuildable.OnPlayerBuildDone(GetTargetBuildable());
                    currentBuildable = null;
                    //nOnBuildableChoosed(currentBuildablePrefab); //Reiniciamos (vuelve a aparecer el buildable para construir) 
                }
            }
        }
        else
        {
            if (currentBuildable) currentBuildable.SetInvisible(true);
        }
        recentlyStartedBuilding = false;
	}

    public void OnPausePhysics()
    {
    }

    public void OnResumePhysics()
    {
        DeselectCurrentBuildable();
    }

    public void DeselectCurrentBuildable()
    {
        if (currentBuildable) Destroy(currentBuildable.gameObject);
        currentBuildable = null;
    }

    public void OnBuildableChoosed(Buildable b)
    {
        DeselectCurrentBuildable();
        recentlyStartedBuilding = true;
        currentBuildablePrefab = b;
        currentBuildable = GameObject.Instantiate(currentBuildablePrefab);
        currentBuildable.SetMode(Buildable.Mode.Build);
    }

    public Ray GetViewRay()
    {
        Vector3 forward = GetComponentInChildren<Camera>().transform.forward;
        return new Ray(transform.position, forward);
    }

    public Vector3 GetVisionRayPoint()
    {
        Ray ray = GetViewRay();
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, buildRange, buildLayerMask)) return hit.point;
        return Vector3.zero;
    }

    public Vector3 GetVisionRayNormal()
    {
        Ray ray = GetViewRay();
        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, buildRange, buildLayerMask)) return hit.normal;
        return Vector3.zero;
    }

    public GameObject GetTargetBuildable()
    {
        Ray ray = GetViewRay();
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, buildRange, buildLayerMask)) return hit.collider.gameObject;
        return null;
    }
}
