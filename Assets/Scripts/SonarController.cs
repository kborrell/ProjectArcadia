using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SonarController : MonoBehaviour
{
    [SerializeField]
    private Character _target;

    [SerializeField]
    private RectTransform _rect;

    private Image _image;
    private bool animationEnded;
    private float panelWidth, panelHeight, w, h;

    void Start()
    {
        _image = GetComponent<Image>();
        _target = CharactersManager.Instance.getTargetCharacter();
    }

    void Update()
    {
    }

    public IEnumerator PlaySonarAnimation()
    {
        if (_target == null)
        {
            _image = GetComponent<Image>();
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
            _target = CharactersManager.Instance.getTargetCharacter();
        }
        Coroutine routine = StartCoroutine(DoImageFade());

        while (!animationEnded)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(_target.transform.position);

            panelWidth = _rect.rect.width * viewPos.x;
            panelHeight = _rect.rect.height * viewPos.y;

            w = Mathf.Clamp(panelWidth, 0, _rect.rect.width);
            h = Mathf.Clamp(panelHeight, 0, _rect.rect.height);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(w, h);

            yield return null;
        }
    }

    public IEnumerator DoImageFade()
    {
        animationEnded = false;
        yield return _image.DOColor(new Color(_image.color.r, _image.color.g, _image.color.b, 1f), 1f).WaitForCompletion();
        yield return _image.DOColor(new Color(_image.color.r, _image.color.g, _image.color.b, 0f), 1f).WaitForCompletion();
        animationEnded = true;
    }
}
