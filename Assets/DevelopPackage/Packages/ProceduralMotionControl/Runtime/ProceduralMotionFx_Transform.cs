using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(ProceduralMotionManager))]
public class ProceduralMotionFx_Transform : ProceduralMotionElement
{
    public AnimationCurve curve;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Quaternion startQuaternion;
    public Quaternion endQuaternion;
    public Vector3 startScale;
    public Vector3 endScale;
    void Start()
    {
        
    }
    
    [ContextMenu("SetStartPosition")]
    void SetStartPosition ()
    {
        startPosition = transform.position;
        startQuaternion = transform.localRotation;
        startScale = transform.localScale;
    }
    
    [ContextMenu("SetEndPosition")]
    void SetEndPosition ()
    {
        endPosition = transform.position;
        endQuaternion = transform.localRotation;
        endScale = transform.localScale;
    }

    private void Reset()
    {
        var manager = GetComponent<ProceduralMotionManager>();

        if (manager)
        {
            manager.GetOwnEffects();
            
        }
        
    }

    public override void OnProcessFrame(double time)
    {
        var rate = Mathf.Clamp((float)time, 0, 1);
        transform.position = Vector3.Lerp(startPosition,endPosition,curve.Evaluate(rate));
        transform.rotation = Quaternion.Lerp(startQuaternion, endQuaternion, curve.Evaluate(rate));
        transform.localScale = Vector3.Lerp(startScale, endScale, curve.Evaluate(rate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
