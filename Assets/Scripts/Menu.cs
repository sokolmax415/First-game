using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("Меню активно и подписано на событие загрузки сцены.");
    }

    private void OnDisable()
    {
        // Отписываемся от события загрузки сцены
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("Меню деактивировано и отписано от события загрузки сцены.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Проверяем, если это сцена, на которую мы хотим загрузить игрока
        Debug.Log($"Сцена загружена: {scene.name}");
        if (scene.name == "Sample Scene") // Замените "SampleScene" на имя вашей сцены
        {
            // Запускаем корутину, чтобы подождать, пока объекты полностью загрузятся
            Debug.Log("Запускаем корутину для установки позиции игрока после загрузки сцены.");
            StartCoroutine(SetPlayerPositionAfterLoad());
        }
    }

    public void Play()
    {
        // Начать игру, загружая первую сцену после меню
        Debug.Log("Нажата кнопка Play. Загружаем следующую сцену.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLastSave()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Debug.Log("Найдено сохранение. Загружаем сцену и восстанавливаем позицию.");
            // Загружаем сцену
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Функция загрузки сохранения запустилась.");
        }
        else
        {
            Debug.Log("Нет сохранённого прогресса. Загружаем сцену по умолчанию.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator SetPlayerPositionAfterLoad()
    {
        Debug.Log("Запуск корутины для установки позиции игрока.");
        // Даем время, чтобы сцена полностью загрузилась и все объекты были активны
        yield return new WaitForSeconds(0.5f);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // Загружаем координаты
            float x = PlayerPrefs.GetFloat("PlayerX", 0f); // Значения по умолчанию на случай, если ключи не найдены
            float y = PlayerPrefs.GetFloat("PlayerY", 0f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);

            Debug.Log($"Загруженные координаты: ({x}, {y}, {z})");

            // Применяем координаты игроку
            player.transform.position = new Vector3(x, y, z);
            Debug.Log("Позиция игрока успешно установлена.");
        }
        else
        {
            Debug.LogWarning("Игрок не найден на сцене.");
        }
    }

    public void Exit()
    {
        Debug.Log("Выход из игры.");
        Application.Quit();
    }
}


