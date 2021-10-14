using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 targetPosition = new Vector2();

    private RectTransform rectTransform;

    // potential actions for the enemy:
    private Vector2[] Directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Vector2 selectedMove = new Vector2();
    private float selectedMoveValue = 0f;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        if (rectTransform==null)
        {
            Debug.Log("Enemy prefab is not on UI: "+gameObject.name); 
        }
    }

    public IEnumerator MoveSingleEnemy(Vector2 currentPlayerPosition, Vector2 oldPlayerPosition)
    {
        bool moveEnemy = UpdateEnemyPosition(currentPlayerPosition, oldPlayerPosition);
        if (!moveEnemy) yield break;

        // otherwise, move:
        while (Vector2.Distance(rectTransform.anchoredPosition, targetPosition)<0.01f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, 1);
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
    }

    private bool UpdateEnemyPosition(Vector2 currPlayer, Vector2 oldPlayerPosition)
    {
        // pick best dir to go with curr position,
        // if none are available, don't move.
        // if there are more than one good directions to move, try the old player position:
        selectedMove = Vector2.zero;
        selectedMoveValue = 0f;
        float newMoveValue = 0f;
        for (int i = 0; i<Directions.Length; i++)
        {
            newMoveValue = calculateMoveValue(currPlayer, rectTransform.anchoredPosition, Directions[i]);
            if (newMoveValue == selectedMoveValue && selectedMoveValue !=0)
            {
                // more than one movement will work, check if this move should be replaced:
                if (calculateMoveValue(oldPlayerPosition, rectTransform.anchoredPosition, selectedMove) < calculateMoveValue(oldPlayerPosition, rectTransform.anchoredPosition, Directions[i]))
                {
                    selectedMoveValue = newMoveValue;
                    selectedMove = Directions[i];
                }
            } else if (newMoveValue > selectedMoveValue) // can choose not to move, if all available are negative
            {
                selectedMoveValue = newMoveValue;
                selectedMove = Directions[i];
            }
        }

        // check if it needs moving:
        if (selectedMoveValue != 0)
        {
            // moving...
            targetPosition = rectTransform.anchoredPosition + selectedMove;
            return true;
        }

        return false;
        
    }

    // value is the the change in distance between the users (old - new)
    // best value would be the largest positive change
    private float calculateMoveValue(Vector2 playerPosition, Vector2 enemyPosition, Vector2 suggestedMove)
    {
        return Vector2.Distance(playerPosition, enemyPosition) - Vector2.Distance(playerPosition, enemyPosition + suggestedMove);
    }
}
