using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public string address;

    [SerializeField] private GameObject _pointRender;
    public GameObject characterRender;
    [SerializeField] private GameObject _pointZoneRender;

    public Color Color { get { return _pointRender.GetComponent<SpriteRenderer>().color; } set { _pointRender.GetComponent<SpriteRenderer>().color = value; } }
    public void Show()
    {
        _pointRender.SetActive(true);
        characterRender.SetActive(true);
        _pointZoneRender.SetActive(true);
    }

    public void Hide()
    {
        _pointRender.SetActive(false);
        characterRender.SetActive(false);
        _pointZoneRender.SetActive(false);
    }

    public void HideClient()
    {
        characterRender.SetActive(false);
    }

    public void ShowClient()
    {
        characterRender.SetActive(true);
    }

    public void ShowZone()
    {
        _pointZoneRender.SetActive(true);
    }

    public void HideZone()
    {
        _pointZoneRender.SetActive(false);
    }
}
