using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBending : MonoBehaviour
{
    public float ThumbPercentage, IndexPercentage, MiddlePercentage, RingPercentage, PinkyPercentage;

    Animator anima;

    void Start()
    {
        anima = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Normalization(ThumbPercentage);
        // Normalization(IndexPercentage);
        // Normalization(MiddlePercentage);
        // Normalization(RingPercentage);
        // Normalization(PinkyPercentage);
        anima.SetFloat("ThumbBlend", ThumbPercentage);
        anima.SetFloat("IndexBlend", IndexPercentage);
        anima.SetFloat("MiddleBlend", MiddlePercentage);
        anima.SetFloat("RingBlend", RingPercentage);
        anima.SetFloat("PinkyBlend", PinkyPercentage);
        anima.SetFloat("Blend", 0);
    }

    // Normalization: is used to make sure the 0 =< percentage =<1 
    float Normalization(float percentage){
        return (percentage>1)? 1: ((percentage<0)? 0: percentage);
    }
}

