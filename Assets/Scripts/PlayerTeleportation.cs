using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTeleportation : MonoBehaviour
{
    private GameObject currentTeleporter;

    private void SavePlayerPosition()
    {
        // Сохраняем координаты игрока перед переходом на новый уровень (например, при телепорте)
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        PlayerPrefs.Save();  // Не забываем сохранить изменения
        Debug.Log($"Сохранены координаты: ({transform.position.x}, {transform.position.y}, {transform.position.z})");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                // Перемещаем игрока и сохраняем его позицию
                Debug.Log("Кнопка E нажата. Начинаем телепортацию.");
                transform.position = currentTeleporter.GetComponent<Teleport>().GetDestination().position;
                Debug.Log("Телепортация выполнена.");
                SavePlayerPosition();
            }
            else
            {
                Debug.LogWarning("Телепортер не найден. Невозможно переместить игрока.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Нажата клавиша Escape. Переход на предыдущую сцену.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            Debug.Log($"Игрок вошел в триггер телепортера: {collision.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                Debug.Log("Игрок покинул триггер телепортера.");
            }
        }
    }
}




