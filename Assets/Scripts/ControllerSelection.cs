using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSelection : MonoBehaviour
{
    public LineRenderer ray;
    public Decayrow decayrow;
    public NuclidDictionary dictionary;
    private bool nuclidSelected = false;
    public GameObject selectionPoint;
    public GameObject avatarpoint;
    public Material SelectionRayMaterial;
    public Material TranslationRayMaterial;
    public GameObject nuclidParent;
    private Transform currentParent;
    private bool TranslationStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        ray.enabled = false;
        selectionPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            ray.enabled = true;
            ray.material = SelectionRayMaterial;
        }


        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            ray.enabled = false;
            selectionPoint.SetActive(false);
            

            if (TranslationStarted)
            {
                TranslationStarted = false;
                nuclidParent.transform.parent = null;
                currentParent.parent = nuclidParent.transform;
                avatarpoint.transform.parent = null;
                nuclidSelected = false;
            }
            else if (nuclidSelected)
            {
                nuclidSelected = false;
                KeyClassDict nuclidKey = dictionary.RayIntersection(transform.position, transform.forward);
                if (nuclidKey != null)
                {
                    Nuklidclass nuclidElement;
                    if (dictionary.nucliddata.TryGetValue(nuclidKey, out nuclidElement))
                    {
                        decayrow.checkSelected(nuclidKey);
                        decayrow.interactionCount++;
                        avatarpoint.transform.position = nuclidElement.Prefab.GetComponent<NuclidPrefabElements>().TeleportTarget.position;
                    }
                }
            }
        }



        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (ray.enabled) { 
                int layer = 6;

                int layerMask = 1 << layer;

                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, layerMask))
                {
                    selectionPoint.SetActive(true);
                    ray.SetPositions(new Vector3[] { transform.position, hit.point });
                    nuclidSelected = true;
                    selectionPoint.transform.position = hit.point;

                    if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
                    {
                        ray.enabled = true;
                        ray.material = TranslationRayMaterial;
                        hit.transform.parent = null;
                        nuclidParent.transform.parent = hit.transform;
                        currentParent = hit.transform;
                        TranslationStarted = true;
                        avatarpoint.transform.parent = nuclidParent.transform;
                        decayrow.interactionCount++;
                    }
                    else if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
                    {
                        ray.material = SelectionRayMaterial;
                        TranslationStarted = false;
                        nuclidParent.transform.parent = null;
                        currentParent.parent = nuclidParent.transform;
                        avatarpoint.transform.parent = null;

                        ray.enabled = false;
                        selectionPoint.SetActive(false);
                        nuclidSelected = false;
                    }
                    if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && TranslationStarted)
                    {
                        currentParent.position = new Vector3(currentParent.position.x, hit.point.y, hit.point.z);
                    }
                }
                else
                {
                    ray.SetPositions(new Vector3[] { transform.position, transform.position + transform.forward });
                    nuclidSelected = false;
                    selectionPoint.SetActive(false);
                    /*if (TranslationStarted)
                    {
                        TranslationStarted = false;
                        nuclidParent.transform.parent = null;
                        currentParent.parent = nuclidParent.transform;
                        avatarpoint.transform.parent = null;
                    }*/
                }
            }
        }
        
    }
}
