using UnityEngine;
using UnityEngine.UI;

public class SonarController : MonoBehaviour
{
    [SerializeField]
    private Character _target;
    
    [SerializeField]
    private RectTransform _rect;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _target = CharactersManager.Instance.getTargetCharacter();
    }
    
    void Update()
    {
        if(_target != null)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(_target.transform.position);
            
            float panelWidth = _rect.rect.width * viewPos.x;
            float panelHeight = _rect.rect.height * viewPos.y;
            
            float w = Mathf.Clamp(panelWidth, 0, _rect.rect.width);
            float h = Mathf.Clamp(panelHeight, 0, _rect.rect.height);
            
            GetComponent<RectTransform>().anchoredPosition = new Vector2(w, h);
            
            Vector3 targetDirection = new Vector3((panelWidth / 2), (panelHeight / 2), 0) - new Vector3(w, h, 0);

            Color color = _image.color;
            _image.color = new Color(color.r, color.g, color.b, Mathf.PingPong(Time.time * 1f, 1.0f));
        }
    }
}
