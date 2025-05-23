using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuklidclass
{
    public string nuclidname;
    public string ordinalnumber;
    public float halflifeperiod;
    public string halflifeunit;
    public float alphadecay;
    public float betaplusdecay;
    public float betaminusdecay;
    public bool stabildecay;
    public GameObject Prefab;

    public Nuklidclass()
    {

    }

    public Nuklidclass(string nuclidname, string ordinalnumber, float halflifeperiod, string halflifeunit, float alphadecay, float betaplusdecay, float betaminusdecay, bool stabildecay)
    {
        this.nuclidname = nuclidname;
        this.ordinalnumber = ordinalnumber;
        this.halflifeperiod = halflifeperiod;
        this.halflifeunit = halflifeunit;
        this.alphadecay = alphadecay;
        this.betaplusdecay = betaplusdecay;
        this.betaminusdecay = betaminusdecay;
        this.stabildecay = stabildecay;
    }

}
