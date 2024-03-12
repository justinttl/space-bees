using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void GainHealth(float healthGain);
    public void LoseHealth(float heathLoss);
}
