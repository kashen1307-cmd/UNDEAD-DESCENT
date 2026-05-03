using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public EnemyAttack enemyAttack;

    public void DealDamage()
    {
        enemyAttack.DealDamage();
    }
}