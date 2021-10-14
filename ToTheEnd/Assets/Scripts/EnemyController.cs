using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerMovement PlayerController;
    public SpawnMap MainController;
    public List<EnemyMovement> allEnemies = new List<EnemyMovement>();
    IEnumerator runningTurn;
    public void RemoveEnemies()
    {
        allEnemies.Clear();
    }
    public void AddNewEnemy(EnemyMovement newEnemy)
    {
        if (newEnemy == null) return;
        allEnemies.Add(newEnemy);
    }

    public void StartEnemyTurn()
    {
        if (runningTurn == null)
        {
            runningTurn = enemyTurn();
            StartCoroutine(runningTurn);
        }
        else
        {
            Debug.Log("extra call for enemy to move before all enemies are ready");
        }
    }

    IEnumerator enemyTurn()
    {
        foreach(EnemyMovement singleEnemy in allEnemies)
        { 
            yield return singleEnemy.MoveSingleEnemy(PlayerController.Player.anchoredPosition, PlayerController.oldPosition);
        }
        yield return null;
        // once it is done, let the map know:
        runningTurn = null;
        MainController.EnemyHasMoved();

    }
     
}
