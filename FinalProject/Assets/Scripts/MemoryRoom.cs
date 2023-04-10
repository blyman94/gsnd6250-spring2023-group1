using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MemoryRoom : MonoBehaviour
{
    [SerializeField] private float _roomFadeDuration = 5.0f;
    private List<ObjectFade> _allObjectFades;
    private List<List<ObjectFade>> _objectFadesByPriority;

    private int _minPriority;
    private int _maxPriority;

    private void Start()
    {
        _allObjectFades = GetComponentsInChildren<ObjectFade>().ToList();
        _objectFadesByPriority = new List<List<ObjectFade>>();

        _maxPriority = _allObjectFades.Max(objectFade =>
            objectFade.FadePriority);
        _minPriority = _allObjectFades.Min(objectFade =>
            objectFade.FadePriority);

        for (int i = 0; i <= _maxPriority; i++)
        {
            List<ObjectFade> priorityList =
                _allObjectFades.Where(objectFade =>
                objectFade.FadePriority == i).ToList();
            _objectFadesByPriority.Add(priorityList);
        }
    }

    public void FadeInAll()
    {
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.In(_roomFadeDuration);
        }
    }

    public void FadeOutAll()
    {
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.Out(_roomFadeDuration);
        }
    }

    public void FadeRandomInPriority(int priority)
    {
        int priorityFinal = Mathf.Clamp(priority, _minPriority, _maxPriority);
        
    }

    public void HideAll()
    {
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.Hide();
        }
    }

    public void ShowAll()
    {
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.Show();
        }
    }
}
