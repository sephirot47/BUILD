using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BEngineCM : ContextualMenu
{
    private BEngine engine;
    public InputField forceIF;
    public Toggle togRotX, togRotY, togRotZ;
    public Toggle togRotXNeg, togRotYNeg, togRotZNeg;
    public Toggle togRelativeAxis;

	void Start () 
    {
        base.Start();

        engine = parentBuildable as BEngine;

        forceIF.text = engine.force.ToString();

        if (engine.rotationAxis.x > 0.0f) togRotX.isOn = true;
        if (engine.rotationAxis.y > 0.0f) togRotY.isOn = true;
        if (engine.rotationAxis.z > 0.0f) togRotZ.isOn = true;

        if (engine.rotationAxis.x < 0.0f) togRotXNeg.isOn = true;
        if (engine.rotationAxis.y < 0.0f) togRotYNeg.isOn = true;
        if (engine.rotationAxis.z < 0.0f) togRotZNeg.isOn = true;

        if (engine.relativeRotationAxis) togRelativeAxis.isOn = true; else togRelativeAxis.isOn = false;
	}
	
	void Update ()
    {
        base.Update();

        if (forceIF.text.Length <= 0) engine.force = 0.0f;
        else engine.force = float.Parse(forceIF.text);

        Vector3 axis = Vector3.zero;
        if (togRotX.isOn) axis.x = 1.0f; else if (togRotXNeg.isOn) axis.x = -1.0f;
        if (togRotY.isOn) axis.y = 1.0f; else if (togRotYNeg.isOn) axis.y = -1.0f;
        if (togRotZ.isOn) axis.z = 1.0f; else if (togRotZNeg.isOn) axis.z = -1.0f;

        engine.rotationAxis = axis;
        engine.relativeRotationAxis = togRelativeAxis.isOn;
	}
}
