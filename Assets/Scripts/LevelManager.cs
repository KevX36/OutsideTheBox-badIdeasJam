using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentLevel;
    private GameObject Player;
    [SerializeField] private SurviceHub surviceHub;
    private void Awake()
    {
        Player = surviceHub.player;
    }
    public void ChangeLevels(GameObject target, Vector2 spawnPoint)
    {
        CurrentLevel.SetActive(false);
        target.SetActive(true);

        Player.transform.position = spawnPoint;
        CurrentLevel = target;
    }
}
