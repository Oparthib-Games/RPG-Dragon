using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{   
    public Texture2D WalkCursor;
    public Texture2D AttackCursor;
    public Texture2D ConfuseCursor;

    public Vector2 cursorHotspot = new Vector2(98, 98);

    [SerializeField] const int walkLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;


    CameraRaycaster cameraRaycaster;

    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();

        cameraRaycaster.layer_change_observer += OnLayerChange;
    }

    void OnLayerChange(int new_layer_number)
    {
        switch(new_layer_number)
        {
            case walkLayerNumber:
                Cursor.SetCursor(WalkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(AttackCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(ConfuseCursor, cursorHotspot, CursorMode.Auto);
                break;
        }
    }
}
