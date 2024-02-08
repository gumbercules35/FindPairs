using System;
using UnityEngine;

public class WaitTimer : MonoBehaviour
{
    [SerializeField] private BidirectionalVoidEvent_ChannelSO _timerBidirectionalVoid_ChannelSO;
    [SerializeField]private float _cardFlipWaitTimer;
    [SerializeField]private float _cardFlipWaitTimerMax;
    private bool _isTimerRunning;

    private void OnEnable() {
        _timerBidirectionalVoid_ChannelSO.OnStartTrigger += TimerBidirectionalChannel_OnStartTrigger; 
    }
    private void OnDisable() {
        _timerBidirectionalVoid_ChannelSO.OnStartTrigger -= TimerBidirectionalChannel_OnStartTrigger;
    }

    private void TimerBidirectionalChannel_OnStartTrigger(object sender, EventArgs e)
    {
        StartTimer();
    }

    private void Update() {
        if(!_isTimerRunning) return;

        _cardFlipWaitTimer -= Time.deltaTime;
        if (_cardFlipWaitTimer < float.Epsilon) {
            _cardFlipWaitTimer = _cardFlipWaitTimerMax;
            _timerBidirectionalVoid_ChannelSO.NotifyComplete(this);
            _isTimerRunning = false;
        }

    }

    public bool IsTimerRunning(){
        return _isTimerRunning;
    }

    public void StartTimer(){
        _isTimerRunning = true;
    }
    


}
