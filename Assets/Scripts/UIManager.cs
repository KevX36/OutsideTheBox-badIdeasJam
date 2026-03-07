using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SurviceHub surviceHub;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] public GameObject player;
    [SerializeField] private GameObject pauseUI;
    


    private void Start()
    {
        player = surviceHub.player;
    }
    public void ShowMainMenu()
    {
        CloseAllUI();
        mainMenu.SetActive(true);
    }
    public void ShowGameplayUI()
    {
        CloseAllUI(true);
        Time.timeScale = 1f;
        player.SetActive(true);
        gameScreen.SetActive(true);
        gameplayUI.SetActive(true);
    }
    public void ShowPauseUI()
    {
        CloseAllUI(true);
        Time.timeScale = 0.0f;
        pauseUI.SetActive(true);
    }
    





    public void CloseAllUI()
    {
        mainMenu.SetActive(false);
        gameplayUI.SetActive(false);
        gameScreen.SetActive(false);
        pauseUI.SetActive(false);
        
        player.SetActive(false);
    }
    public void CloseAllUI(bool doNotCloseGamePlay)
    {
        mainMenu.SetActive(false);
        gameplayUI.SetActive(false);
        
        pauseUI.SetActive(false);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        gameScreen = surviceHub.levelManager.CurrentLevel;
    }
}
