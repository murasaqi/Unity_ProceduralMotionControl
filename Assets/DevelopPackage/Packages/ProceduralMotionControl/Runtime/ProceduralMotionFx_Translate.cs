using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(ProceduralMotionManager))]
public class ProceduralMotionFx_Translate : ProceduralMotionElement
{
    public AnimationCurve curve;
    public Vector3 startPosition;
    public Vector3 endPosition;
    void Start()
    {
        
    }
    
    [ContextMenu("SetStartPosition")]
    void SetStartPosition ()
    {
        startPosition = transform.localPosition;
    }
    
    [ContextMenu("SetEndPosition")]
    void SetEndPosition ()
    {
        endPosition = transform.localPosition;
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
        transform.localPosition = Vector3.Lerp(startPosition,endPosition,curve.Evaluate(rate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
