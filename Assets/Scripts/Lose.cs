using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{

    [SerializeField] AudioClip audioClip;
    [SerializeField] Vector2 deathKick = new Vector2(25f,25f);
    AudioSource audioSource;
    
    private void Awake()
    {
        if(this.tag == "Player")
        {
            audioSource = new AudioSource();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "FlyingEnemy")
        {
            return;
        }
        else
        {
            if(other.tag == "Player")
            {
                other.GetComponent<player>().Die();
                AudioSource.PlayClipAtPoint(audioClip, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z));
                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                sr.color = new Color(1, 0, 0);
                other.GetComponent<Rigidbody2D>().velocity = deathKick;
                other.GetComponent<Animator>().SetTrigger("Dying");
                StartCoroutine(ChangeScreens(other));
            }
        }
    }

    IEnumerator ChangeScreens(Collider2D other)
    {
        if(other.tag == "Player")
        {
            yield return new WaitForSeconds(2f);
            other.GetComponent<AudioSource>().Stop();
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 0, 0, 0);
            SceneManager.LoadScene("Lose");
        }
    }
}
