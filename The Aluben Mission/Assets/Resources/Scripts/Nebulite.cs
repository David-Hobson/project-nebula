using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebulite : MonoBehaviour
{

    private int total;
    private int level;

    public Nebulite(int start)
    {
        total = start;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetLevel() { return level; }
    public int GetTotal() { return total; }

    public void AddNebulite(int num) { level += num; }

    public void RemoveNebulite(int num) { total -= num; }

    public void ResetNebulite() { level = 0; }

    public void UpdateNebulite()
    {
        total += level;
        level = 0;
    }
}
