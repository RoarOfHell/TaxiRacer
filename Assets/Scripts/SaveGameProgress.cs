using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameProgress : MonoBehaviour
{
    [SerializeField] private CarController _car;
    [SerializeField] private DayNight _dayNight;
    private void Start()
    {
        _dayNight.LoadSavedTime(PlayerPrefs.GetFloat("SavedTime"));
        _car.LoadSavedMoney(PlayerPrefs.GetInt("SavedMoney"));
        _car.transform.position = new Vector3(PlayerPrefs.GetFloat("CarPositionX"), PlayerPrefs.GetFloat("CarPositionY"), PlayerPrefs.GetFloat("CarPositionZ"));
        _car.transform.rotation = new Quaternion(PlayerPrefs.GetFloat("CarRotationX"), PlayerPrefs.GetFloat("CarRotationY"), PlayerPrefs.GetFloat("CarRotationZ"), PlayerPrefs.GetFloat("CarRotationW"));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("SavedTime", _dayNight.GetSavedTime());
        PlayerPrefs.SetInt("SavedMoney", _car.GetMoney());
        PlayerPrefs.SetFloat("CarPositionX", _car.transform.position.x);
        PlayerPrefs.SetFloat("CarPositionY", _car.transform.position.y);
        PlayerPrefs.SetFloat("CarPositionZ", _car.transform.position.z);

        PlayerPrefs.SetFloat("CarRotationX", _car.transform.rotation.x);
        PlayerPrefs.SetFloat("CarRotationY", _car.transform.rotation.y);
        PlayerPrefs.SetFloat("CarRotationZ", _car.transform.rotation.z);
        PlayerPrefs.SetFloat("CarRotationW", _car.transform.rotation.w);
    }
}
