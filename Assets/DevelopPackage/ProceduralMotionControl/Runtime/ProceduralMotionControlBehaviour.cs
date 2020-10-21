using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class ProceduralMotionControlBehaviour : PlayableBehaviour
{
    public ProceduralMotionManager proceduralMotionManager;
    public bool inverse = false;
    [Range(0f, 1f)] public float min = 0f;
    [Range(0f, 1f)] public float max = 1f;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
