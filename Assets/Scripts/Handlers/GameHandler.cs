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

    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private MouseRayCastHandler _mouseRayCastHandler;
    [SerializeField] private CardHandler _cardHandler;
    [SerializeField] private WaitTimer _waitTimer;
    [SerializeField] private Lives _lives;

    [SerializeField] private ProcessCard_ChannelSO processCard_ChannelSO;

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
                    _waitTimer.StartTimer();
                    processCard_ChannelSO.RaiseEvent(this, new ProcessCard_ChannelSO.OnProcessCardSelectionEventArgs{ isMatch = _cardHandler.CheckSelectedCardsForMatch()});
                }
                break;
            case GameState.GameOver:
                break;
        }
        
    }

    private void OnEnable() {
        _inputHandler.OnSelectPerformed += InputHandler_OnSelectPerformed;
        _waitTimer.OnTimerElapsed += WaitTimer_OnTimerElapsed;
        _lives.OnNoLivesRemaining += Lives_OnNoLivesRemaining;
    }

    private void OnDisable() {
        _inputHandler.OnSelectPerformed -= InputHandler_OnSelectPerformed;
        _waitTimer.OnTimerElapsed -= WaitTimer_OnTimerElapsed;
        _lives.OnNoLivesRemaining -= Lives_OnNoLivesRemaining;
    }

    //Event Callbacks
    private void InputHandler_OnSelectPerformed(object sender, EventArgs e)
    {
        if (_gameState != GameState.Playing) return;

        if (_mouseRayCastHandler.TrySelectCard(out Card _card)){
            _cardHandler.AddCardToSelection(_card);
        }
    }

    private void WaitTimer_OnTimerElapsed(object sender, EventArgs e)
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
