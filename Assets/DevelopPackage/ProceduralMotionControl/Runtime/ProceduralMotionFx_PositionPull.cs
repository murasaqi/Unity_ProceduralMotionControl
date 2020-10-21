using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(ProceduralMotionManager))]
public class ProceduralMotionFx_PositionPull : ProceduralMotionElement
{

    public ProceduralMotionFx_Transform parent;
    // public AnimationCurve curve;
    public Vector3 offsetPosition;
    // public Quaternion offsetQuaternion;
    // public Vector3 offsetScale;
    // void Start()
    // {
    //     
    // }
    //
    // [ContextMenu("SetStartPosition")]
    // void SetStartPosition ()
    // {
    //     startPosition = transform.position;
    //     startQuaternion = transform.localRotation;
    //     startScale = transform.localScale;
    // }
    //
    // [ContextMenu("SetEndPosition")]
    // void SetEndPosition ()
    // {
    //     endPosition = transform.position;
    //     endQuaternion = transform.localRotation;
    //     endScale = transform.localScale;
    // }

    private void Reset()
    {
        // var manager = GetComponent<ProceduralMotionManager>();
        //
        // if (manager)
        // {
        //     manager.GetOwnEffects();
        //     
        // }
        
    }

    public override void OnProcessFrame(double time)
    {
        var rate = Mathf.Clamp((float)time, 0, 1);
        transform.position = offsetPosition+ Vector3.Lerp(parent.startPosition,parent.endPosition,parent.curve.Evaluate(rate));
        // transform.rotation = Quaternion.Lerp(parent.startQuaternion, parent.endQuaternion, parent.curve.Evaluate(rate));
        // transform.localScale = Vector3.Lerp(parent.startScale, parent.endScale, parent.curve.Evaluate(rate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
