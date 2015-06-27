using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buildable : MonoBehaviour, IGameStateListener
{
    public enum Mode
    {
        Build, Over, Select, Destroy, Default
    };

    public float mass = 1.0f;
    public float anchorOffsetScale = 0.0f;
    
    public Material buildMaterial, overMaterial, selectMaterial, destroyMaterial;
    public Material defaultMaterial;

    public ContextualMenu contextualMenuPrefab;
    private ContextualMenu cmenu;

    private Mode currentMode = Mode.Build;

    public List<Buildable> attachedBuildables;

    public virtual void Start()
    {
        transform.position = GetBuildPosition();
        attachedBuildables = new List<Buildable>();
	}

    public virtual void Update() 
    {
        HandlePositioning();
	}

    public void OnContextualMenuClosed()
    {
        cmenu = null;
    }

    void HandlePositioning()
    {
        if (currentMode == Mode.Build)
        {
            transform.position = GetBuildPosition();
            transform.rotation = GetBuildRotation();
        }
    }

    ////////////////////////////////////////////////
    public void OnMouseIsOver()
    {
        Debug.Log("Over: " + name);
        if(!IsSelected())
            SetMode(Mode.Over);
    }

    public void OnLeftClick()
    {
        if (!IsSelected())
            SetMode(Mode.Select);
        else SetMode(Mode.Over);
    }

    public void OnRightClick()
    {
        if (contextualMenuPrefab && GSM.CurrentStateIs(GSM.Playing))
        {
            cmenu = Instantiate(contextualMenuPrefab);
            cmenu.parentBuildable = this;
        }
    }

    public void OnMiddleClick()
    {
        this.Destroy();
    }

    public void OnClickOutside()
    {
        SetMode(Mode.Default);
    }

    public void OnMouseIsOut()
    {
        if(!IsSelected())
            SetMode(Mode.Default);
    }

    public bool IsSelected()
    {
        return currentMode == Mode.Select;
    }

    ////////////////////////////////////////////////

    public void AttachBuildable(Buildable b)
    {
        attachedBuildables.Add(b);
    }

    public virtual void OnPlayerBuildDone(GameObject raycastedObject)
    {
        SetMode(Mode.Default);

        if (raycastedObject == null) return;

        Buildable b = raycastedObject.GetComponent<Buildable>();
        if(b) //Si estaba raycasteando sobre un buildable, entonces se attacheara a el
        {
            b.AttachBuildable(this);
            this.AttachBuildable(b);
        }
    }

    private Vector3 GetBuildPosition()
    {
        Vector3 buildPoint = GC.player.GetComponent<PlayerBuilding>().GetVisionRayPoint();
        Vector3 buildNormal = GC.player.GetComponent<PlayerBuilding>().GetVisionRayNormal();
        if(buildPoint == Vector3.zero) return transform.position;
        return buildPoint + anchorOffsetScale * buildNormal;
    }

    private Quaternion GetBuildRotation()
    {
        Vector3 buildNormal = GC.player.GetComponent<PlayerBuilding>().GetVisionRayNormal();
        if (buildNormal == Vector3.zero) return Quaternion.identity;
        return Quaternion.LookRotation(buildNormal);
    }

    public Mode GetMode()
    {
        return currentMode;
    }

    public virtual void OnPausePhysics()
    {
        if (GetComponent<Rigidbody>()) Destroy(gameObject.GetComponent<Rigidbody>()); 
    }

    public virtual void OnResumePhysics()
    {
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().mass = mass;
        }
    }

    public virtual void SetMode(Mode mode)
    {
        currentMode = mode;
        if (currentMode == Mode.Build) GetComponent<MeshRenderer>().material = buildMaterial;
        else if (currentMode == Mode.Over) GetComponent<MeshRenderer>().material = overMaterial;
        else if (currentMode == Mode.Select) GetComponent<MeshRenderer>().material = selectMaterial;
        else if (currentMode == Mode.Destroy) GetComponent<MeshRenderer>().material = destroyMaterial;
        else GetComponent<MeshRenderer>().material = defaultMaterial;

        if(currentMode == Mode.Build)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
           gameObject.GetComponent<Collider>().enabled = true;
           gameObject.layer = LayerMask.NameToLayer("BuildZone");
        }
    }

    public bool MouseOver()
    {
        int layer = (1 << LayerMask.NameToLayer("BuildZone")) | (1 << LayerMask.NameToLayer("BuildIgnore"));

        Vector3 forward = GC.player.GetComponentInChildren<Camera>().transform.forward;
        Ray ray = new Ray(GC.player.transform.position, forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 9999.9f, layer))
            return (hit.collider.gameObject == gameObject);
        return false;
    }

    public virtual void Destroy()
    {
        foreach (Buildable b in attachedBuildables)
        {
            Accessory acc = b as Accessory;
            if(acc) acc.Destroy();
        }
        Destroy(gameObject);
    }

    public void SetInvisible(bool invisible)
    {
        if (invisible) GetComponent<MeshRenderer>().enabled = false;
        else GetComponent<MeshRenderer>().enabled = true;
    }

    public void OnGameStateChange(GameState previousState, GameState newState)
    {
        if(previousState == GSM.Playing)
        {
            if(currentMode != Mode.Build) SetMode(Mode.Default);
        }

        if(newState == GSM.Playing)
        {
            if (currentMode != Mode.Build) SetMode(Mode.Default);
        }
    }
}
