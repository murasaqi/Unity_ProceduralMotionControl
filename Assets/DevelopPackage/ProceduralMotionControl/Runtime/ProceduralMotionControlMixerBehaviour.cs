
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
            if(inputPort != 0 &&  m_Director.time < clip.start) break;
           
            ScriptPlayable<ProceduralMotionControlBehaviour> scriptPlayable =
                (ScriptPlayable<ProceduralMotionControlBehaviour>) playable.GetInput(inputPort);
            var playableBehaviour = scriptPlayable.GetBehaviour();
            var motionManager = playableBehaviour.proceduralMotionManager;



            var min = playableBehaviour.min;
            var max = playableBehaviour.max;
            // var startProgress = playableBehaviour.inverse ? 1f : 0f;
            // var endProgress = playableBehaviour.inverse ? 0f : 1f;

            var startProgress = playableBehaviour.inverse ? max: min;
            // Debug.Log(playableBehaviour.min);
            var endProgress = playableBehaviour.inverse ? min: max;
            
            if (inputPort == (m_Clips.Count() - 1))
            {
                if (clip.start + clip.duration < m_Director.time && motionManager.progressRate != endProgress)
                {
                    motionManager.OnProcessFrame(endProgress);
                    break;
                }
            }
           
            //
            if (clip.start <= m_Director.time && m_Director.time <= clip.start + clip.duration )
            {
                    
                var initializedTime = (m_Director.time - clip.start) / clip.duration;
                if (playableBehaviour.inverse) initializedTime = 1d - initializedTime;
                motionManager.OnProcessFrame(Mathf.Clamp((float)initializedTime,min,max));
            }
            else if(m_Director.time < clip.start && motionManager.progressRate != startProgress)
            {
                motionManager.OnProcessFrame(startProgress);
            } 
            else if (m_Director.time > clip.start + clip.duration && motionManager.progressRate != endProgress)
            {
                motionManager.OnProcessFrame(endProgress);
            }
            
            inputPort++;
        }

    }
}
