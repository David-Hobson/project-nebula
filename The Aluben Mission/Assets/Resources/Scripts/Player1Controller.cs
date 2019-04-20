using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

    private bool paused = false;

    private GameObject bullet;
    private GameObject gunPivot;
    private GameObject currentWeapon;
    private GameObject muzzleFlash;

    private Animator animator;
    private AudioSource audSource;
    private AudioClip shot;

    public Canvas pauseCanvas;

    public float health;
    private float maxHealth;

    private float armour;
    private float maxArmour;
    private float speed;

    public void Start() {
        this.Construct();
    }

    //Construct the Player 1 prefab with the corresponding stats
    public void Construct(){
        animator = this.GetComponent<Animator>();
        audSource = this.GetComponent<AudioSource>();
        shot = audSource.clip;
        bullet = Resources.Load<GameObject>("Prefabs/P1Projectile");
        gunPivot = GameObject.Find("P1GunPivot");
        currentWeapon = GameObject.Find("P1Weapon");
        muzzleFlash = Resources.Load<GameObject>("Prefabs/MuzzleFlash");
        health = 100;
        maxHealth = 100;
        armour = 100;
        maxArmour = 100;
        speed = 1;
    }

    private void Update() {

        //Check if the game is pasused
        if (!paused) {

            //Send controller information to various methods for movement and animation
            this.CalculateMovement(Input.GetAxis("P1LSX"), Input.GetAxis("P1LSY"));
            this.DirectionAnimation(Input.GetAxis("P1RSX"), Input.GetAxis("P1RSY"));
            this.MovementAnimation(Input.GetAxis("P1LSX"), Input.GetAxis("P1LSY"), Input.GetAxis("P1RSX"), Input.GetAxis("P1RSY"));
            this.WeaponDirection(Input.GetAxis("P1RSX"), Input.GetAxis("P1RSY"));

            if (Input.GetButtonDown("P1R1")) {
                Fire();
            }

        }

        if (Input.GetButtonDown("P1Opt")) {
            //Pause();
        }


    }

    //REQUIREMENT: F-8, F-48
    //Move the player object based off of a X and Y values
    public void CalculateMovement(float x, float y){
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0);
    }

    //Rotate the equipped weapon based off of X and Y values
    public void WeaponDirection(float x, float y){
        var vect = new Vector2(x, y);
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        //Change the sprite order in order to create depth for when the gun is behind the player
        if(y > 0) {
            currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = 9;
            muzzleFlash.GetComponent<SpriteRenderer>().sortingOrder = 9;
        }else{
            currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = 10;
            muzzleFlash.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }

        gunPivot.GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    //REQUIREMENT: F-49
    //Rotate and animate the player based on X and Y values
    //This function is the default direction based on the left stick
    public void DirectionAnimation(float x, float y){
        if (x < 0.01 && y < 0.01) {
            animator.StopPlayback();
        }

        //Set LastX and LastY direction for idle animation to keep player facing the same way
        //it was when moving
        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {

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

        }

        animator.SetFloat("SpeedX", x);
        animator.SetFloat("SpeedY", y);
    }

    //REQUIREMENT: F-48, F-49
    //Rotate and animate the player based on X and Y values but prioritizes the right stick movement
    public void MovementAnimation(float x, float y, float xr, float xy){
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

        //Shift priority to left stick if not moving the right stick
        if(Mathf.Abs(xr) < 0.1 && Mathf.Abs(xy) < 0.1){
            animator.SetFloat("SpeedX", x);
            animator.SetFloat("SpeedY", y);
            this.WeaponDirection(x, y);
        }
    }

    //REQUIREMENT: F-9, F-11
    //Fire a projectile from the barrel position on the player
    public void Fire() {
        var barrelVect = currentWeapon.transform.Find("Barrel");
        audSource.PlayOneShot(shot, 1f);
        GameObject tempMuzzleFlash = Instantiate(muzzleFlash, barrelVect.position, currentWeapon.GetComponent<Transform>().rotation);
        tempMuzzleFlash.transform.parent = barrelVect;
        Instantiate(bullet, barrelVect.position, Quaternion.identity);
    }

    //REQUIREMENT: F-50
    //Return the current armour amount
    public float GetArmour(){
        return armour;
    }

    //REQUIREMENT: F-51
    //Return the current speed
    public float GetSpeed(){
        return speed;
    }

    //REQUIREMENT: F-50
    //Return the maximum armour count
    public float GetMaxArmour(){
        return maxArmour;
    }

    //REQUIREMENT: F-33
    //Return the maximum health amount
    public float GetMaxHealth(){
        return maxHealth;
    }


    //REQUIREMENT: F-17, F-50, F-51
    //Upgrade the health, armour, or speed based on the type of upgrade
    public void Upgrade(int type){
        if(type == 1){
            maxHealth += 100;
        }else if(type == 2){
            maxArmour += 50;
        }else if(type == 3){
            speed += 0.5f;
        }else{
            return;
        }
    }

    //REQUIREMENT: F-13, F-33
    //Damage player based on integer amount
    //When player's health reaches 0, make a copy destroy the current object
    public GameObject Damage(int damageAmount){
        health -= damageAmount;

        if(health <= 0){
            this.enabled = false;
            //Duplication needed for respawning
            var duplicate = Instantiate(this.gameObject);
            duplicate.SetActive(false);
            Destroy(this.gameObject);
            return duplicate;
        }

        return null;
    }

    //REQUIREMENT: F-50
    //Damage the player's armour based on integer amount
    public void DamageArmour(int damageAmount){
        this.armour -= damageAmount;
    }

    //REQUIRMENT: F-33
    //Heal player based on integer amount
    public void Heal(int healAmount){
        health += healAmount;
        if (health >= maxHealth) {
            health = maxHealth;
        }

    }

    //REQUIREMENT: F-50
    //Heal player armour based on integer amount
    public void HealArmour(int healAmount){
        this.armour += healAmount;
        if (armour >= maxArmour) {
            armour = maxArmour;
        }
    }

    //REQUIRMENT: F-33
    //Return the current health of the player
    public float GetHealth(){
        return this.health;
    }

    //REQUIREMENT: F-14
    //Respawn the player 
    public void Respawn(){
        this.gameObject.SetActive(true);
    }

    //REQUIREMENT: F-32
    //Set the player to dodging state
    public string Dodge(){
        //TODO

        //Disable player hitbox
        //Add directional force to player
        //Run player animation for dodge
        //Enable player hitbox when finished
        return "Dodging";
    }

    //REQUIREMENT: F-26, F-27, F-43, F-44
    //Pause the game when invokved, toggles between paused and unpaused
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

    //Return the current bullet object
    public GameObject GetBullet(){
        //TODO

        //Update bullet object based on currently equipped weapon
        return bullet;
    }


    //REQUIREMENT: F-52
    //Set current weapon to new Weapon object parameter
    public void EquipWeapon(Weapon w){
        //TODO

        //currentWeapon = w;
    }

}