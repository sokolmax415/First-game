using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnEnable()
    {
        // ������������� �� ������� �������� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("���� ������� � ��������� �� ������� �������� �����.");
    }

    private void OnDisable()
    {
        // ������������ �� ������� �������� �����
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("���� �������������� � �������� �� ������� �������� �����.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���������, ���� ��� �����, �� ������� �� ����� ��������� ������
        Debug.Log($"����� ���������: {scene.name}");
        if (scene.name == "Sample Scene") // �������� "SampleScene" �� ��� ����� �����
        {
            // ��������� ��������, ����� ���������, ���� ������� ��������� ����������
            Debug.Log("��������� �������� ��� ��������� ������� ������ ����� �������� �����.");
            StartCoroutine(SetPlayerPositionAfterLoad());
        }
    }

    public void Play()
    {
        // ������ ����, �������� ������ ����� ����� ����
        Debug.Log("������ ������ Play. ��������� ��������� �����.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLastSave()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Debug.Log("������� ����������. ��������� ����� � ��������������� �������.");
            // ��������� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("������� �������� ���������� �����������.");
        }
        else
        {
            Debug.Log("��� ����������� ���������. ��������� ����� �� ���������.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator SetPlayerPositionAfterLoad()
    {
        Debug.Log("������ �������� ��� ��������� ������� ������.");
        // ���� �����, ����� ����� ��������� ����������� � ��� ������� ���� �������
        yield return new WaitForSeconds(0.5f);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // ��������� ����������
            float x = PlayerPrefs.GetFloat("PlayerX", 0f); // �������� �� ��������� �� ������, ���� ����� �� �������
            float y = PlayerPrefs.GetFloat("PlayerY", 0f);
            float z = PlayerPrefs.GetFloat("PlayerZ", 0f);

            Debug.Log($"����������� ����������: ({x}, {y}, {z})");

            // ��������� ���������� ������
            player.transform.position = new Vector3(x, y, z);
            Debug.Log("������� ������ ������� �����������.");
        }
        else
        {
            Debug.LogWarning("����� �� ������ �� �����.");
        }
    }

    public void Exit()
    {
        Debug.Log("����� �� ����.");
        Application.Quit();
    }
}


