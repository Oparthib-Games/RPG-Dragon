using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

public class CameraRaycaster : MonoBehaviour
{
	// INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
	[SerializeField] int[] layerPriorities;

    float maxRaycastDepth = 100f; // Hard coded value
	int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

	
    public delegate void CursorLayerChange_Delegate(int newLayer);
    public event CursorLayerChange_Delegate layer_change_observer;

	public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);
	public event OnClickPriorityLayer notifyMouseClickObservers;


    void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject ())     // if mouse over UI
		{
			NotifyObserersIfLayerChanged (5);
			return; //------------------------------------------ Stop looking for other objects
		}

		// Raycast to max depth, every frame as things can move under mouse
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hitInfo = Physics.RaycastAll (ray, maxRaycastDepth);

        RaycastHit? priorityHit = FindTopPriorityHit(hitInfo);
        if (!priorityHit.HasValue) // if hit no priority object
		{
			NotifyObserersIfLayerChanged (0); // broadcast default layer
			return;
		}

		// Notify delegates of layer change
		var layerHit = priorityHit.Value.collider.gameObject.layer;
		NotifyObserersIfLayerChanged(layerHit);
		
		// Notify delegates of highest priority game object under mouse when clicked
		if (Input.GetMouseButton (0))
		{
			notifyMouseClickObservers (priorityHit.Value, layerHit);
		}
	}

	void NotifyObserersIfLayerChanged(int newLayer)
	{
		if (newLayer != topPriorityLayerLastFrame)
		{
			topPriorityLayerLastFrame = newLayer;
			layer_change_observer (newLayer);
		}
	}

	RaycastHit? FindTopPriorityHit (RaycastHit[] hitInfo)
	{
		List<int> layersOfHitColliders = new List<int> ();      // Form list of layer numbers hit

        foreach (RaycastHit the_hitInfo in hitInfo)
		{
			layersOfHitColliders.Add (the_hitInfo.collider.gameObject.layer);   // Add all layer from RayCastAll into the list;
		}

		// --------- Step through layers in order of priority looking for a gameobject with that layer
		foreach (int theLayer in layerPriorities)
        {
			foreach (RaycastHit the_hitInfo in hitInfo)
			{
				if (the_hitInfo.collider.gameObject.layer == theLayer)
				{
					return the_hitInfo; // stop looking
				}
			}
		}
		return null; // because cannot use GameObject? nullable
	}
}