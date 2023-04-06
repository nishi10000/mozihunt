using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public string SortingLayerName = "gift";
        public int SortingOrder = 2;
 
        void Awake ()
        {
                 gameObject.GetComponent<MeshRenderer> ().sortingLayerName = SortingLayerName;
                 gameObject.GetComponent<MeshRenderer> ().sortingOrder = SortingOrder;
        }
}
