using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public class DuckTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            */
            DeathBehaviour deathEvent = GameObject.Find("DeathBehaviour").GetComponent<DeathBehaviour>();
            StartCoroutine(deathEvent.DuckDied(20, this.GetComponentInParent<Collider>()));

        }
    }
}
