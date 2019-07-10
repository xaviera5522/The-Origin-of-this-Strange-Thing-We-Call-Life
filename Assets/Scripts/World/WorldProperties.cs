using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldProperties : MonoBehaviour
{
    public float stepTime;          //Time between steps
    public int maxSteps;           //Number of steps before the sim cuts off.
    public float populationLimit; //Death Rate doubles for all life when the limit is reached

    int stepCount;              //Number of steps this Sim

    public GameObject starterLifeform;   //The first life form

    public List<LifeForm> life;     //Container for all life
    public List<LifeForm> newBornQueue;     //temporary container for new life

    bool activeLife;

    bool paused;                //Automode will pause when true

    public bool autoMode = true;       //Sim will Auto Step when true

    public bool overPopulated { get { return life.Count >= populationLimit; } }     //Death rate doubles when overpopulated

    public static WorldProperties world;    //Static instance

    void Awake()
    {
        life = new List<LifeForm>();
        newBornQueue = new List<LifeForm>();

        //create first lifeform
        life.Add(Instantiate(starterLifeform).GetComponent<LifeForm>());

        activeLife = true;
        paused = false;

        world = this;
        stepCount = 0;

        StartCoroutine("LifeGoesOn");
    }

    void Update()
    {
        if(Input.GetButtonUp("Pause"))
        {
            Pause();
        }
        if (Input.GetButtonDown("Step") && !autoMode)
        {
            WorldStep();
            Debug.Log(life.Count);
        }
    }

    IEnumerator LifeGoesOn()
    {
        while (activeLife && stepCount < maxSteps)
        {
            if (autoMode)
            {
                if (!paused)
                {
                    yield return new WaitForSeconds(stepTime);

                    WorldStep();
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void WorldStep()
    {
        life.AddRange(newBornQueue);
        newBornQueue.Clear();

        foreach (LifeForm livingThing in life)
        {
            livingThing.Step();             //replace with burst compiler
        }
        stepCount++;
        //Debug.Log(stepCount);
    }

    void Pause()
    {
        paused = !paused;
        //TODO: Stop all movement
    }
}
