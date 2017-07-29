using UnityEngine;
using System;

public class PanelBase : MonoBehaviour
{
    Transform active, inactive;

	protected Action OnShowEnd;
	protected Action OnHideEnd;

    bool stateActive;

    Transform cachedTransform;
    public Transform Transform { get { if (cachedTransform == null) cachedTransform = GetComponent<Transform>(); return cachedTransform; } }

    RectTransform cachedRectTransform;
    public RectTransform Rect { get { if (cachedRectTransform == null) cachedRectTransform = GetComponent<RectTransform>(); return cachedRectTransform; } }

    public bool IsActive { get { return stateActive; } }

    public virtual void Initialize(Transform active, Transform inactive)
    {
        this.active = active;
        this.inactive = inactive;
        stateActive = false;
    }

    public virtual void Init()
    {
        Show();
    }

    public virtual void Finish()
    {
        Hide();
    }

    protected virtual void Show()
    {
        stateActive = true;
        Transform.SetParent(active, false);
		if (OnShowEnd != null)
			OnShowEnd ();
    }

    protected virtual void Hide()
    {
        stateActive = false;
        Transform.SetParent(inactive, false);
		if (OnHideEnd != null)
			OnHideEnd ();
    }
}
