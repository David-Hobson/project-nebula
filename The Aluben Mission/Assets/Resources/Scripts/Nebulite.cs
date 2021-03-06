﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebulite : MonoBehaviour
{

    public static Nebulite instance; //persists between levels

    private int total; //amount of nebulite successfully acquired and can use
    private int level; //the amount of nebulite gathered in a level

    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("P1Weapon", 0);
            PlayerPrefs.SetInt("P2Weapon", 0);
            total = 500; //to be later changed to allow save/load
            level = 0; //nebulite aquired starts at zero
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }


   
    public int GetLevel() { return level; } //return the amount of nebulite acquired 

    public int GetTotal() { return total; } //return the total nebulite available to spend

    //FR 24, 25
    //add nebulite to count collected in level
    public void AddNebulite(int num) { level += num; }

    //FR 25
    //spend nebulite
    public void RemoveNebulite(int num) { total -= num; }

    //FR 25
    //when exiting mission or dying nebulute gathered in level is reset
    public void ResetNebulite() { level = 0; } 

    //FR 23, 25
    //When level is completed the gathered nebulute is added to the total
    public void UpdateNebulite()
    {
        total += level;
        level = 0;
    }
}