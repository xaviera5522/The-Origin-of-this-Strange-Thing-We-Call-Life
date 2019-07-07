using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProperties : MonoBehaviour
{
    public float stepTime;         //Time between steps
    public float populationLimit; //Death Rate doubles for all life when the limit is reached

    public LifeForm starterLifeform;

    List<LifeForm> life;

    void Awake()
    {
        life = new List<LifeForm>();

        //create first lifeform
        life.Add(Instantiate(starterLifeform));

        StartCoroutine("LifeGoesOn");
    }

    IEnumerator LifeGoesOn()
    {
        foreach (LifeForm livingThing in life)
        {
            livingThing.Step();             //replace with burst compiler
        }

        yield return new WaitForSeconds(stepTime);
    }
}
