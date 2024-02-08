using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ProcessCard_ChannelSO : ScriptableObject
{
    public event EventHandler<OnProcessCardSelectionEventArgs> OnProcessCardSelection;
    public class OnProcessCardSelectionEventArgs : EventArgs {
        public bool isMatch;
    }

    public void RaiseEvent(object sender, OnProcessCardSelectionEventArgs eventArgs) {
        OnProcessCardSelection?.Invoke(sender, eventArgs);
    }
}
