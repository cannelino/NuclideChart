using RengeGames.HealthBars;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSelection : MonoBehaviour
{
    public GameObject Avatar;
    public Transform Cam;
    public Decayrow decayrow;
    private KeyClassDict currentnuklid;
    private KeyClassDict startreference;
    private DateTime startTime;
    public UltimateCircularHealthBar timebar;
    public NuclidDictionary nucliddata;
    private bool hascounted = false;


    // Start is called before the first frame update
    void Start()
    {
        currentnuklid = new KeyClassDict(999, 999);
        startreference = new KeyClassDict(999, 999);
        timebar.gameObject.SetActive(false);
    }

    public void setnuclidstart(KeyClassDict start)
    {
        currentnuklid = start;
    }

    // Update is called once per frame
    void Update()
    {
        int layer = 6;

        int layerMask = 1 << layer;

        RaycastHit hit;

        KeyClassDict returnValue = new KeyClassDict();

        if (Physics.Raycast(Cam.position, -Vector3.up, out hit, 3.5f, layerMask))
        {
            returnValue = new KeyClassDict(Mathf.RoundToInt(hit.transform.localPosition.z), Mathf.RoundToInt(hit.transform.localPosition.x));
            if (returnValue.Equals(currentnuklid))
            {
                TimeSpan elapsedTime = DateTime.Now - startTime;
                if (timebar.gameObject.activeSelf)
                {
                    timebar.SetPercent((float)elapsedTime.TotalSeconds / 2.0f);
                    timebar.transform.position = hit.point;
                    if (elapsedTime.TotalSeconds > 2)
                    {
                        decayrow.checkSelected(currentnuklid);
                        timebar.gameObject.SetActive(false);

                    }
                }
                currentnuklid = returnValue;
                if (elapsedTime.TotalSeconds > 2 && !hascounted)
                {
                    decayrow.interactionCount++;
                    hascounted = true;
                }

            }
            else
            {
                timebar.gameObject.SetActive(false);
                hascounted = false;
                Nuklidclass nuklid = new Nuklidclass();
                if (nucliddata.nucliddata.TryGetValue(returnValue, out nuklid))
                {
                    if (nuklid.Prefab.GetComponent<NuclidPrefabElements>().selected == false)
                    {
                        timebar.gameObject.SetActive(true);

                    }
                }
                currentnuklid = returnValue;
                startTime = DateTime.Now;

            }
            Avatar.transform.position = hit.point;
        }
        else
        {
            returnValue = null;
        }

    }
}
