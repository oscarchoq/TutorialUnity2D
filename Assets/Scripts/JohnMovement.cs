using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{

    public AudioClip JumpSound;
    public AudioClip HurtSound;
    public AudioClip DieSound;

    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;

    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private float DelayShoot = 0.25f;
    private int Health = 5; //Para la vida

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) { transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        else if (Horizontal > 0.0f) { transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }
        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1F, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else { Grounded = false; }
        
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(JumpSound);
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + DelayShoot){
            Shoot();
            LastShoot = Time.time;
        }

        verifyPositionJohn();
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) { direction = Vector3.right; }
        else { direction = Vector3.left; }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Health = Health - 1;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HurtSound);
        if (Health == 0) { 
            Camera.main.GetComponent<AudioSource>().Stop();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(DieSound);
            Destroy(gameObject);  
        }
    }

    private void verifyPositionJohn()
    {
        // posicion incial -3.368
        // Muerte en -3.749
        float posicionY = transform.position.y;
        
        if (posicionY <= -3.749f) {
            Debug.Log("Muerto por caida");
            Health = 0;
            Camera.main.GetComponent<AudioSource>().Stop();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(DieSound);
            Destroy(gameObject);
        }
    }
}
