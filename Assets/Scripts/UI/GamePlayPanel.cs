using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPanel : MonoBehaviour
{
    private VisualElement _rootUIDocument;
    private Label _energyLabel;
    private Label _drawCardLabel;
    private Label _discardCardLabel;
    private Label _roundInformationLabel;
    private Button _endTurnButton;

    private void OnEnable()
    {
        _rootUIDocument = GetComponent<UIDocument>().rootVisualElement;
        _energyLabel = _rootUIDocument.Q<Label>("EnergyAmount");
        _drawCardLabel = _rootUIDocument.Q<Label>("DrawAmount");
        _discardCardLabel = _rootUIDocument.Q<Label>("DiscardAmount");
        _roundInformationLabel = _rootUIDocument.Q<Label>("RoundInformation");
        _endTurnButton = _rootUIDocument.Q<Button>("EndTurn");

        _drawCardLabel.text = "0";
        _discardCardLabel.text = "0";
        _energyLabel.text = "0";
        _roundInformationLabel.text = "Game Start";
    }
    
    public void UpdateDrawDeckAmount(int amount)
    {
        _drawCardLabel.text = amount.ToString();
    }
    
    public void UpdateDiscardDeckAmount(int amount)
    {
        _discardCardLabel.text = amount.ToString();
    }
}
