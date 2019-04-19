using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FossilFuel : MonoBehaviour {
    private float timeBtwTrans;
    private float startTimeBtwTrans;
    private float transparentLevel = 0.2f;

    private bool finishTrans;

    private Animator FossAnim;

    private GameObject player1;
    private GameObject player2;
    // Use this for initialization
    void Start () {
        finishTrans = false;
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        FossAnim = GetComponent<Animator>();

        startTimeBtwTrans = 1.0f;
        timeBtwTrans = startTimeBtwTrans;

        GetComponent<SpriteRenderer>().color = new Color(243f, 255f, 0f, transparentLevel);
    }
	
	// Update is called once per frame
	void Update () {
        Transparent();
	}

    public void Transparent()
    {
        if (transparentLevel < 1.0f)
        {
            if (timeBtwTrans <= 0)
            {
                transparentLevel = transparentLevel + 0.4f;
                GetComponent<SpriteRenderer>().color = new Color(243f, 255f, 0f, transparentLevel);
                timeBtwTrans = startTimeBtwTrans;
            }
            else
            {
                timeBtwTrans -= Time.deltaTime;
            }
        }
        else
        {
            finishTrans = true;
            FossAnim.SetBool("TransparentDone", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(finishTrans == true)
        {
            if (other.gameObject.name == "Fire")
            {
                Destroy(gameObject);
            }
            if (other.gameObject.name == "Player 1")
            {
                player1.GetComponent<Player1Controller>().Damage(5);
            }
            if (other.gameObject.name == "Player 2")
            {
                player2.GetComponent<Player2Controller>().Damage(5);
            }
        }
    }
}
