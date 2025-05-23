using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;


public class NuclidGenerator : MonoBehaviour
{
    public bool Smallmap;
    public GameObject nuclidePrefab;
    public NuclidDictionary nucliddata;
    public GameObject nuclide;
    public Decayrow rows;
    public bool isFloor;
    public HandSelection handselector;

    // Start is called before the first frame update
    void Start()
    {


        foreach (KeyValuePair<KeyClassDict, Nuklidclass> entry in nucliddata.nucliddata)
        {
            if (!Smallmap || (entry.Key.protonnumber > 78 && entry.Key.protonnumber < 89) && (entry.Key.neutronnumber > 121 && entry.Key.neutronnumber < 137))
            {
                GameObject nuclid = Instantiate(nuclidePrefab);
                nuclid.transform.position = new Vector3(entry.Key.neutronnumber * 1, 0, entry.Key.protonnumber * 1);
                //Debug.Log(entry.Value.nuclidname);
                float alpha = entry.Value.alphadecay;
                float betaplus = entry.Value.betaplusdecay;
                float betaminus = entry.Value.betaminusdecay;
                float sum = alpha + betaplus + betaminus;
                if (sum > 0) alpha = alpha / sum;
                if (sum > 0) betaplus = betaplus * 1 / sum;
                if (sum > 0) betaminus = betaminus * 1 / sum;
                nuclid.GetComponent<NuclidPrefabElements>().alphaplane.transform.localScale = new Vector3(0.1f * alpha, 0.1f, 0.1f);
                nuclid.GetComponent<NuclidPrefabElements>().alphaplane.transform.localPosition = new Vector3(-0.5f + 0.5f * alpha, 0.11f, 0);
                nuclid.GetComponent<NuclidPrefabElements>().betaplusplane.transform.localScale = new Vector3(0.1f * betaplus, 0.1f, 0.1f);
                nuclid.GetComponent<NuclidPrefabElements>().betaplusplane.transform.localPosition = new Vector3(-0.5f + alpha + 0.5f * betaplus, 0.11f, 0);
                nuclid.GetComponent<NuclidPrefabElements>().betaminusplane.transform.localScale = new Vector3(0.1f * betaminus, 0.1f, 0.1f);
                nuclid.GetComponent<NuclidPrefabElements>().betaminusplane.transform.localPosition = new Vector3(-0.5f + alpha + betaplus + 0.5f * betaminus, 0.11f, 0);
                if (entry.Value.stabildecay && sum == 0) nuclid.GetComponent<NuclidPrefabElements>().stabilplane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                else if (entry.Value.stabildecay)
                {
                    nuclid.GetComponent<NuclidPrefabElements>().alphaplane.transform.localScale = new Vector3(0.1f * alpha, 0.1f, 0.05f);
                    nuclid.GetComponent<NuclidPrefabElements>().alphaplane.transform.localPosition = new Vector3(-0.5f + 0.5f * alpha, 0.11f, -0.25f);
                    nuclid.GetComponent<NuclidPrefabElements>().betaplusplane.transform.localScale = new Vector3(0.1f * betaplus, 0.1f, 0.05f);
                    nuclid.GetComponent<NuclidPrefabElements>().betaplusplane.transform.localPosition = new Vector3(-0.5f + alpha + 0.5f * betaplus, 0.11f, -0.25f);
                    nuclid.GetComponent<NuclidPrefabElements>().betaminusplane.transform.localScale = new Vector3(0.1f * betaminus, 0.1f, 0.05f);
                    nuclid.GetComponent<NuclidPrefabElements>().betaminusplane.transform.localPosition = new Vector3(-0.5f + alpha + betaplus + 0.5f * betaminus, 0.11f, -0.25f);
                    nuclid.GetComponent<NuclidPrefabElements>().stabilplane.transform.localScale = new Vector3(0.1f, 0.1f, 0.05f);
                    nuclid.GetComponent<NuclidPrefabElements>().stabilplane.transform.localPosition = new Vector3(0, 0.11f, 0.25f);
                }

                nuclid.GetComponent<NuclidPrefabElements>().nuclidname.text = entry.Value.nuclidname;
                nuclid.GetComponent<NuclidPrefabElements>().ordinalnumber.text = entry.Value.ordinalnumber;
                if (entry.Value.halflifeperiod > 0) nuclid.GetComponent<NuclidPrefabElements>().halflifetime.text = entry.Value.halflifeperiod.ToString() + " " + entry.Value.halflifeunit;

                InteractableUnityEventWrapper eventWrapper;// = new InteractableUnityEventWrapper();
                if (nuclid.TryGetComponent<InteractableUnityEventWrapper>(out eventWrapper))
                {
                    eventWrapper.WhenUnselect.AddListener(delegate { handselector.onUnselect(entry.Key); });
                    eventWrapper.WhenSelect.AddListener(delegate { handselector.onSelect(entry.Key); });
                    eventWrapper.WhenHover.AddListener(delegate { handselector.onHover(entry.Key); });
                    eventWrapper.WhenUnhover.AddListener(delegate { handselector.onUnhover(entry.Key); });


                }


                nuclid.transform.parent = nuclide.transform;
                entry.Value.Prefab = nuclid;
            }
        }
        // Nuklidclass Thorium = new Nuklidclass();
        //if (nucliddata.nucliddata.TryGetValue(new KeyClassDict(2, 2), out Thorium)) Thorium.Prefab.GetComponent<NuclidPrefabElements>().SetOutlineColor(Color.red);

        rows.SetStart();
        if (isFloor)
        {
            if(rows.ActiveScene == Decayrow.Scene.FloorWalk) nuclide.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
            else nuclide.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else
        {
            nuclide.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            nuclide.transform.localPosition = new Vector3(115.73f, -25.5f, 107.2f);
            nuclide.transform.localRotation = Quaternion.Euler(-90, 90, 0);

        }
    }


}
