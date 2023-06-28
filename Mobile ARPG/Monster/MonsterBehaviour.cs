using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBehaviour : MonoBehaviour
{
	[SerializeField] private static float actionDelay = 0.2f;
	private static Queue<MonsterBehaviour> monserQueue = new Queue<MonsterBehaviour>();
	private static float attackDistance = 3.5f;

	[SerializeField] private Transform target;
	[SerializeField] private NavMeshAgent myNavMesh;
	[SerializeField] private Life targetLife;
	private bool isAlive = true;
	private bool rushStart = false;

	public static void RefreshQueue()
	{
		monserQueue = new Queue<MonsterBehaviour>();
	}

	public static IEnumerator ActionNext()
	{
		while (true)
		{
			if (LifePlayer.SomePlayersAlive)
			{
				if (monserQueue.Count != 0)
				{
					MonsterBehaviour monster = monserQueue.Dequeue();
					if (monster.isAlive && monster != null)
					{
						monster.Move();
						monster.Invoke(nameof(returnToQueue), actionDelay);

						if (monster.myNavMesh.remainingDistance <= attackDistance)
						{
							if (!monster.rushStart)
							{
								monster.GetComponent<Renderer>().material.color = Color.red;
								monster.myNavMesh.speed *= 1.5f;
								monster.myNavMesh.acceleration *= 1.5f;
								monster.rushStart = true;
							}

							monster.targetLife = monster.target.gameObject.GetComponent<Life>();
							monster.Attack();
						}
						else
						{
							//monster.GetComponent<Renderer>().material.color = Color.grey;
						}


						yield return null;
					}
					else
					{
						if(monster != null)
							Destroy(monster.gameObject);
						yield return null;
					}
				}
				else
				{
					yield return null;
				}
			}
			yield return null;
		}
		

		//if (!LifePlayer.SomePlayersAlive)
		//{
		//	yield break;
		//}
	}

	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

	private void Start()
	{
		if (myNavMesh == null)
			myNavMesh = GetComponent<NavMeshAgent>();
		monserQueue.Enqueue(this);

		myNavMesh.SetDestination(target.position);
	}

	public void Die()
	{
		isAlive = false;
	}

	private void OnDisable()
	{
		CancelInvoke();
	}

	private void returnToQueue()
	{
		monserQueue.Enqueue(this);
	}

	private void Attack()
	{
		targetLife.TakeDamage(1);
	}

	private void Move()
	{
		if(target == null)
			return;
		myNavMesh.SetDestination(target.position);
	}
}
