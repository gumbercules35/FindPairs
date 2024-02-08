using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI Event Channel SO")]
public class UIEvent_ChannelSO : ScriptableObject
{
    public event EventHandler<UIEventArgs> OnUIEventRaised;

    public class UIEventArgs : EventArgs {
        public int livesRemaining;
    }

    public void RaiseEvent(object sender, UIEventArgs uIEventArgs){
        OnUIEventRaised?.Invoke(sender, uIEventArgs);
    }
}
