using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform baseTransform; // Ссылка на трансформ базы робота
    public float moveSpeed = 5f; // Скорость перемещения робота
    public float orePickupDistance = 2f; // Расстояние для сбора руды

    private bool isMoving = false; // Флаг для проверки, двигается ли робот
    private GameObject targetOre; // Ссылка на объект руды, которую робот собирает

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    // Вызывается другой сущностью для начала процесса сбора руды
    public void BringOre(GameObject ore)
    {
        targetOre = ore;
        isMoving = true;
    }

    // Метод для движения робота к цели
    private void MoveToTarget()
    {
        if (targetOre == null)
        {
            isMoving = false;
            return;
        }

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetOre.transform.position, step);

        // Если робот достиг объекта руды, собираем его
        if (Vector3.Distance(transform.position, targetOre.transform.position) < orePickupDistance)
        {
            CollectOre();
        }
    }

    // Метод для сбора руды и доставки на базу
    private void CollectOre()
    {
        // Здесь может быть логика для сбора руды и доставки на базу
        // Например, уничтожение объекта руды и перемещение робота обратно на базу
        Destroy(targetOre);
        isMoving = false;
        targetOre = null;
        ReturnToBase();
    }

    // Метод для возвращения на базу
    private void ReturnToBase()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, baseTransform.position, step);

        // Если робот вернулся на базу, можно добавить дополнительные действия
        if (Vector3.Distance(transform.position, baseTransform.position) < 0.1f)
        {
            // Дополнительные действия после возвращения на базу
        }
    }
}


