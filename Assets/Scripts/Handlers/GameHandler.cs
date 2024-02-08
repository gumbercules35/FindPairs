using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    private enum GameState {
        Playing,
        GameOver,
    }
    private GameState _gameState;

    [SerializeField] private MouseRayCastHandler _mouseRayCastHandler;
    [SerializeField] private CardHandler _cardHandler;
    [SerializeField] private WaitTimer _waitTimer;

    [Header("Event Channels")]
    [SerializeField] private ProcessCard_ChannelSO _processCard_ChannelSO;
    [SerializeField] private VoidEvent_ChannelSO _livesVoidEvent_ChannelSO;
    [SerializeField] private VoidEvent_ChannelSO _inputVoidEvent_ChannelSO;
    [SerializeField] private BidirectionalVoidEvent_ChannelSO _timerBidirectionalVoid_ChannelSO;

    //Unity Functions
    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        _cardHandler.SpawnCards();
        _gameState = GameState.Playing;
    }

    private void Update() {
        switch (_gameState)
        {
            case GameState.Playing:
                if (_cardHandler.IsSelectionComplete() && !_waitTimer.IsTimerRunning()){
                    _timerBidirectionalVoid_ChannelSO.StartTrigger(this);
                    _processCard_ChannelSO.RaiseEvent(this, new ProcessCard_ChannelSO.OnProcessCardSelectionEventArgs{ isMatch = _cardHandler.CheckSelectedCardsForMatch()});
                }
                break;
            case GameState.GameOver:
                break;
        }
        
    }

    private void OnEnable() {
        _inputVoidEvent_ChannelSO.OnEventRaised += InputHandler_OnSelectPerformed;
        _livesVoidEvent_ChannelSO.OnEventRaised += Lives_OnNoLivesRemaining;
        _timerBidirectionalVoid_ChannelSO.OnTaskComplete += TimerBidirectionalChannel_OnTaskComplete;
    }


    private void OnDisable() {
        _inputVoidEvent_ChannelSO.OnEventRaised -= InputHandler_OnSelectPerformed;
        _livesVoidEvent_ChannelSO.OnEventRaised -= Lives_OnNoLivesRemaining;
        _timerBidirectionalVoid_ChannelSO.OnTaskComplete -= TimerBidirectionalChannel_OnTaskComplete;
    }

    //Event Callbacks
    private void InputHandler_OnSelectPerformed(object sender, EventArgs e)
    {
        if (_gameState != GameState.Playing) return;

        if (_mouseRayCastHandler.TrySelectCard(out Card _card)){
            _cardHandler.AddCardToSelection(_card);
        }
    }

    private void TimerBidirectionalChannel_OnTaskComplete(object sender, EventArgs e)
    {
        if (_gameState != GameState.Playing) return;
        
        if (_cardHandler.CheckSelectedCardsForMatch()){
            
            _cardHandler.ProcessMatchingCards();
        } else {
           
            _cardHandler.ProcessFailedCards();
        }
        
        _cardHandler.ResetSelection();
    }

    private void Lives_OnNoLivesRemaining(object sender, EventArgs e)
    {
        print("gameOver");
        _gameState = GameState.GameOver;
    }

}
