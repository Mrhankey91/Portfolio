using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private LevelController levelController;
    private CoinsController coinsController;
    private PlayerController playerController;
    private PlayerControls playerControls;
    private CameraFollow cameraFollow;

    private Timer timer;
    private GameOverPanel gameOverPanel;
    private Animator startCountDown;

    private WaitForSeconds playerControlsWait = new WaitForSeconds(1f);
    private bool gameOver = false;

    private GameObject touchInstructions;

    // Start is called before the first frame update
    void Awake()
    {
        levelController = GetComponent<LevelController>();
        coinsController = GetComponent<CoinsController>();
        playerController = GetComponent<PlayerController>();

        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        timer = GetComponent<Timer>();
        startCountDown = GameObject.Find("StartCountDown").GetComponent<Animator>();

        gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<GameOverPanel>();
        gameOverPanel.HideGameOverPanel();

        touchInstructions = GameObject.Find("TouchInstructions");
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        touchInstructions?.SetActive(false);
#endif
    }

    private void Start()
    {
        levelController.LoadLevel(DataController.instance.levelID);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        SpawnPlayer();
        //StartCoroutine(EnablePlayerControls());
        playerControls.enabled = true;
        startCountDown.SetTrigger("CountDown");

        yield return new WaitForSeconds(3f);

        playerControls.UnfreezeRigidBodies();
        timer.StartTimer();
    }

    private void SpawnPlayer()
    {
        GameObject player = playerController.Respawn();
        playerControls = player.GetComponent<PlayerControls>();
        cameraFollow.followObject = player.transform.Find("Body/Rig/root");
    }

    public virtual void RestartLevel()
    {
        gameOver = false;
        ShowTouchInstructions(true);
        timer.Reset();
        levelController.RestartLevel();
        gameOverPanel.HideGameOverPanel();
        GoogleAds.instance?.HideBanner();
        StartCoroutine(StartGame());
    }

    public void NextLevel()
    {
        gameOver = false;
        ShowTouchInstructions(true);
        timer.Reset();
        levelController.NextLevel();
        gameOverPanel.HideGameOverPanel();
        GoogleAds.instance?.HideBanner();
        StartCoroutine(StartGame());
    }

    private IEnumerator EnablePlayerControls()
    {
        yield return playerControlsWait;
        playerControls.enabled = true;
    }

    public void Finish()
    {
        if (gameOver)
            return;

        gameOver = true;
        timer.StopTimer();
        playerControls.enabled = false;
        cameraFollow.followObject = null;

        //if (!extraBallPanelShowed && GoogleAds.instance != null && GoogleAds.instance.isConnected && GoogleAds.instance.CanPlayRewardAd("ExtraBall"))
        {
         //   extraBallPanel.Show();
        }

        GoogleAds.instance?.ShowBanner();
        GoogleAds.instance?.ShowInterstitial("GameOver");
        int starsCompleted = GetStarsCompleted();

        gameOverPanel.ShowGameOverPanel(timer.GetTime(), starsCompleted, GetPersonalBestTime(), levelController.HasNextLevel(), levelController.levelData.targetTimes, DidFinishWithTube());
        //FirebaseLogging.instance.LogScore(levelController.levelData.level, score.GetScore());
        //FirebaseLogging.instance.LogStars(levelController.levelData.level, starsCompleted);
        //FirebaseLogging.instance.LogLevelFinished(levelController.levelData.level, tryNumber, score.GetScore(), starsCompleted);
        //FirebaseLogging.instance.EndLevel(levelController.levelData.level);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private int GetStarsCompleted()
    {
        //for (int i= 0; i < levelController.levelData.targetScores; ++i)
        int stars = 0;
        foreach (float tt in levelController.levelData.targetTimes)
        {
            if (timer.GetTime() <= tt)
                stars++;
            else
                break;
        }
        return stars;
    }

    private float GetPersonalBestTime()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LevelEditor")
            return 0;

        float bestTime = DataController.instance.times.bestTimes[levelController.levelData.level - 1];
        float currentTime = timer.GetTime();
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            DataController.instance.times.bestTimes[levelController.levelData.level - 1] = bestTime;
            DataController.instance.SaveTimes();
        }

        return bestTime;
    }

    private bool DidFinishWithTube()
    {
        return playerControls.transform.Find("Tube")?.GetComponent<CharacterJoint>();
    }

    public void ShowTouchInstructions(bool show)
    {
        if(touchInstructions.activeSelf != show)
        {
#if UNITY_ANDROID || UNITY_IOS
            touchInstructions?.SetActive(show);
#else
            touchInstructions?.SetActive(false);
#endif
        }
    }
}
