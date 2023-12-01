using UnityEngine;
using System.Collections;
using TMPro;

public class BatCollision : MonoBehaviour
{
    [SerializeField] float timerToVulnerable = 0.5f;
    public GameObject winMenu;
    public TMP_Text scoreTxt;
    public TMP_Text highScoreTxt;
    BatMovement bat;

    private bool _beenHit;


    private void Start()
    {
        bat = GetComponent<BatMovement>();
        if (bat == null)
        {
            Debug.LogError("BatMovement not found on " + gameObject.name);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (_beenHit) { return; }
            
            ChangeBatScale();
            bat.ChangeSpeed();
            if (transform.localScale.x < 0.1f)
            {
                ScoreManager.Instance.Addscore(10000);
                Time.timeScale = 0;
                if (ScoreManager.Instance.Score > ScoreManager.Instance.HighScore)
                {

                    ScoreManager.Instance.SaveHighScore();
                    Debug.Log(Application.persistentDataPath);
                }
                winMenu.SetActive(true);
                
                scoreTxt.gameObject.SetActive(true);
                scoreTxt.text = $"Score: {ScoreManager.Instance.Score}";
                highScoreTxt.gameObject.SetActive(true);
                highScoreTxt.text = $"HighScore: {ScoreManager.Instance.HighScore}";
              

            }
            _beenHit = true;
            StartCoroutine(ResetHit());
            Destroy(collision.gameObject);
        }
    }

    private void ChangeBatScale()
    {
        Vector3 newScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f);
        transform.localScale = newScale;
        if (bat != null)
        {
            bat.ChangeSpeed();
        }
    }

    IEnumerator ResetHit()
    {  
        yield return new WaitForSeconds(timerToVulnerable);      
        _beenHit = false;
    }
}

