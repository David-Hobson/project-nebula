using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

    private bool paused = false;

    private GameObject bullet;

    private Animator animator;
    private AudioSource audSource;
    private AudioClip shot;

    public Canvas pauseCanvas;

    private int health;
    private int maxHealth;

    private int armour;
    private int maxArmour;
    private float speed;

    public void Start() {
        this.Construct();
    }

    public void Construct(){
        animator = this.GetComponent<Animator>();
        audSource = this.GetComponent<AudioSource>();
        shot = audSource.clip;
        bullet = Resources.Load<GameObject>("Prefabs/bulleta");
        health = 100;
        maxHealth = 100;
        armour = 100;
        maxArmour = 100;
        speed = 1;
    }

    private void Update() {

        if (!paused) {

            this.CalculateMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            this.MovementAnimation(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("P1R1")) {
                Fire();
            }

        }

        if (Input.GetButtonDown("P1Opt")) {
            Pause();
        }


    }

    public void CalculateMovement(float x, float y){
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
    }

    public void MovementAnimation(float x, float y){
        if (x < 0.01 && y < 0.01) {
            animator.StopPlayback();
        }

        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {
            animator.SetBool("Moving", true);

            if (x > 0) {
                animator.SetFloat("LastX", 1);
            } else if (x < 0) {
                animator.SetFloat("LastX", -1);
            } else {
                animator.SetFloat("LastX", 0);
            }

            if (y > 0) {
                animator.SetFloat("LastY", 1);
            } else if (y < 0) {
                animator.SetFloat("LastY", -1);
            } else {
                animator.SetFloat("LastY", 0);
            }

        } else {
            animator.SetBool("Moving", false);
        }

        animator.SetFloat("SpeedX", x);
        animator.SetFloat("SpeedY", y);
    }

    public void Fire() {
        audSource.PlayOneShot(shot, 1f);
        Instantiate(bullet, this.transform.position, Quaternion.identity);
    }

    public int GetArmour(){
        return armour;
    }

    public float GetSpeed(){
        return speed;
    }

    public int GetMaxArmour(){
        return maxArmour;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }

    public void Upgrade(int type){
        if(type == 1){
            maxHealth += 100;
        }else if(type == 2){
            maxArmour += 50;
        }else if(type == 3){
            speed += 0.5f;
        }
    }

    public GameObject Damage(int damageAmount){
        health -= damageAmount;

        if(health <= 0){
            this.enabled = false;
            var duplicate = Instantiate(this.gameObject);
            duplicate.SetActive(false);
            Destroy(this.gameObject);
            return duplicate;
        }

        return null;
    }

    public void DamageArmour(int damageAmount){
        this.armour -= damageAmount;
    }

    public void Heal(int healAmount){
        health += healAmount;
        if (health >= maxHealth) {
            health = maxHealth;
        }

    }

    public void HealArmour(int healAmount){
        this.armour += healAmount;
        if (armour >= maxArmour) {
            armour = maxArmour;
        }
    }

    public int GetHealth(){
        return this.health;
    }

    public void Respawn(){
        this.gameObject.SetActive(true);
    }

    public string Dodge(){
        return "Dodging";
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            health -= 20;
            Debug.Log("current health is " + health);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 10;
            Debug.Log("current health is " + health);
        }
    }

    private void Pause() {
        if (!paused) {
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
            paused = true;
        } else {
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
            paused = false;
        }
    }

    public GameObject GetBullet(){
        return bullet;
    }

}