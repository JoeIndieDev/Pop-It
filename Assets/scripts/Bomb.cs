using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bomb : MonoBehaviour
{
    Animator camAnimator;
    GameManager manager;
    //public bool gameOver;
    [SerializeField] GameObject explosion;

    private void Start()
    {
        camAnimator = Camera.main.GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //gameOver = false;
    }
    public void DestroyObject()
    {
        GameObject explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        //Destroy(ballonPrefab, timer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("deathZone"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pin"))
        {
            
            StartCoroutine(GameOver());
            
        }
    }
    IEnumerator GameOver()
    {
        DestroyObject();
        Audiomanager.Instance.PlaySfx("Explosion");
        camAnimator.SetTrigger("shake");
        manager.gameOverPanel.SetActive(true);
        //gameOver = true;
        manager.isGameEnded = true;
        yield return new WaitForSeconds(.1f);
        // yield return new WaitForSeconds(1.3f);
        Time.timeScale = 0;
    }
}
