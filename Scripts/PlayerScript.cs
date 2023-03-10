using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public GameObject WinText;
    public GameObject LoseText;
    public AudioClip WinSound;
    public AudioSource musicSource;
    private int scoreValue = 0;
    private int livesValue = 3;
    private bool facingRight = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
       rd2d = GetComponent<Rigidbody2D>(); 
       score.text = scoreValue.ToString();
       lives.text = livesValue.ToString();

       WinText.SetActive(false);
       LoseText.SetActive(false);

       anim = GetComponent<Animator>();
    }
    

    void Update()
    {

     if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
         }

     if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }

    if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
         }

     if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }

    if (Input.GetKeyDown(KeyCode.W))
        {
          anim.SetInteger("State", 2);
         }
    
    if (Input.GetKeyUp(KeyCode.W))
        {
          anim.SetInteger("State", 0);
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (scoreValue >= 9)
        {
           WinText.SetActive(true); 
        }

        if (livesValue <= 0)
        {
            LoseText.SetActive(true);
            Destroy(gameObject);
        }

         if (facingRight == false && hozMovement > 0)
         {
            Flip();
         }
         else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue >= 9)
       {
         musicSource.clip = WinSound;
         musicSource.Play();
         musicSource.loop = false;
       }
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

         if (scoreValue == 4)
        {
            transform.position = new Vector3(-79.49f, 46.73f, 90.0f);
            livesValue = 3;
            lives.text = livesValue.ToString();
        }

        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }



}
