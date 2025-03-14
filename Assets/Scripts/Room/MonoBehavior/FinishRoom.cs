using System;
using UnityEngine;

public class FinishRoom : MonoBehaviour
{
    public ObjectEventSO loadMapEvent;
    private void OnMouseUp()
    {
        // Back to the map
        loadMapEvent.RaiseEvent(null, this);
    }
}
