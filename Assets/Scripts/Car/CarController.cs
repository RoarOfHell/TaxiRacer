using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    float driftFactor = 0.5f;
    Vector2 movement;
    Rigidbody2D rb;
    public float speed = 5;
    public float turnSpeed = 50;
    public float maxSpeed =4;
    public bool isClientMove = false;
    private bool isStop = false;
    private int money = 0;


    [SerializeField] private List<GameObject> _stopLamp;
    [SerializeField] private List<GameObject> _Lamps;

    [SerializeField] private TextMeshProUGUI _textSpeedometr;
    [SerializeField] private TextMeshProUGUI _moneyText;


    public GameObject startPointSelected;
    public GameObject endPointSelected;
    public GameObject currentPointSelected;
    public bool isArrival = false;

    private DayNight dayNight;
    private FixedJoystick fixedJoystick;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.Find("TimeDayChange"))
        {
            dayNight = GameObject.Find("TimeDayChange").GetComponent<DayNight>();
        }
        if (GameObject.Find("FixedJoystick"))
        {
            fixedJoystick = GameObject.Find("FixedJoystick").GetComponent<FixedJoystick>();
        }
    }

    private void Update()
    {
        OnMove();
        StopLamp();
        StopCar();
        _moneyText.text = $"Баланс: {money}$";
        OnOffLamp();
        if (isClientMove)
        {
            startPointSelected.GetComponent<PointController>().ShowClient();
            startPointSelected.GetComponent<PointController>().characterRender.transform.position = ClientMoveToCar(startPointSelected.GetComponent<PointController>().characterRender);
            if (DistanceClientToCar(startPointSelected.GetComponent<PointController>().characterRender) <= 1)
            {
                startPointSelected.GetComponent<PointController>().HideClient();
                startPointSelected.GetComponent<PointController>().characterRender.transform.position = startPointSelected.transform.position;
                isClientMove = false;
            }
        }

    }

    private void FixedUpdate()
    {
        // Get Forward Velocity
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);

        // Get Right Velocity (drift)
        Vector2 rightDrift = transform.right * Vector2.Dot(rb.velocity, transform.right);
        _textSpeedometr.text = (GetMinus(rb.velocity.magnitude * 10)*-1).ToString("0") + " км/ч";
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else{
            rb.velocity = forwardVelocity + rightDrift * driftFactor;
        }

        rb.AddForce(transform.up * speed * movement.y * Time.deltaTime, ForceMode2D.Force);
        rb.MoveRotation(rb.rotation + turnSpeed * -movement.x * Time.deltaTime);
    }

    void OnMove()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            movement.y = Input.GetAxis("Vertical");
            movement.x = Input.GetAxis("Horizontal");
            if (fixedJoystick)
            {
                fixedJoystick.gameObject.SetActive(false);
            }
        }
        if (fixedJoystick)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                fixedJoystick.gameObject.SetActive(true);
                movement.y = fixedJoystick.Vertical;
                movement.x = fixedJoystick.Horizontal;
            }

        }
        
    }

    void StopLamp()
    {
        if (movement.y < 0 || isStop)
        {
            for (int i = 0; i < _stopLamp.Count; i++)
            {
                _stopLamp[i].SetActive(true);
                rb.drag = 3;
            }
        }
        else
        {
            for (int i = 0; i < _stopLamp.Count; i++)
            {
                _stopLamp[i].SetActive(false);
                rb.drag = 0;
            }
        }
    }

    void StopCar()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.drag = 3;
            isStop = true;
        }
        else{
            rb.drag = 0.1f;
            isStop = false;
        }
    }

    float GetMinus(float value)
    {
        if (value >=0)
        {
            value *= -1;
        }
        return value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentPointSelected == collision.gameObject)
        {
            isArrival = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentPointSelected == collision.gameObject)
        {
            isArrival = false;
        }
    }

    public void RandomSpawnCharacter()
    {
        var points = GameObject.FindGameObjectsWithTag("CharacterPoint").ToList();
        startPointSelected = points[Random.Range(0, points.Count-1)];
        startPointSelected.GetComponent<PointController>().Show();
        startPointSelected.GetComponent<PointController>().Color = Color.green;
        points.Remove(startPointSelected);
        endPointSelected = points[Random.Range(0, points.Count - 1)];
        endPointSelected.GetComponent<PointController>().Show();
        endPointSelected.GetComponent<PointController>().Color = Color.blue;
    }

    public void HideAllPoints()
    {
        var points = GameObject.FindGameObjectsWithTag("CharacterPoint").ToList();
        foreach (var point in points) 
            point.GetComponent<PointController>().Hide();
    }

    public void AddMoney(int count)
    {
        money += count;
        Debug.Log($"Money: {money}");
    }

    private void OnOffLamp()
    {
        if (dayNight)
        {
            if (!dayNight.IsDay())
            {
                foreach (var lamp in _Lamps)
                {
                    lamp.SetActive(false);
                }
            }
            else
            {
                foreach (var lamp in _Lamps)
                {
                    lamp.SetActive(true);
                }
            }
        }
    }

    private Vector3 ClientMoveToCar(GameObject client)
    {
        return Vector3.Lerp(client.transform.position, transform.position, 0.01f);
    }
    private float DistanceClientToCar(GameObject client)
    {
        return Vector3.Distance(client.transform.position, transform.position);
    }
}
