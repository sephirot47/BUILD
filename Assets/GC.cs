using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GC : MonoBehaviour 
{
    public static GameObject player;
    public GameObject _player;

    public static Inventory inventory;
    public Inventory _inventory;

    public static bool physicsPaused = true;
    public static ContextualMenu cMenu = null;

    void Awake()
    {
        player = _player;
        inventory = _inventory;
    }

	void Start ()
    {
        PausePhysics();
	}
	
	void Update () 
    {
	    if(Input.GetKeyUp(KeyCode.I) && !cMenu)
        {
            inventory.TriggerOpenClose();
        }

        if (Input.GetKeyUp(KeyCode.P) )
        {
            if (physicsPaused) ResumePhysics(); 
            else PausePhysics();
        }
	}

    public static void PausePhysics()
    {
        physicsPaused = true;
        List<Buildable> bs = new List<Buildable>( GameObject.FindObjectsOfType<Buildable>() );
        foreach(Buildable b in bs)
        {
            b.OnPausePhysics();
        }

        player.GetComponent<PlayerBuilding>().OnPausePhysics();

        CanvasUtils.Hide(GameObject.Find("ResumeImage"));
        CanvasUtils.Show(GameObject.Find("PauseImage"));
    }

    public static void ResumePhysics()
    {
        physicsPaused = false;
        List<Buildable> bs = new List<Buildable>(GameObject.FindObjectsOfType<Buildable>());
        foreach (Buildable b in bs)
        {
            b.OnResumePhysics();
        }

        player.GetComponent<PlayerBuilding>().OnResumePhysics();

        CanvasUtils.Show(GameObject.Find("ResumeImage"));
        CanvasUtils.Hide(GameObject.Find("PauseImage") );
    }

    public static GameObject GetSubGameObject(GameObject parent, string goName)
    {
        foreach (Transform t in parent.transform)
        {
            if (t.gameObject.name == goName) return t.gameObject;
            else
            {
                GameObject go = GetSubGameObject(t.gameObject, goName);
                if (go != null) return go;
            }
        }
        return null;
    }

}
