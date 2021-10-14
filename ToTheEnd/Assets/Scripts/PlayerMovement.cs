using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform Player;
    private Vector2 targetPosition;
    public Vector2 oldPosition; // for enemies -- find the best movemnt to stalk player.
    private bool movingPlayer;

    // buttons:
    private bool canMoveUp;
    private bool canMoveDown;
    private bool canMoveLeft;
    private bool canMoveRight;

    public Button UpButton;
    public Button DownButton;
    public Button LeftButton;
    public Button RightButton;



    private void Update()
    {
        if (movingPlayer)
        {
            Player.anchoredPosition = Vector2.MoveTowards(Player.anchoredPosition, targetPosition, 1);
            if (Player.anchoredPosition == targetPosition)
            {
                movingPlayer = false;
                ReadSides();
            }
        }
    }

    public void ReadSides()
    {
        StartCoroutine(readSides());

    }
    
    IEnumerator readSides()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("check positions");
        RaycastHit2D hit = Physics2D.Raycast(Player.transform.position + Vector3.up*20, Vector2.up,30);
        canMoveUp = hit.collider != null;
         hit = Physics2D.Raycast(Player.transform.position + Vector3.down * 20, Vector2.down * 20, 30); 
        canMoveDown = hit.collider != null;
         hit = Physics2D.Raycast(Player.transform.position + Vector3.left * 20, Vector2.left * 20, 30); 
        canMoveLeft = hit.collider != null;
         hit = Physics2D.Raycast(Player.transform.position + Vector3.right * 20, Vector2.right * 20, 30); 
        canMoveRight = hit.collider != null;
 
        // update buttons:
        UpdateButtonInteractivity();
    }

    void UpdateButtonInteractivity()
    {
        UpButton.interactable = canMoveUp;
        DownButton.interactable = canMoveDown;
        LeftButton.interactable = canMoveLeft;
        RightButton.interactable = canMoveRight;
    }

    void ManualSetInteractivity(bool val)
    {
        UpButton.interactable = val;
        DownButton.interactable = val;
        LeftButton.interactable = val;
        RightButton.interactable = val;
    }

    // move PC:
    public void MoveUp()
    {
        ManualSetInteractivity(false);
        movingPlayer = true;
        oldPosition = Player.anchoredPosition;
        targetPosition = Player.anchoredPosition + Vector2.up * 50;

    }
    public void MoveDown()
    {
        ManualSetInteractivity(false);
        movingPlayer = true;
        oldPosition = Player.anchoredPosition;
        targetPosition = Player.anchoredPosition + Vector2.down * 50;
    }
    public void MoveLeft()
    {
        ManualSetInteractivity(false);
        movingPlayer = true;
        oldPosition = Player.anchoredPosition;
        targetPosition = Player.anchoredPosition + Vector2.left * 50;
    }
    public void MoveRight()
    {
        ManualSetInteractivity(false);
        movingPlayer = true;
        oldPosition = Player.anchoredPosition;
        targetPosition = Player.anchoredPosition + Vector2.right * 50;
    }



}
