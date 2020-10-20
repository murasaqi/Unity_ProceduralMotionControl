using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.855f, 0.8623f, 0.87f)]
[TrackClipType(typeof(ProceduralMotionControlClip))]
public class ProceduralMotionControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();
        var playable = ScriptPlayable<ProceduralMotionControlMixerBehaviour>.Create (graph, inputCount);
        var playableBehaviour  = playable.GetBehaviour();
        
        if (playableBehaviour != null)
        {
            playableBehaviour.director = playableDirector;
            playableBehaviour.clips = GetClips();
        }

        return playable;
    }
}
