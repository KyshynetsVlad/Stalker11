using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LocationsTrigger : MonoBehaviour
{
    public GameObject textLocation;
    public bool onTrigger = false;
    public int nextLocation;
    public VectorValue valuePlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textLocation.SetActive(true);
            onTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textLocation.SetActive(false);
            onTrigger = false;
        }
    }
    private void Update()
    {
        if(onTrigger && Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(nextLocation);
            valuePlayer.initialValue = transform.position;
        }
    }
}