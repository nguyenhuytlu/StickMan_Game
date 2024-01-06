using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindAnyObjectByType<UIManager>();
    }

    public void CheckRespawn() 
    {
        //check diem hoi sinh
        if (currentCheckpoint == null) {
            //hien thi man hinh chet
            uiManager.GameOver();
            return;
        }

        transform.position = currentCheckpoint.position; // di chuye nguoi choi den diem hoi sinh
        playerHealth.Respawn();// lam lai mau

        //di chuyen camera den diem hoi sinh
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear"); // trigger cua hoi sinh
        }
    }
}
