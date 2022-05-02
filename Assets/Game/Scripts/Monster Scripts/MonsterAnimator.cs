using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
  public void AttackIsOver()
  {
    transform.parent.GetComponent<MonsterBehaviour>().AttackIsOver();
  }
  public void EatingSoulIsOver()
  {
    transform.parent.GetComponent<MonsterBehaviour>().EatingSoulIsOver();
  }
  
  public void DeathIdOver()
  {
    transform.parent.GetComponent<MonsterBehaviour>().DeathIdOver();
  }
}
