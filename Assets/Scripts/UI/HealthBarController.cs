using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Transform healthBarTransform;
    private UIDocument _healthBarUIDocument;
    private ProgressBar _healthBar;
    
    private void Awake()
    {
        _healthBarUIDocument = GetComponent<UIDocument>();
        _healthBar = _healthBarUIDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(_healthBar, healthBarTransform.position, Vector2.zero);
    }

    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        var rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size, Camera.main);
        element.transform.position = rect.position;
    }
}
