using System;
using UnityEngine;

public class WaitTimer : MonoBehaviour
{
    [SerializeField]private float cardFlipWaitTimer;
    [SerializeField]private float cardFlipWaitTimerMax;
    private bool _isTimerRunning;
    public event EventHandler OnTimerElapsed;

    private void Update() {
        if(!_isTimerRunning) return;

        cardFlipWaitTimer -= Time.deltaTime;
        if (cardFlipWaitTimer < float.Epsilon) {
            cardFlipWaitTimer = cardFlipWaitTimerMax;
            OnTimerElapsed?.Invoke(this, EventArgs.Empty);
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
