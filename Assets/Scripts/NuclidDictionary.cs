using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NuclidDictionary : MonoBehaviour
{
    public Dictionary<KeyClassDict, Nuklidclass> nucliddata;
    public TextAsset DataFile;
    // Start is called before the first frame update
    private void Awake()
    {
        nucliddata = new Dictionary<KeyClassDict, Nuklidclass>();

        //nucliddata.Add(new KeyClassDict(0, 2), new Nuklidclass("Pb", "222", "11437", 50, 50, 0, 0));
        //nucliddata.Add( new KeyClassDict(3, 2), new Nuklidclass("N", "35", "11337", 0, 100, 0, 0));
        //nucliddata.Add( new KeyClassDict(5, 4), new Nuklidclass("H", "74", "55437", 0, 0, 0, 100));

        var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        string[] lines = DataFile.text.Split('\n');
        foreach (var item in lines)
        {
            if (item[16] == ' ')
            {
                string name = item.Substring(11, 5).TrimStart(digits).Trim();
                int protonkey = System.Int32.Parse(item.Substring(4, 3).TrimStart(new char[] { '0' }));
                int ordinalnumber = System.Int32.Parse(item.Substring(0, 3).TrimStart(new char[] { '0' }));
                int neutronkey = ordinalnumber - protonkey;
                float halflifeperiod = 0;
                string halflifeunit = item.Substring(78, 2).Trim();
                float alphadecay = 0;
                float betaplusdecay = 0;
                float betaminusdecay = 0;
                bool stabildecay = false;
                if (item.Substring(70, 4) == "stbl")stabildecay=true;
                //Debug.Log(item.Substring(70, 4));

                string decaymode = item.Substring(119).Trim();
                string[] decays = decaymode.Split(';');
                foreach (string mode in decays)
                {
                    if (mode.StartsWith("A=") || mode.StartsWith("A~"))
                    {
                        //alphadecay = Single.Parse(mode.Split(' ')[0].Split('=')[1]);

                        try
                        {
                            decimal alpha = Decimal.Parse(mode.Split(' ')[0].Split('=', '~')[1], new CultureInfo("en-US", false)) /100;
                            alphadecay = (float)alpha; 
                            //if (alphadecay > 1) Debug.Log(alphadecay);
                        }
                        catch (FormatException)
                        {
                            //Debug.Log(mode.Split(' ')[0].Split('=')[1]);
                        }
                    }

                    if (mode.StartsWith("B-=") || mode.StartsWith("B-~"))
                    {
                        //betaminusdecay = Single.Parse(mode.Split(' ')[0].Split('=')[1]);

                        try
                        {
                            decimal betaminus = Decimal.Parse(mode.Split(' ', '#', '[')[0].Split('=', '~')[1], new CultureInfo("en-US", false)) /100;
                            betaminusdecay = (float)betaminus;
                            //if (betaminusdecay > 0) Debug.Log(betaminusdecay);
                        }
                        catch (FormatException)
                        {
                            //Debug.Log(mode.Split(' ')[0].Split('=')[1]);
                        }
                    }

                    if (mode.StartsWith("B+=") || mode.StartsWith("B+~"))
                    {
                        //betaplusdecay = Single.Parse(mode.Split(' ')[0].Split('=')[1]);

                        try
                        {
                            decimal betaplus = Decimal.Parse(mode.Split(' ', '#')[0].Split('=', '~')[1], new CultureInfo("en-US", false)) /100;
                            betaplusdecay = (float)betaplus; 
                        }
                        catch (FormatException)
                        {
                            //Debug.Log(mode.Split(' ', '#')[0].Split('=')[1]);
                        }
                    }
                }

                string halflifemode = item.Substring(69, 9).Trim();
                try
                {
                    decimal halflife = Decimal.Parse(halflifemode, new CultureInfo("en-US", false));
                    halflifeperiod = (float)halflife;

                }
                catch (FormatException)
                {

                }

                if (ordinalnumber.ToString() + name == "40K") stabildecay = true;
                if (ordinalnumber.ToString() + name == "128Te") stabildecay = true;
                if (ordinalnumber.ToString() + name == "124Xe") stabildecay = true;
                if (ordinalnumber.ToString() + name == "78Kr") stabildecay = true;
                if (ordinalnumber.ToString() + name == "136Xe") stabildecay = true;
                if (ordinalnumber.ToString() + name == "76Ge") stabildecay = true;
                if (ordinalnumber.ToString() + name == "130Ba") stabildecay = true;
                if (ordinalnumber.ToString() + name == "82Se") stabildecay = true;
                if (ordinalnumber.ToString() + name == "116Cd") stabildecay = true;
                if (ordinalnumber.ToString() + name == "48Ca") stabildecay = true;
                if (ordinalnumber.ToString() + name == "209Bi") stabildecay = true;
                if (ordinalnumber.ToString() + name == "96Cr") stabildecay = true;
                if (ordinalnumber.ToString() + name == "130Te") stabildecay = true;
                if (ordinalnumber.ToString() + name == "150Nd") stabildecay = true;
                if (ordinalnumber.ToString() + name == "100Mo") stabildecay = true;
                if (ordinalnumber.ToString() + name == "151Eu") stabildecay = true;
                if (ordinalnumber.ToString() + name == "180W") stabildecay = true;
                if (ordinalnumber.ToString() + name == "50V") stabildecay = true;
                if (ordinalnumber.ToString() + name == "113Cd") stabildecay = true;
                if (ordinalnumber.ToString() + name == "148Sm") stabildecay = true;
                if (ordinalnumber.ToString() + name == "144Nd") stabildecay = true;
                if (ordinalnumber.ToString() + name == "186Os") stabildecay = true;
                if (ordinalnumber.ToString() + name == "174Hf") stabildecay = true;
                if (ordinalnumber.ToString() + name == "115ln") stabildecay = true;
                if (ordinalnumber.ToString() + name == "152Gd") stabildecay = true;
                if (ordinalnumber.ToString() + name == "190Pt") stabildecay = true;
                if (ordinalnumber.ToString() + name == "147Sm") stabildecay = true;
                if (ordinalnumber.ToString() + name == "138La") stabildecay = true;
                if (ordinalnumber.ToString() + name == "87Rb") stabildecay = true;
                if (ordinalnumber.ToString() + name == "187Re") stabildecay = true;
                if (ordinalnumber.ToString() + name == "176Lu") stabildecay = true;
                if (ordinalnumber.ToString() + name == "232Th") stabildecay = true;
                if (ordinalnumber.ToString() + name == "238U") stabildecay = true;
                if (ordinalnumber.ToString() + name == "235U") stabildecay = true;
                if (ordinalnumber.ToString() + name == "244Pu") stabildecay = true;
                if (ordinalnumber.ToString() + name == "146Sm") stabildecay = true;

                nucliddata.Add(new KeyClassDict(protonkey, neutronkey), new Nuklidclass(name, ordinalnumber.ToString(), halflifeperiod, halflifeunit, alphadecay, betaplusdecay, betaminusdecay, stabildecay));

            }

        }
    }


    public KeyClassDict RayIntersection(Vector3 origin, Vector3 direction)
    {

        int layer = 6;

        int layerMask = 1 << layer;

        RaycastHit hit;

        KeyClassDict returnValue = new KeyClassDict();

        if (Physics.Raycast(origin, direction, out hit, 10f, layerMask))
        {
            returnValue = new KeyClassDict(Mathf.RoundToInt(hit.transform.localPosition.z), Mathf.RoundToInt(hit.transform.localPosition.x));

        }
        else
        {
            returnValue = null;
        }

        return returnValue;
    }
}
