using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTeleportation : MonoBehaviour
{
    private GameObject currentTeleporter;

    private void SavePlayerPosition()
    {
        // ��������� ���������� ������ ����� ��������� �� ����� ������� (��������, ��� ���������)
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        PlayerPrefs.Save();  // �� �������� ��������� ���������
        Debug.Log($"��������� ����������: ({transform.position.x}, {transform.position.y}, {transform.position.z})");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                // ���������� ������ � ��������� ��� �������
                Debug.Log("������ E ������. �������� ������������.");
                transform.position = currentTeleporter.GetComponent<Teleport>().GetDestination().position;
                Debug.Log("������������ ���������.");
                SavePlayerPosition();
            }
            else
            {
                Debug.LogWarning("���������� �� ������. ���������� ����������� ������.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("������ ������� Escape. ������� �� ���������� �����.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            Debug.Log($"����� ����� � ������� �����������: {collision.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                Debug.Log("����� ������� ������� �����������.");
            }
        }
    }
}




