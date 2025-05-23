using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelection : MonoBehaviour
{
   
    public Decayrow decayrow;
    public NuclidDictionary dictionary;
    public GameObject avatarpoint;
    public GameObject nuclidParent;
    private Transform currentParent;
    private bool TranslationStarted = false;
    private bool nuclidSelected = false;
    private bool isFist;
    private KeyClassDict currentNuklidKey;
    public Transform rightHand;
    public GameObject hoverIndicator;

    // Start is called before the first frame update
    void Start()
    {
        //InteractableUnityEventWrapper test = new InteractableUnityEventWrapper();
        //test.WhenSelect.AddListener();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFist && nuclidSelected && !TranslationStarted)
        {
            Nuklidclass nuklid = new Nuklidclass();
            if (dictionary.nucliddata.TryGetValue(currentNuklidKey, out nuklid))
            {
                nuklid.Prefab.transform.parent = null;
                nuclidParent.transform.parent = nuklid.Prefab.transform;
                currentParent = nuklid.Prefab.transform;
                TranslationStarted = true;
                nuclidSelected = false;
                avatarpoint.transform.parent = nuclidParent.transform;
                decayrow.interactionCount++;
            }
        }
        if (TranslationStarted)
        {
            if(isFist) currentParent.position = new Vector3(currentParent.position.x, rightHand.position.y, rightHand.position.z);
            else
            {
                TranslationStarted = false;
                nuclidParent.transform.parent = null;
                currentParent.parent = nuclidParent.transform;
                hoverIndicator.transform.parent = null;
                hoverIndicator.SetActive(false);
                avatarpoint.transform.parent = null;

            }
        }
    }

    public void startTranslating()
    {
        isFist = true;
        hoverIndicator.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1, 0.8f);
    }
    public void stopTranslating()
    {
        isFist = false;
        hoverIndicator.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,0.8f);
    }
    public  void onUnselect(KeyClassDict key)
    {
        //Debug.Log("Key:" + key.protonnumber + " ; " + key.neutronnumber);
        /*if (!TranslationStarted && nuclidSelected)
        {
            Nuklidclass nuklid = new Nuklidclass();
            if (dictionary.nucliddata.TryGetValue(currentNuklidKey, out nuklid))
            {
                decayrow.checkSelected(key);
                decayrow.interactionCount++;
                avatarpoint.transform.position = nuklid.Prefab.GetComponent<NuclidPrefabElements>().TeleportTarget.position;
            }

        }*/
        nuclidSelected = false;
    }

    public void onSelect(KeyClassDict key)
    {
        //Debug.Log("Key:" + key.protonnumber + " ; " + key.neutronnumber);
        //decayrow.checkSelected(key);
        nuclidSelected = true;
        currentNuklidKey = key;
        if (!TranslationStarted && nuclidSelected && !isFist)
        {
            Nuklidclass nuklid = new Nuklidclass();
            if (dictionary.nucliddata.TryGetValue(currentNuklidKey, out nuklid))
            {
                decayrow.checkSelected(key);
                decayrow.interactionCount++;
                avatarpoint.transform.position = nuklid.Prefab.GetComponent<NuclidPrefabElements>().TeleportTarget.position;
            }

        }
    }

    public void onHover(KeyClassDict key)
    {
        if (!TranslationStarted) { 
            hoverIndicator.SetActive(true);
            nuclidSelected = true;
            currentNuklidKey = key;
            Nuklidclass nuklid = new Nuklidclass();
            if (dictionary.nucliddata.TryGetValue(currentNuklidKey, out nuklid))
            {
                hoverIndicator.transform.position = nuklid.Prefab.transform.position + nuklid.Prefab.transform.up * 0.075f;
                hoverIndicator.transform.parent = nuclidParent.transform;
            }
        }
    }

    public void onUnhover(KeyClassDict key)
    {
        if(!TranslationStarted) hoverIndicator.SetActive(false);
    }
}
