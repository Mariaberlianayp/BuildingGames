using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;
    [SerializeField] private Material[] materials;

    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    public float rotateAmount;

    public float gridSize;
    bool gridOn = true;
    public bool canPlace;

    [SerializeField]
    private Toggle gridToggle;

    public CurrencyCount currencyCount;

    void Update()
    {
        if(pendingObject != null)
        {
            if(gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z)
                 );
            }
            else
            {
                pendingObject.transform.position = pos;
            }

            if(Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            UpdateMaterials();
        }
    }
    public void PlaceObject()
    {
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        pendingObject = null;
        
    }
    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }


    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }

    }

    void UpdateMaterials()
    {
         if(canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }
         if(!canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void SelectObject(int index)
    {
        int objectCost = GetObjectCost(index);

        
        if (currencyCount.SubtractCurrency(objectCost))
        {
            
            pendingObject = Instantiate(objects[index], pos, transform.rotation);
        }
        else
        {
            Debug.Log("Currency tidak mencukupi!");
        }
    }

    int GetObjectCost(int index)
    {
        int objectCost = 0;

        switch (index)
        {
            case 0:
                objectCost = 500;
                break;
            case 1:
                objectCost = 100;
                break;
                
        }

        return objectCost;
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }

    }
    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos += xDiff;
        if(xDiff > (gridSize/2))
        {
            pos += gridSize;
        }

        return pos;
    }


}
