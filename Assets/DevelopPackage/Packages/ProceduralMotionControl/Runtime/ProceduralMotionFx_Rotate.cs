using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(ProceduralMotionManager))]
public class ProceduralMotionFx_Rotate: ProceduralMotionElement
{
    public AnimationCurve curve;
    public Vector3 startRotate;
    public Vector3 endRotate;
    void Start()
    {
        
    }
    
    [ContextMenu("SetStartScale")]
    void SetStartPosition ()
    {
        startRotate = transform.rotation.eulerAngles;
    }
    
    [ContextMenu("SetEndScale")]
    void SetEndPosition ()
    {
        endRotate = transform.rotation.eulerAngles;
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
        transform.eulerAngles = Vector3.Lerp(startRotate,endRotate,curve.Evaluate(rate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
