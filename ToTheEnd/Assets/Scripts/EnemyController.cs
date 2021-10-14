using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerMovement playerPosition;
    public List<EnemyMovement> allEnemies = new List<EnemyMovement>();
    public void RemoveEnemies()
    {
        allEnemies.Clear();
    }
    public void AddNewEnemy(EnemyMovement newEnemy)
    {
        allEnemies.Add(newEnemy);
    }

    public IEnumerator EnemyTurn()
    {
        foreach(EnemyMovement singleEnemy in allEnemies)
        { 
            yield return singleEnemy.MoveSingleEnemy(playerPosition.Player.anchoredPosition, playerPosition.oldPosition);
        }
    }
     
}
