using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralMotionElement : MonoBehaviour
{
    public virtual void OnProcessFrame(double time)
    {
        
    }

}

[ExecuteAlways]
public class ProceduralMotionManager : ProceduralMotionElement
{
    [Range(0, 1)] public double progressRate;
    // private double m_preProcessrate = -1;
    public bool debugMode = false;
    public List<ProceduralMotionElement> motionElements = new List<ProceduralMotionElement>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    [ContextMenu("SetEffects")]
    void SetEffects ()
    {
        GetOwnEffects();
    }


    public void GetOwnEffects()
    {
        var effects = GetComponents<ProceduralMotionElement>();
        motionElements.Clear();
        foreach (var e in effects)
        {
            if (e.GetType() != typeof(ProceduralMotionManager) && e.GetType().IsSubclassOf(typeof(ProceduralMotionElement)))
            {
                motionElements.Add(e);
            }
        }
    }

    public override void OnProcessFrame(double time)
    {
        // if(m_preProcessrate == time) return;
        
        base.OnProcessFrame(time);
        foreach (var m in motionElements)
        {
            if(m!=null)m.OnProcessFrame(time);
        }

        progressRate = time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(debugMode) OnProcessFrame(progressRate);
        
    }
}
