using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeForm : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 100)]
    public float reproductionRate; //Chance to reproduce at each step
    [Range(0, 100)]
    public float reproductionVariance;  //the potential change in reproductionRate

    [Range(0, 100)]
    public float deathRate;        //Chance to Die at each step
    [Range(0, 100)]
    public float deathVariance;   
    
    [Range(0, 100)]
    public float mutationChance;   //Chance there will be a mutation after reproduction
    [Range(0, 100)]
    public float mutationChanceVariance;

    public float moveSpeed;
    public float moveSpeedVariance;

    [Range(0, 100)]
    public float vision;
    [Range(0, 100)]
    public float visionVariance;

    [Range(0, 100)]
    public float size;
    [Range(0, 100)]
    public float sizeVariance;

    [Range(0, 100)]
    public float hunger;        //How much the life form can eat
    [Range(0, 100)]
    public float hungerVariance;

    public float eatRange;  //how close something has to be to get eaten
    float nutrition;        //how much this lifeform has eaten. Being at 0 for 2 steps is instant death

    public void Step()
    {
        if(Random.Range(0.0f, 100f) < deathRate) { Die(); }
        else if(Random.Range(0.0f, 100f) < reproductionRate) { }
    }

    void Reproduce()
    {
        LifeForm offspring = (LifeForm)Instantiate(this);
        if(Random.Range(0f, 100f) < mutationChance)
        {
            //Spawn another LifeForm with a varied stat
            int rand = Random.Range(1, 8);

            switch (rand)
            {
                case 1:
                    offspring.reproductionRate = MutateReproductionRate();
                    break;
                case 2:
                    offspring.deathRate = MutateDeathRate();
                    break;
                default:
                    break;
            }
        }
    }

    float MutateReproductionRate()
    {
        float rate = reproductionRate + Random.Range(-reproductionVariance, reproductionVariance);

        return rate;
    }
    float MutateDeathRate()
    {
        float rate = deathRate + Random.Range(-deathVariance, deathVariance);

        return rate;
    }


    Vector3 GetOffspringPosition()
    {
        return transform.position + (transform.right * size);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public float GetEaten()
    {
        float carbs = nutrition + size * 0.5f;
        Die();
        return carbs;
    }
}
