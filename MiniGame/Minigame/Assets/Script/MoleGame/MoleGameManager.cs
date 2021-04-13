using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoleGameManager : MonoBehaviour
{
    public enum GameState
    {
        Init,
        Ready,
        Play,
        End
    }

    [SerializeField] private MoleObject[] moleObjects;
    [SerializeField] private GameObject startButton;
    [SerializeField] private Text catchCountText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text readyTimeText;

    private GameState gameState;
    private Coroutine gameCoroutine;
    private int catchCount;
    private float timeLimit;
    private float readyTime;

    public static MoleGameManager instance = null;
    private void Awake()
    {
        readyTime = 3f;
        timeLimit = 10f;
        gameState = GameState.Ready;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static MoleGameManager Instance
    {
        get 
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
     
    private void Start()
    {
        catchCount = 0;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Init:
                break;
            case GameState.Ready:
                readyTime -= Time.deltaTime;
                readyTimeText.text = ((int)readyTime).ToString();
                if (readyTime <= 0f)
                {                    
                    StartGame();
                }
                break;
            case GameState.Play:
                {
                    timeLimit -= Time.deltaTime;
                    timeText.text = $"{timeLimit:N2}";
                    if (timeLimit <= 0f)
                    {
                       EndGame();
                    }
                }
                break;
            case GameState.End:
                break;
        }
    }

    private void EndGame()
    {
        gameState = GameState.End;
        if(gameCoroutine != null)
            StopCoroutine(gameCoroutine);
    }
    IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            int rand = Random.Range(0, moleObjects.Length);
            moleObjects[rand].AppearMole();   
            float randNum = Random.Range(0, 2f);
            yield return new WaitForSeconds(randNum);
        }
    }

    private void StartGame()
    {
        readyTimeText.text = string.Empty;
        startButton.SetActive(false);
        gameState = GameState.Play;

        if (gameCoroutine != null)
            StopCoroutine(gameCoroutine);
        gameCoroutine = StartCoroutine(RespawnCoroutine());
    }
    public void OnClickBtnStart()
    {
        StartGame();    
    }

    public void CatchMole()
    {
        catchCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        catchCountText.text = catchCount.ToString();
    }
}
