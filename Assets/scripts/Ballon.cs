using UnityEngine;

public class Ballon : MonoBehaviour
{
    GameManager manager;
    [SerializeField] GameObject particle;
    [SerializeField] float timer;
    public int scoreCount;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    private void Update()
    {
       // manager.scoreText.text = scoreCount.ToString();
    }
    /*private void OnDestroy()
    {
        if (particle != null)
        {
            GameObject ballonPrefab = Instantiate(particle, transform.position, Quaternion.identity);
            //Destroy(ballonPrefab, timer);
        }
        
    }*/

    public void DestroyObject()
    {
        GameObject ballonPrefab = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(ballonPrefab, timer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("deathZone"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("pin"))
        {
            Audiomanager.Instance.PlaySfx("Ballon Pop");
            manager.IncreamentScore();
            DestroyObject();
        }
    }
}
