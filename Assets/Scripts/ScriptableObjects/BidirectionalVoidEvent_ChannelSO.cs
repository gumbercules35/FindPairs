using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bidirectional(void) Channel SO")]
public class BidirectionalVoidEvent_ChannelSO : ScriptableObject
{
    public event EventHandler OnStartTrigger;
    public event EventHandler OnTaskComplete;

    public void StartTrigger(object sender){
        OnStartTrigger?.Invoke(sender, EventArgs.Empty);
    }

    public void NotifyComplete(object sender){
        OnTaskComplete?.Invoke(sender, EventArgs.Empty);
    }
}
