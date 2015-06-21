using UnityEngine;
using System.Collections;

public class GC : MonoBehaviour 
{
    public static GameObject player;
    public GameObject _player;

    public static Inventory inventory;
    public Inventory _inventory;

    void Awake()
    {
        player = _player;
        inventory = _inventory;
    }

	void Start () 
    {
	    
	}
	
	void Update () 
    {
	    if(Input.GetKeyUp(KeyCode.I))
        {
            inventory.TriggerOpenClose();
        }
	}
}
