using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomState
{
    public enum Probability : short { p05, p10, p15, //19 total
                                 p20, p25, p30, p35,
                                 p40, p45, p50, p55,
                                 p60, p65, p70, p75,
                                 p80, p85, p90, p95 }

    public RandomState(Probability basicProbability)
    {
        bearingProbability = pseudoRandomDistribution[(int)basicProbability];
        callAfterProcNumber = 0;
    }

    public bool Get()
    {
        callAfterProcNumber++;
        bool randomState = Random.value <= bearingProbability * callAfterProcNumber;
        if (randomState) {
            callAfterProcNumber = 0;
        }
        return randomState;
    }
    


    float bearingProbability;
    int callAfterProcNumber;
    float[] pseudoRandomDistribution = new float[]    {0.003800f, 0.014745f, 0.032220f,   //0.05-15
                                            0.055705f, 0.084745f, 0.118955f, 0.157975f,   //0.20-35
                                            0.201505f, 0.249300f, 0.302100f, 0.360430f,   //0.40-55
                                            0.422650f, 0.481150f, 0.571500f, 0.666750f,   //0.60-75
                                            0.750000f, 0.823570f, 0.888870f, 0.947410f }; //0.80-95
                                                                                          //19 total
}