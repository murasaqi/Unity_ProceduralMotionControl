
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ProceduralMotionControlMixerBehaviour : PlayableBehaviour
{
    
    private IEnumerable<TimelineClip> m_Clips;
    private PlayableDirector m_Director;
    
    internal PlayableDirector director
    {
        get { return m_Director; }
        set { m_Director = value; }
    }

    internal IEnumerable<TimelineClip> clips
    {
        get { return m_Clips; }
        set { m_Clips = value; }
    }
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        int inputCount = playable.GetInputCount ();

        
        int inputPort = 0;
        foreach (TimelineClip clip in m_Clips)
        {
            ScriptPlayable<ProceduralMotionControlBehaviour> scriptPlayable =
                (ScriptPlayable<ProceduralMotionControlBehaviour>) playable.GetInput(inputPort);
            var playableBehaviour = scriptPlayable.GetBehaviour();
            var motionManager = playableBehaviour.proceduralMotionManager;
            if (clip.start <= m_Director.time && m_Director.time <= clip.start + clip.duration )
            {
                    
                var initializedTime = (m_Director.time - clip.start) / clip.duration;
                motionManager.OnProcessFrame((float)initializedTime);
            }
            else if(m_Director.time < clip.start && motionManager.progressRate > 0d)
            {
                motionManager.OnProcessFrame(0);
            } 
            else if (m_Director.time > clip.start + clip.duration && motionManager.progressRate < 1d)
            {
                motionManager.OnProcessFrame(1);
            }
            
            inputPort++;
        }

    }
}
