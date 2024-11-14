using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadLastSave()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            SceneManager.LoadScene("SampleScene");  // ���������, ��� ��� ����� ���������

            StartCoroutine(SetPlayerPositionAfterLoad());
        }
        else
        {
            Debug.Log("��� ����������� ���������.");
            SceneManager.LoadScene("SampleScene");
        }
    }


    private IEnumerator SetPlayerPositionAfterLoad()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            Debug.Log($"����������� ����������: ({x}, {y}, {z})");

            player.transform.position = new Vector3(x, y, z);
        }
        else
        {
            Debug.LogWarning("����� �� ������ �� �����.");
        }
    }

}

