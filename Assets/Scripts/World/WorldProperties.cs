using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProperties : MonoBehaviour
{
    public float stepTime;          //Time between steps
    public int maxSteps;           //Number of steps before the sim cuts off.
    public float populationLimit; //Death Rate doubles for all life when the limit is reached

    int stepCount;

    public GameObject starterLifeform;

    public List<LifeForm> life;
    public List<LifeForm> newBornQueue;

    bool activeLife;

    public bool overPopulated { get { return life.Count >= populationLimit; } }

    public static WorldProperties world;

    void Awake()
    {
        life = new List<LifeForm>();
        newBornQueue = new List<LifeForm>();

        //create first lifeform
        life.Add(Instantiate(starterLifeform).GetComponent<LifeForm>());

        activeLife = true;

        world = this;
        stepCount = 0;

        StartCoroutine("LifeGoesOn");
    }

    IEnumerator LifeGoesOn()
    {
        while (activeLife && stepCount < maxSteps)
        {
            yield return new WaitForSeconds(stepTime);

            life.AddRange(newBornQueue);
            newBornQueue.Clear();

            foreach (LifeForm livingThing in life)
            {
                livingThing.Step();             //replace with burst compiler
            }
            stepCount++;
            //Debug.Log(stepCount);
        }
    }
}
