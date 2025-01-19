using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport2 : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Protagonist")
        {
            string sceneName = sceneNames[Random.Range(0,sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }

    }
}
