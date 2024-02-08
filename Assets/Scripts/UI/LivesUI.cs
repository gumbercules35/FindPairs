using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _livesRemainingText;

    [Header("Event Channels")]
    [SerializeField] private  UIEvent_ChannelSO _uIEvent_ChannelSO;


    private void Start() {
        _uIEvent_ChannelSO.OnUIEventRaised += UIEvent_OnEventRaised;
    }


    private void OnEnable() {
        _uIEvent_ChannelSO.OnUIEventRaised += UIEvent_OnEventRaised;
    }
    private void OnDisable() {
        _uIEvent_ChannelSO.OnUIEventRaised -= UIEvent_OnEventRaised;
    }

    private void UIEvent_OnEventRaised(object sender, UIEvent_ChannelSO.UIEventArgs e)
    {
        _livesRemainingText.text = $"{e.livesRemaining}";
    }

}
