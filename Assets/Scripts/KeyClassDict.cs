using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClassDict //: IEqualityComparer<KeyClassDict>
{
    public int protonnumber;
    public int neutronnumber;

    public KeyClassDict()
    {

    }

    public KeyClassDict(int protonnumber, int neutronnumber)
    {
        this.protonnumber = protonnumber;
        this.neutronnumber = neutronnumber;
    }

    public override bool Equals(object obj)
    {
        KeyClassDict key = (KeyClassDict)obj;
        return (key.protonnumber == this.protonnumber && key.neutronnumber == this.neutronnumber);
    }

    public override int GetHashCode()
    {
        int hCode = this.protonnumber ^ this.neutronnumber;
        return hCode.GetHashCode();
    }
}
