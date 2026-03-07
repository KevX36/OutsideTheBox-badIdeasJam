using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject extraCam;
    public void Quit()
    {
        Application.Quit();
    }
    public enum GameState
    {

        MainMenu,
        Paused,
        GamePlay,
        




    }
    [SerializeField] private SurviceHub hub;
    private UIManager UI;
    public GameState currentState { get; private set; }
    public GameState lastState { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UI = hub.uIManager;
    }

    public void SetState(GameState state)
    {
        if (currentState == state) return;
        lastState = currentState;
        currentState = state;
        OnStateChange(currentState);


    }

    private void OnStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:

                UI.ShowMainMenu();



                break;


            case GameState.GamePlay:

                UI.ShowGameplayUI();



                break;



            case GameState.Paused:


                UI.ShowPauseUI();


                break;




            



        }
    }

    // Update is called once per frame

    public void OnPause()
    {
        if (currentState == GameState.GamePlay)
        {
            SetState(GameState.Paused);
        }
        else if (currentState == GameState.Paused)
        {
            SetState(GameState.GamePlay);
        }
        
    }
    
    public void CloseSettings()
    {
        SetState(lastState);
    }
    public void ReturnToMainMenu()
    {
        SetState(GameState.MainMenu);
    }
    public void StartGame()
    {
        SetState(GameState.GamePlay);
    }
}
