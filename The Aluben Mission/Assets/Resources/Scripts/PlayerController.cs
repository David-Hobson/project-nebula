using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool paused = false;

    public GameObject bullet;
    private GameObject gunPivot;
    private GameObject currentWeapon;
    private GameObject muzzleFlash;

    private GameObject energyLink;
    private bool energized;

    private Animator animator;
    private AudioSource audSource;
    private AudioClip shot;

    private float health;
    private float maxHealth;

    private float armour;
    private float maxArmour;
    private float speed;

    private Vector3 knockbackDirection;
    private float knockBackTime;
    private bool isKnockedBack;
	private bool isHolding;
    private float maxKnockBackTime;
    private float knockBackPower;

    private float damagedColor;

    private bool isInvincible;
    private float invicibleTime;
    private float maxInvicibilityTime;

    private bool isAiming;

    private bool interaction;

    private bool inDialogue;

    //Buttons used for controller input
    public string RSX;
    public string RSY;
    public string LSX;
    public string LSY;
    public string btnX;
    public string btnR1;
    public string btnL1;


    public void Start() {
        this.Construct();

    }

    //Construct the Player prefab with the corresponding stats
    public void Construct(){
        animator = this.GetComponent<Animator>();
        audSource = this.GetComponent<AudioSource>();

        shot = audSource.clip;
        gunPivot = this.transform.GetChild(0).gameObject;
        currentWeapon = gunPivot.transform.GetChild(0).gameObject;
        muzzleFlash = Resources.Load<GameObject>("Prefabs/MuzzleFlash");

        health = 100;
        maxHealth = 100;
        armour = 100;
        maxArmour = 100;
        speed = 1;

        knockbackDirection = new Vector3(0,0,0);
        knockBackTime = 0;
        isKnockedBack = false;
        maxKnockBackTime = 0.3f;
        knockBackPower = 1.5f;

        isInvincible = false;
        invicibleTime = 0;
        maxInvicibilityTime = 2f;

        inDialogue = false;

		isHolding = false;

        energyLink = this.transform.GetChild(1).gameObject;
        energized = false;
    }

    private void Update() {

        //Check if player is in dialogue
        if (!inDialogue) {

            //Send controller information to various methods for movement, animation, and aiming weapon
            this.CalculateMovement(Input.GetAxis(LSX), Input.GetAxis(LSY));
            this.DirectionAnimation(Input.GetAxis(RSX), Input.GetAxis(RSY));
            this.MovementAnimation(Input.GetAxis(LSX), Input.GetAxis(LSY), Input.GetAxis(RSX), Input.GetAxis(RSY));
            this.AimWeapon(Input.GetAxis(LSX), Input.GetAxis(LSY), Input.GetAxis(RSX), Input.GetAxis(RSY));

            this.CalculateKnockback();
            this.CalculateInvincibility();

            if (Input.GetButtonDown(btnR1) && isAiming && !isHolding) {
                Fire();
            }

        }

        if (Input.GetButtonDown(btnX)) {
            interaction = true;
        } else {
            interaction = false;
        }

        if (Input.GetButtonDown(btnL1)){
            this.SetEnergyLink(true);
        }

        if (Input.GetButtonUp(btnL1)){
            this.SetEnergyLink(false);
        }


    }

    //REQUIREMENT: F-8, F-48, F-50
    //Move the player object based off of a X and Y values
    public void CalculateMovement(float x, float y){
        if(!isKnockedBack){
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(x, y, 0) * speed;
        }
    }

    //Aim the weapon which prioritizes the left stick when not aiming with the right stick
    private void AimWeapon(float lx, float ly, float rx, float ry){
        if (Mathf.Abs(rx) > 0.5 || Mathf.Abs(ry) > 0.5) {
            this.WeaponDirection(rx, ry);
            currentWeapon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            isAiming = true;
        } else if (Mathf.Abs(lx) > 0.5 || Mathf.Abs(ly) > 0.5) {
            this.WeaponDirection(lx, ly);
            currentWeapon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            isAiming = true;
        } else {
            currentWeapon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            isAiming = false;
        }
    }

    //Displaces the character away from enemies after being hit
    //Helps prevent players from being continiously hit and allows them to recover
    public void CalculateKnockback(){
        if (isKnockedBack) {
            this.GetComponent<Rigidbody2D>().velocity = knockbackDirection * 1.5f;
            knockBackTime += Time.deltaTime;
            if (knockBackTime >= maxKnockBackTime) {
                isKnockedBack = false;
            }
        }
    }

    //Makes the player invincible for a short period of time
    //Helps prevent players from being continiously hit and allows them to recover
    public void CalculateInvincibility(){
        if (isInvincible) {
            var spriteColor = this.GetComponent<SpriteRenderer>().color;
            this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.5f);
            invicibleTime += Time.deltaTime;

            energized = false;
            energyLink.GetComponent<SpriteRenderer>().enabled = energized;
            if (invicibleTime >= maxInvicibilityTime) {
                isInvincible = false;
                this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1f);
            }
        }
    }

    //Requirement: F-45
    //Rotate the equipped weapon based off of X and Y values
    public void WeaponDirection(float x, float y){

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

    //REQUIREMENT: F-46
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

    //REQUIREMENT: F-46, F-48
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

    //REQUIREMENT: F-47
    //Return the current armour amount
    public float GetArmour(){
        return armour;
    }

    //REQUIREMENT: F-51
    //Return the current speed
    public float GetSpeed(){
        return speed;
    }

    //REQUIREMENT: F-47
    //Return the maximum armour count
    public float GetMaxArmour(){
        return maxArmour;
    }

    //REQUIREMENT: F-33
    //Return the maximum health amount
    public float GetMaxHealth(){
        return maxHealth;
    }

    //Set the movement speed of the player
    public void SetSpeed(float speed){
        this.speed = speed;
    }


    //REQUIREMENT: F-17, F-47, F-51
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
    public GameObject Damage(int damageAmount, Vector3 damageDirection){
        health -= damageAmount;

        //Set knockback parameters
        isKnockedBack = true;
        knockBackTime = 0;
        knockbackDirection = damageDirection.normalized;

        //Set invicibility parameters
        isInvincible = true;
        invicibleTime = 0;

        isHolding = false;

        //Check to see if the player's health has reached 0
        //Duplicate the object for spawning purposes and remove the current one
        if (health <= 0){
            this.enabled = false;
            //Duplication needed for respawning
            var duplicate = Instantiate(this.gameObject);
            duplicate.SetActive(false);
            Destroy(this.gameObject);
            return duplicate;
        }

        return null;
    }

    //REQUIREMENT: F-47
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

    //REQUIREMENT: F-47
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


    //Return the current bullet object
    public GameObject GetBullet(){
        //TODO

        //Update bullet object based on currently equipped weapon
        return bullet;
    }


    //REQUIREMENT: F-49
    //Set current weapon to new Weapon object parameter
    public void EquipWeapon(Weapon w){
        //TODO

        //currentWeapon = w;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(!isInvincible){
            if (collision.gameObject.tag == "Enemy") {
                this.Damage(10, transform.position - collision.transform.position);

            }
        }

        if(collision.gameObject.tag == "HealthPack"){
            this.Heal(20);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!isInvincible) {
            if (collision.gameObject.tag == "Enemy") {
                this.Damage(10, transform.position - collision.transform.position);

            }
        }
    }

    //Return if the player is interacting with an interactable object
    public bool GetInteraction(){
        return this.interaction;
    }

    //Check if the player is in a dialogue sequence
    public bool GetInDialogue(){
        return inDialogue;
    }

    //Set the player to be in a dialogue sequence
    public void SetInDialogue(bool d){
        inDialogue = d;
    }

    //Set the player to be holding an object
	public void SetIsHolding(bool hold){
		isHolding = hold;
	}

    //Return if the player is holding something
    public bool GetIsHolding(){
        return isHolding;
    }

    //Toggles whether the player is holding something
    public void ToggleIsHolding(){
        isHolding = !isHolding;
    }

    //Toggle the energy link between the two players
    public void SetEnergyLink(bool set){
        energized = set;
        energyLink.GetComponent<SpriteRenderer>().enabled = set;
    }

    //Returns if the player is energized for the energy link
    public bool IsEnergized(){
        return this.energized;
    }
}
