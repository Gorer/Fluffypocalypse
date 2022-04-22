using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Переменная скорости передвижения
    [SerializeField] float moveSpeed = 5f;
    // Вектор направления движения
    Vector2 rawInput;

    // Отступ слева
    [SerializeField] float paddingLeft;
    // Отступ справа
    [SerializeField] float paddingRight;
    // Отступ сверху
    [SerializeField] float paddingTop;
    // Отступ снизу
    [SerializeField] float paddingBottom;

    // Координаты левой нижнец границы экрана
    Vector2 minBounds;
    // Координаты правой верхней границы экрана
    Vector2 maxBounds;

    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    // Инициализирует границы для передвижения персонажа на экране
    void InitBounds()
    {
        // Сохраняет в переменную mainCamera камеру в иерархии с тегом MainCamera
        Camera mainCamera = Camera.main;
        // Преобразует координаты левого нижнего угла камеры в мировые координаты
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        // Преобразует координаты правого верхнего угла камеры в мировые координаты
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Двигает персонажа
    private void Move()
    {
        // Высчитывает вектор движения по направлению движения, скорости движения и количеству времени прошедшего с предыдущего кадра
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        // Чтобы просчитать не зашёл ли персонаж за границы экрана используется переменная newPos в которую будет перемещаться персонаж
        Vector2 newPos = new Vector2();
        // перед перемещением персонажа в newPos, newPos проверяется на находимость в пределах экрана
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    //При нажатии на клавишу (InputValue input)
    private void OnMove(InputValue value)
    {
        //Получаем координаты направления движения (rawInput)
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);
    }
}
