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

    private bool touched1;
    private bool touched2;
    // Use this for initialization
    void Start () {
        finishTrans = false;
        touched1 = false;
        touched1 = false;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        FossAnim = GetComponent<Animator>();

        startTimeBtwTrans = 1.0f;
        timeBtwTrans = startTimeBtwTrans;

        GetComponent<SpriteRenderer>().color = new Color(243f, 255f, 0f, transparentLevel);

        InvokeRepeating("DoDamage", 1f, 1f);
        InvokeRepeating("NewTransparent", 0.1f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void NewTransparent()
    {
        if (transparentLevel < 1.0f)
        {
            transparentLevel += 0.1f;
            GetComponent<SpriteRenderer>().color = new Color(243f, 255f, 0f, transparentLevel);
        }
        else
        {
            finishTrans = true;
            FossAnim.SetBool("TransparentDone", true);
        }
    }
   
    public void DoDamage()
    {
        if(touched1 == true)
        {
            player1.GetComponent<PlayerController>().Damage(5, player1.transform.position - transform.position);
        }
        if(touched2 == true)
        {
            player2.GetComponent<PlayerController>().Damage(5, player2.transform.position - transform.position);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(finishTrans == true)
        {
            if (other.gameObject.name == "Player 1")
            {
                touched1 = true;
            }
            if (other.gameObject.name == "Player 2")
            {
                touched2 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            touched1 = false;
        }
        if (other.gameObject.name == "Player 2")
        {
            touched2 = false;
        }
    }
    


}
