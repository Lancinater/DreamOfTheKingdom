using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Transform healthBarTransform;
    private UIDocument _healthBarUIDocument;
    private ProgressBar _healthBar;
    private CharacterBase _currentCharacter;
    private void Awake()
    {
        _currentCharacter = GetComponent<CharacterBase>();
        InitializeHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        var rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size, Camera.main);
        element.transform.position = rect.position;
    }

    [ContextMenu("Update Health Bar")]
    public void InitializeHealthBar()
    {
        _healthBarUIDocument = GetComponent<UIDocument>();
        _healthBar = _healthBarUIDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        MoveToWorldPosition(_healthBar, healthBarTransform.position, Vector2.zero);
        _healthBar.highValue = _currentCharacter.MaxHp;
        _healthBar.lowValue = 0;
    }
    
    private void UpdateHealthBar()
    {
        if (_currentCharacter.isDead)
        {
            _healthBar.style.display = DisplayStyle.None;
        }
        if (_healthBar != null)
        {
            _healthBar.value = _currentCharacter.CurrentHp;
            _healthBar.title = _currentCharacter.CurrentHp + "/" + _currentCharacter.MaxHp;
            
            _healthBar.RemoveFromClassList("highHealth");
            _healthBar.RemoveFromClassList("lowHealth");
            _healthBar.RemoveFromClassList("mediumHealth");
            
            var percentage = (float) _currentCharacter.CurrentHp / _currentCharacter.MaxHp;
            if (percentage > 0.6f)
            {
                _healthBar.AddToClassList("highHealth");
            }
            else if (percentage > 0.3f)
            {
                _healthBar.AddToClassList("mediumHealth");
            }
            else
            {
                _healthBar.AddToClassList("lowHealth");
            }
        }
    }
}
