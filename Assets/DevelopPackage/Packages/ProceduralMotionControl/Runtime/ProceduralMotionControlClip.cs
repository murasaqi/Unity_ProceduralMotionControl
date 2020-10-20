using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ProceduralMotionControlClip : PlayableAsset, ITimelineClipAsset
{
    public ProceduralMotionControlBehaviour template = new ProceduralMotionControlBehaviour ();
    public ExposedReference<ProceduralMotionManager> proceduralMotionManager;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    const double DefaultDuration = 0.1f;
    
    // IPlayableAsset.durationを実装するとその値をデフォルト値としてTimelineClipを生成してくれる
    public override double duration
    {
        get { return DefaultDuration; }
    }

    
    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ProceduralMotionControlBehaviour>.Create (graph, template);
        // playable.SetDuration(0.1);
        ProceduralMotionControlBehaviour clone = playable.GetBehaviour ();
        clone.proceduralMotionManager = proceduralMotionManager.Resolve (graph.GetResolver ());
        return playable;
    }
}
