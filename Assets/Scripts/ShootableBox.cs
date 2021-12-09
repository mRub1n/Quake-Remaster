using UnityEngine;
using System.Collections;

public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 1;

	public void Damage(int damageAmount)
	{
		//subtract damage amount when Damage function is called
		currentHealth -= damageAmount;

		//Check if health has fallen below zero
		if (currentHealth <= 0) 
		{
			StartCoroutine(ExampleCoroutine());

			//if health has fallen below zero, deactivate it 
			//gameObject.SetActive (false);
		}

		IEnumerator ExampleCoroutine()
		{
			yield return new WaitForSeconds(1);
			Destroy(gameObject);
		}

	}
}
