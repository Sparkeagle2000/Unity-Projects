using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    Animator anim;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    private int scoreValue = 0;
    private int lives=3;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text="";
        SetLivesText ();
        musicSource.clip = musicClipOne;
          musicSource.Play();
          musicSource.loop=true;
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            anim.SetInteger("State", 2);
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            anim.SetInteger("State", 2);
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue==4)
            {
                transform.position = new Vector2(-10.0f, 4.5f);
                lives=3;
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue==8)
        {
            winText.text="You Win! Game created by Tonnie Hovanec.     (Credits to Reachground for the song Now you're a hero.) ";
            Destroy(this);
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
          other.gameObject.SetActive(false);
          lives = lives - 1;  
          SetLivesText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
        if (collision.collider.tag == "Wall")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(1, 3), ForceMode2D.Impulse);
            }
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString ();

        if (lives == 0)
        {
            winText.text = "You lost! Game created by Tonnie Hovanec.      (Credits to Reachground for the song Now you're a hero.) ";
            Destroy(this);
        }  
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
