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
    private bool isStop = false;
    private int money = 0;


    [SerializeField] private List<GameObject> _stopLamp;

    [SerializeField] private Image _arrowSpeedometr;
    [SerializeField] private TextMeshProUGUI _moneyText;


    public GameObject startPointSelected;
    public GameObject endPointSelected;
    public GameObject currentPointSelected;
    public bool isArrival = false;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        OnMove();
        StopLamp();
        StopCar();
        _moneyText.text = $"Баланс: {money}$";
    }

    private void FixedUpdate()
    {
        // Get Forward Velocity
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);

        // Get Right Velocity (drift)
        Vector2 rightDrift = transform.right * Vector2.Dot(rb.velocity, transform.right);
        _arrowSpeedometr.transform.localRotation= new Quaternion()
        {
            z = GetMinus(rb.velocity.magnitude/10),
            x = _arrowSpeedometr.transform.rotation.x,
            y = _arrowSpeedometr.transform.rotation.y,
            w = 1
        };
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
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");
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
}
