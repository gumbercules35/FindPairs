using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Void Event Channel SO")]
public class VoidEvent_ChannelSO : ScriptableObject
{
    public event EventHandler OnEventRaised;

    public void RaiseEvent(object sender, EventArgs e){
        OnEventRaised?.Invoke(sender, e);
    }
}
