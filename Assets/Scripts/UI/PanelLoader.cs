using UnityEngine;
using System.Collections.Generic;
using System;

public class PanelLoader : MonoBehaviour
{
    [SerializeField]
    GameObject[] _panels;

    [SerializeField]
    Transform _active = null, _inactive = null;

    public int ActivePanels { get { return _active.childCount; } }

    public Dictionary<Type, PanelBase> LoadPanels()
    {
        Dictionary<Type, PanelBase> panels = new Dictionary<Type, PanelBase>(_panels.Length);
        for (int i = 0; i < _panels.Length; i++)
        {
            Transform panel = Instantiate(_panels[i]).transform as Transform;
            panel.SetParent(_inactive, false);
            PanelBase panelBase = panel.GetComponent<PanelBase>();

            panelBase.Initialize(_active, _inactive);
            panels.Add(panelBase.GetType(), panelBase);
        }

        return panels;
    }
}