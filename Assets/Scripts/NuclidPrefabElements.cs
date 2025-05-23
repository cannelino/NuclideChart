using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NuclidPrefabElements : MonoBehaviour
{
    public GameObject alphaplane;
    public GameObject stabilplane;
    public GameObject betaminusplane;
    public GameObject betaplusplane;
    public TextMeshProUGUI nuclidname;
    public TextMeshProUGUI ordinalnumber;
    public TextMeshProUGUI halflifetime;
    public Material outlineMaterial;
    public MeshRenderer outlineRenderer;
    private Material instanceMaterial;
    public Transform TeleportTarget;
    public bool selected = false;


    private void Awake()
    {
        instanceMaterial = Instantiate(outlineMaterial);//Resources.Load<Material>(@"Materials/OutlineMask"));

        instanceMaterial.name = "Outline (Instance)";

        instanceMaterial.SetColor("_Color", Color.white);
        //instanceMaterial.SetFloat("_Alpha", 0);
        //instanceMaterial.SetFloat("_AlphaOutline", 1);
        //instanceMaterial.SetFloat("_Outline", 0.0969f);
        outlineRenderer.material = instanceMaterial;
        
    }

    public void SetOutlineColor(Color color)
    {
        instanceMaterial.SetColor("_Color", color);


    }

    public void SetRaise(bool raised)
    {
        if (raised) this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.1f, this.transform.localPosition.z);
        else this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z);
    }

}

//_Outline("Outline width", Range(0.0, 0.15)) = .005

//_Alpha("Alpha", Float) = 1
	//	_AlphaOutline("Alpha Outline", Float) = 0.8
