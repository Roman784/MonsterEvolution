using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bestiary : PanelMenu
{
    [SerializeField] private List<MonsterInfo> _monstersInfo = new List<MonsterInfo>();

    [Space]

    [SerializeField] private TMP_Text _nameRenderer;
    [SerializeField] private TMP_Text _descriptionRenderer;
    [SerializeField] private TMP_Text _CPSRenderer;
    [SerializeField] private Image _spriteRenderer;

    [Space]

    [SerializeField] private Button _previousMonster;
    [SerializeField] private Button _nextMonster;

    private int _currentMonsterIndex;
    private int _maxIndex;

    [Space]

    [SerializeField] private GameObject _noDataPanel;

    private void Awake()
    {
    }

    private new void Open()
    {
        base.Open();

        _previousMonster.onClick.AddListener(ShowPreviousMonster);
        _nextMonster.onClick.AddListener(ShowNextMonster);

        _currentMonsterIndex = 0;
        _previousMonster.gameObject.SetActive(false);

        _maxIndex = DataContext.Instance.GameData.MaxMonsterLevel - 1;
        _noDataPanel.SetActive(_maxIndex < 0);

        ShowMonster();
    }

    private void ShowPreviousMonster()
    {
        if (_currentMonsterIndex <= 0) return;

        _currentMonsterIndex -= 1;
        ShowMonster();
    }

    private void ShowNextMonster() 
    {
        if (_currentMonsterIndex >= _maxIndex) return;

        _currentMonsterIndex += 1;
        ShowMonster();
    }

    private void ShowMonster()
    {
        _previousMonster.gameObject.SetActive(_currentMonsterIndex > 0);
        _nextMonster.gameObject.SetActive(_currentMonsterIndex < _maxIndex && _currentMonsterIndex < _monstersInfo.Count);

        if (_maxIndex < 0 || _monstersInfo.Count == 0 || _monstersInfo.Count - 1 < _maxIndex) return;

        UpdateRenderers();
    }

    private void UpdateRenderers()
    {
        _nameRenderer.text = _monstersInfo[_currentMonsterIndex].Name.Translate();
        _descriptionRenderer.text = _monstersInfo[_currentMonsterIndex].Description.Translate();
        _CPSRenderer.text = CoinsRenderer.GetFormattedValue(_monstersInfo[_currentMonsterIndex].CPS).ToString();
        _spriteRenderer.sprite = _monstersInfo[_currentMonsterIndex].Sprite;
    }
}
