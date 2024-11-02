using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public AudioClip HurtSound;

    public GameObject BulletPrefab;
    public GameObject John;

    private float LastShoot;
    private float DelayShoot = 0.25f;
    private int Health = 3; //Para la vida

    private void Update()
    {

        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) { transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }
        else { transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + DelayShoot)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot ()
    {
        Debug.Log("Shoot Grunt");
        Vector3 direction;
        if (transform.localScale.x == 1.0f) { direction = Vector3.right; }
        else { direction = Vector3.left; }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(HurtSound);
        if (Health == 0) { Destroy(gameObject); }
    }
}
