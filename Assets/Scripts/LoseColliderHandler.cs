using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseColliderHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int lastScene = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(lastScene);
    }

}
