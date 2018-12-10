using System.Collections;
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
            total = 100; //to be later changed to allow save/load
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


    public int GetLevel() { return level; } //return the amount of nebulite aquired 
    public int GetTotal() { return total; } //return the total nebulite available to spend

    public void AddNebulite(int num) { level += num; } //add nebulite to count collected in level

    public void RemoveNebulite(int num) { total -= num; } //spend nebulite

    public void ResetNebulite() { level = 0; } //when exiting mission or dying nebulute gathered in level is reset

    //When level is completed the gathered nebulute is added to the total
    public void UpdateNebulite()
    {
        total += level;
        level = 0;
    }
}
