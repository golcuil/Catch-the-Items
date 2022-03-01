using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    private Transform slider;

    public AudioSource audioSource;

    public bool isGameActive;

    private float lives = 3;
    private float score = 0;
    private float spawnRate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        slider = GameObject.Find("Volume").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = GameObject.Find("Volume").GetComponent<Slider>().value;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            

        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    public void UpdateLives(int livesToAdd)
    {
        lives -= livesToAdd;
        livesText.text = "Lives : " + lives;

        if(lives == 0)
        {
            isGameActive = false;
            livesText.enabled = false;
            GameOver();
        }
    }

    public void GameOver()
    {        
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
             
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateLives(0);
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        volumeText.enabled = false;
        slider.position = new Vector3(100000, 1000000, 1000000);
    }
}
