using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    Vector2 checkpointPos;
    void Start()
    {
        checkpointPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
        if(collision.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
    void Die()
    {
        StartCoroutine(Respawn(0.5f)); // Start the respawn coroutine with a delay of 2 seconds
    }
    IEnumerator Respawn(float duration)
    {
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
