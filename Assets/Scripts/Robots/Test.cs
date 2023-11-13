using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform baseTransform; // ������ �� ��������� ���� ������
    public float moveSpeed = 5f; // �������� ����������� ������
    public float orePickupDistance = 2f; // ���������� ��� ����� ����

    private bool isMoving = false; // ���� ��� ��������, ��������� �� �����
    private GameObject targetOre; // ������ �� ������ ����, ������� ����� ��������

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    // ���������� ������ ��������� ��� ������ �������� ����� ����
    public void BringOre(GameObject ore)
    {
        targetOre = ore;
        isMoving = true;
    }

    // ����� ��� �������� ������ � ����
    private void MoveToTarget()
    {
        if (targetOre == null)
        {
            isMoving = false;
            return;
        }

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetOre.transform.position, step);

        // ���� ����� ������ ������� ����, �������� ���
        if (Vector3.Distance(transform.position, targetOre.transform.position) < orePickupDistance)
        {
            CollectOre();
        }
    }

    // ����� ��� ����� ���� � �������� �� ����
    private void CollectOre()
    {
        // ����� ����� ���� ������ ��� ����� ���� � �������� �� ����
        // ��������, ����������� ������� ���� � ����������� ������ ������� �� ����
        Destroy(targetOre);
        isMoving = false;
        targetOre = null;
        ReturnToBase();
    }

    // ����� ��� ����������� �� ����
    private void ReturnToBase()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, baseTransform.position, step);

        // ���� ����� �������� �� ����, ����� �������� �������������� ��������
        if (Vector3.Distance(transform.position, baseTransform.position) < 0.1f)
        {
            // �������������� �������� ����� ����������� �� ����
        }
    }
}


