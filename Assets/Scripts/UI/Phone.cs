using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [Header("Sheets")]
    [SerializeField] private Image _searchOrder;
    [SerializeField] private Image _order;
    [SerializeField] private Image _arrival;
    [SerializeField] private Image _phone;
    [SerializeField] private Image _taxiApp;
    [SerializeField] private Image _bankApp;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _clientNameText;
    [SerializeField] private TextMeshProUGUI _startAddressText;
    [SerializeField] private TextMeshProUGUI _endAddressText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _topBarTime;
    [SerializeField] private TextMeshProUGUI _mainTime;
    [SerializeField] private TextMeshProUGUI _minimapTime;

    private DayNight dayNightTime;
    private int moneyOrder;

    private void Awake()
    {
        if (GameObject.Find("TimeDayChange"))
        {
            dayNightTime = GameObject.Find("TimeDayChange").GetComponent<DayNight>();
        }
    }

    private void Update()
    {
        if (dayNightTime)
        {
            _topBarTime.text = dayNightTime.GetTimeAtString();
            _mainTime.text = dayNightTime.GetTimeAtString();
            _minimapTime.text = dayNightTime.GetTimeAtString();
        }
        
    }

    public void SearchOrder()
    {
        var car = GameObject.Find("Car").GetComponent<CarController>();
        car.RandomSpawnCharacter();
        _searchOrder.gameObject.SetActive(false);
        _order.gameObject.SetActive(true);
        car.endPointSelected.GetComponent<PointController>().HideClient();
        _clientNameText.text = "Клиент: Иванов Иван Иванович";
        _startAddressText.text = $"Откуда: {car.startPointSelected.GetComponent<PointController>().address}";
        _endAddressText.text = $"Куда: {car.endPointSelected.GetComponent<PointController>().address}";
        float distance = Mathf.Round(Vector2.Distance(car.startPointSelected.transform.position, car.endPointSelected.transform.position));
        _distanceText.text = $"Растояние: {distance}км";
        moneyOrder = (int)(100 * distance);
        _moneyText.text = $"Оплата: {100* distance}$";

    }

    public void Cancel()
    {
        var car = GameObject.Find("Car").GetComponent<CarController>();
        car.HideAllPoints();
        _searchOrder.gameObject.SetActive(true);
        _order.gameObject.SetActive(false);
    }

    public void Accept()
    {
        StartCoroutine(PlayAnimationAccept());   
        
    }

    public void Arrival()
    {
        var car = GameObject.Find("Car").GetComponent<CarController>();
        if (car.isArrival)
        {
            if (car.currentPointSelected == car.startPointSelected)
            {
                car.currentPointSelected.GetComponent<PointController>().HideClient();
                car.currentPointSelected.GetComponent<PointController>().HideZone();
                car.currentPointSelected.GetComponent<PointController>().Hide();
                car.isClientMove = true;
                car.currentPointSelected = car.endPointSelected;
                car.currentPointSelected.GetComponent<PointController>().Show();
                Debug.Log("Start");
            }
            else if (car.currentPointSelected == car.endPointSelected)
            {
                Debug.Log("End");
                car.currentPointSelected.GetComponent<PointController>().HideClient();
                car.currentPointSelected.GetComponent<PointController>().HideZone();
                car.currentPointSelected.GetComponent<PointController>().Hide();
                _phone.gameObject.SetActive(true);
                _phone.GetComponent<Animator>().Play("ShowPhone");
                _arrival.gameObject.SetActive(false);
                _searchOrder.gameObject.SetActive(true);
                
                car.AddMoney(moneyOrder);
            }
        }
        else
        {
            Debug.Log("Вы не прибыли к клиенту заказ отменяется и вы получаете жалобу");
            _searchOrder.gameObject.SetActive(true);
            _order.gameObject.SetActive(false);
            car.currentPointSelected = null;
            car.isArrival = false;
            _phone.gameObject.SetActive(true);
            _phone.GetComponent<Animator>().Play("ShowPhone");
            car.HideAllPoints();
        }
    }

    public void Home()
    {
        _taxiApp.gameObject.SetActive(false);
    }

    public void TaxiApp()
    {
        _taxiApp.gameObject.SetActive(true);
    }

    public void BankApp()
    {

    }

    IEnumerator PlayAnimationAccept()
    {
        GameObject.Find("Phone").GetComponent<Animator>().Play("PhoneHide");
        _order.gameObject.SetActive(false);
        var car = GameObject.Find("Car").GetComponent<CarController>();
        car.currentPointSelected = car.startPointSelected;
        car.endPointSelected.GetComponent<PointController>().Hide();
        yield return new WaitForSeconds(0.15f);
        _arrival.gameObject.SetActive(true);
        _phone.gameObject.SetActive(false);
        car.currentPointSelected.GetComponent<PointController>().ShowClient();
        car.currentPointSelected.GetComponent<PointController>().ShowZone();
    }

    
}
