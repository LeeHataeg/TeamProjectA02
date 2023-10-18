using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPlatform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            Invoke("DisablePlatform", 0.6f); 
        }
    }

    private void DisablePlatform()
    {
        gameObject.SetActive(false); 
        Invoke("EnablePlatform", 2.0f);
    }

    private void EnablePlatform()
    {
        gameObject.SetActive(true); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }
}
