using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logica;
using Unity.VisualScripting;
using UnityEngine;

public class Cementerio : MonoBehaviour
{
    GameObject cemt;
    List<GameObject> cementerio;
    public List<GameObject> posicionestab;

    // Start is called before the first frame update
    public void Start()
    {
        cemt = this.gameObject;
        cementerio = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Posiciones(string tag)
    {
        posicionestab = GameObject.FindGameObjectsWithTag(tag).ToList();
    }
    public void CartasCementerio()
    {
       for(int i = 0; i < posicionestab.Count; i++)
       {
         DropPosition pos = posicionestab[i].GetComponent<DropPosition>();
         if(pos._item != null)
         {
            cementerio.Add(pos._item);
            pos._item.transform.SetParent(cemt.transform);
            pos._item.transform.position = cemt.transform.position;
            pos._item = null;
         }
       }
    }
}
