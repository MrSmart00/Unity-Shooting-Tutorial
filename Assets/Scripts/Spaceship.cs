using UnityEngine;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Spaceship : MonoBehaviour
{
	// 移動スピード
	[Range(0.0f, 10.0f)]
	public float speed = 1.0f;
	
	// 弾を撃つ間隔
	[Range(0.0f,  1.0f)]
	public float shotDelay = 1.0f;
	
	// 弾のPrefab
	public GameObject bullet;
	
	// 爆発のPrefab
	public GameObject explosion;
	
	// 爆発の作成
	public void Explosion (bool gameover)
	{
		GameObject go = (GameObject)Instantiate (explosion, transform.position, transform.rotation);
		go.GetComponent<Explosion> ().gameOver = gameover;
	}
	
	// 弾の作成
	public void Shot (Transform origin)
	{
		GameObject b = ObjectPool.instance.GetGameObject (bullet, origin.position, origin.rotation);
		foreach (Transform child in b.transform){
			child.gameObject.SetActive(true);
		}
	}

	protected abstract void Move (Vector2 direction);
}
