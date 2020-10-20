using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(ProceduralMotionManager))]
public class ProceduralMotionFx_TranslatePrimitive : ProceduralMotionElement
{
    public AnimationCurve curve_x = new AnimationCurve();
    public AnimationCurve curve_y = new AnimationCurve();
    public AnimationCurve curve_z = new AnimationCurve();
    public bool offset;
    public Vector3 offsetPosition = new Vector3();
    void Start()
    {
        
    }

    [ContextMenu("SetOffsetPosition")]
    void SetOffsetPosition()
    {
        offsetPosition = transform.localPosition;
    }
    
    [ContextMenu("SetStartPosition")]
    void SetStartPosition ()
    {
        var pos = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            transform.localPosition.z
            );

        if (offset) pos -= offsetPosition;
        curve_x.MoveKey(0,new Keyframe(0,pos.x));
        curve_y.MoveKey(0,new Keyframe(0,pos.y));
        curve_z.MoveKey(0,new Keyframe(0,pos.z));
        // startPosition = transform.localPosition;

        foreach (var VARIABLE in curve_z.keys)
        {
            Debug.Log(VARIABLE.value);
        }
    }
    
    [ContextMenu("SetEndPosition")]
    void SetEndPosition ()
    {
        var pos = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            transform.localPosition.z
        );

        if (offset) pos -= offsetPosition;
        curve_x.MoveKey(curve_x.keys.Length-1,new Keyframe(1,pos.x));
        curve_y.MoveKey(curve_y.keys.Length-1,new Keyframe(1,pos.y));
        curve_z.MoveKey(curve_z.keys.Length-1,new Keyframe(1,pos.z));
        
        Debug.Log(curve_z.keys[curve_z.length-1].value);
        
    }

    private void Reset()
    {
        var manager = GetComponent<ProceduralMotionManager>();

        curve_x = new AnimationCurve();
        curve_y = new AnimationCurve();
        curve_z = new AnimationCurve();
        var count = 0;    
        while (curve_x.keys.Length <2)
        {
            curve_x.AddKey(count, count);
            count++;
        }

        count = 0;
        while (curve_y.keys.Length <2)
        {
            curve_y.AddKey(count, count);
            count++;
        }
            
        
        count = 0;
        while (curve_z.keys.Length <2)
        {
            curve_z.AddKey(count, count);
            count++;
        }
        if (manager)
        {
            manager.GetOwnEffects();
            
        }
        
    }

    public override void OnProcessFrame(double time)
    {
        var rate = Mathf.Clamp((float)time, 0, 1);
        transform.localPosition = new Vector3(
            curve_x.Evaluate(rate),
            curve_y.Evaluate(rate),
            curve_z.Evaluate(rate)
            );
        if (offset) transform.localPosition += offsetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
