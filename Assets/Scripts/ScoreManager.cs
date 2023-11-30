using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance{  get; private set; } 
    public int Score {  get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log(Instance.Score);
        }
    }

}
