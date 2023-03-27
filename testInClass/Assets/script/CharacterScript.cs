using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterScript : MonoBehaviour
{
    public float jumpForce = 500f;
    public float speed = 10f;

    bool jump = false;
    bool gamestarted = false;
    bool grounded = false;
    Rigidbody2D rigidbody;
    Animator anim;
    AudioSource audioSource;

    public GameObject ending;
    public AudioMixerSnapshot death;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        ending.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            if ((grounded == true) && (gamestarted == true))
            {
                jump = true;
                grounded = false;
                anim.SetTrigger("Jump");
                audioSource.Play();
            }
            else
            {
                gamestarted = true;
                anim.SetTrigger("Start");
            }
        }
        anim.SetBool("Grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        if (collision.gameObject.CompareTag("Respawn"))
        {
            death.TransitionTo(.00f);

            rigidbody.gravityScale = 0f;
            speed = 0;
            jumpForce = 0;
            ending.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (gamestarted == true)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }
        if (jump == true)
        {
            rigidbody.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

}
