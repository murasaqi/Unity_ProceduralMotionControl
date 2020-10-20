using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(ProceduralMotionManager))]
public class ProceduralMotionFx_Scale: ProceduralMotionElement
{
    public AnimationCurve curve;
    public Vector3 startScale;
    public Vector3 endScale;
    
    void Start()
    {
        
    }
    
    [ContextMenu("SetStartScale")]
    void SetStartPosition ()
    {
        startScale = transform.localScale;
    }
    
    [ContextMenu("SetEndScale")]
    void SetEndPosition ()
    {
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
        if(transform!=null)transform.localScale = Vector3.Lerp(startScale,endScale,curve.Evaluate(rate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
