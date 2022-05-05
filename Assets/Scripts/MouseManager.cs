using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    #region Attributes
    //what objects are clickable
    [SerializeField] private LayerMask clickableLayerMask;
    //Swap cursors for object
    [SerializeField] private Texture2D pointer;
    [SerializeField] private Texture2D target;//Pointer for clickable objects
    [SerializeField] private Texture2D doorways;//Pointer for doorways objects
       

    [SerializeField] private Texture2D combat;//Pointer for combat objects

    public LayerMask ClickableLayerMask { get { return clickableLayerMask; } }
    public Texture2D Pointer { get { return pointer; } }
    public Texture2D Target { get { return target; } }
    public Texture2D Doorways { get { return doorways; } }
    public Texture2D Combat { get { return combat; } }
    #endregion

    private static MouseManager instance;

    //Singleton implementation for only one instance
    public static MouseManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MouseManager>();
            return instance;
        }
    }

    private void Update()
    {
        UpdateCursor();
    }

    private void UpdateCursor()
    {
        RaycastHit hit = GetHitOnClickableLayer();
        Texture2D textureCursor;

        //If any is clickable
        if (hit.collider != null)
        {
            bool door = false;
            bool item = false;

            switch (hit.collider.gameObject.tag)
            {
                case Constants.Tags.DoorwayTag:
                    textureCursor=doorways;
                    door = true;
                    break;
                
                case Constants.Tags.Item:
                    item = true;
                    textureCursor = target;
                    break;

                default:
                    textureCursor = target;
                    break;
            }

            Cursor.SetCursor(textureCursor, new Vector2(16, 16), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);           
        }
    }

    public Vector3 GetMousePosition()
    {
        RaycastHit hit = GetHitOnClickableLayer();

        //If any is clickable
        if (hit.collider != null)
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public RaycastHit GetHitOnClickableLayer()
    {
       RaycastHit hit;

       //If any is clickable the result out in hit
       Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayerMask.value);

       return hit;

    }
}
