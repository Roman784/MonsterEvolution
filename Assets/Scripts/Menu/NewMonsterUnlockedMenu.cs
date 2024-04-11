using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewMonsterUnlockedMenu : PanelMenu
{
    public static NewMonsterUnlockedMenu Instance { get; private set; }

    [Space]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _image;

    [Space]
    [SerializeField] private Button _closeButton;

    [Space]
    [SerializeField] private Transform _circularLight;
    [SerializeField] private float _rotationSpeed;

    private void Awake()
    {
        Instance = Singleton.Get<NewMonsterUnlockedMenu>();

        _closeButton.onClick.AddListener(Close);
    }

    private void Update()
    {
        RotateLight();
    }

    private void RotateLight()
    {
        _circularLight.Rotate(-Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    public void Open(MonsterInfo monsterInfo)
    {
        base.Open();
        UpdateRenderers(monsterInfo);

        StartCoroutine(SetCloseButton());
    }

    private void UpdateRenderers(MonsterInfo monsterInfo)
    {
        _name.text = monsterInfo.Name.Translate();
        _image.sprite = monsterInfo.Sprite;
        _image.SetNativeSize();
    }

    private IEnumerator SetCloseButton()
    {
        _closeButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        _closeButton.gameObject.SetActive(true);
    }
}
