using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Accessory : Buildable
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public virtual void FixedUpdate()
    {
        if (!GC.physicsPaused)
        {
            foreach (Buildable b in attachedBuildables)
            {
                Accessory acc = b as Accessory;
                if(!acc) //Si no es un accesorio, aplica el efecto
                { 
                    ApplyAccessoryEffect(b);
                }
            }
        }
    }

    public override void OnPlayerBuildDone(GameObject raycastedObject)
    {
        base.OnPlayerBuildDone(raycastedObject);

        if (raycastedObject != null) 
            transform.parent = raycastedObject.transform;
    }

    public override void OnPausePhysics()
    {
        base.OnPausePhysics();
    }

    public override void OnResumePhysics()
    {
    }

    public override void SetMode(Mode mode)
    {
        base.SetMode(mode);
        gameObject.layer = LayerMask.NameToLayer("BuildIgnore");
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }

    public abstract void ApplyAccessoryEffect(Buildable b);
}
