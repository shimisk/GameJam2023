using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance{  get; private set; } 
    public int Score {  get; private set; }
    public int HighScore { get; private set; }

    private void Awake()
    {
        
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadHighscore();
        Score = 5000;
        StartCoroutine(DetractScoreRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Addscore(int scoreToadd)
    {
        Score += scoreToadd;
    }


    IEnumerator DetractScoreRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Score -= 5;
           
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }

    public void SaveHighScore()
    {
        HighScore = Score;
        SaveData data = new SaveData();
        data.highScore = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
        Debug.Log("saved highscore " + HighScore);
    }

    public void LoadHighscore()
    {
        
        string path = Application.persistentDataPath + "/highscore.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            HighScore = data.highScore;
            Debug.Log("Loaded Highscore " + HighScore);

        }
    }
}
