using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletPool : MonoBehaviour {

    public bool WillGrow = false;

    public float Lifetime = 1.0f;

    [SerializeField]
    private int NumToPool = 100;

    [SerializeField]
    private Bullet BulletPrefab;

    private List<Bullet> bullets;

    private int curIndex = 0;

    private WaitForSeconds lifetime;

	private void Start ()
    {
        bullets = new List<Bullet>(NumToPool);
        for(int i = 0; i < NumToPool; i++)
        {
            Bullet b = (Bullet)Instantiate(BulletPrefab, transform, false);
            b.gameObject.SetActive(false);
            bullets.Add(b);
        }
        lifetime = new WaitForSeconds(Lifetime);
    }
	
	public Bullet GetBullet ()
    {
        for(int i = 0; i < NumToPool; i++)
        {
            if(!bullets[i].gameObject.activeInHierarchy)
            {
                curIndex = i;
                StartCoroutine(TimeExpiration(bullets[i]));
                return bullets[i];
            }
        }
        if(WillGrow)
        {
            Bullet b = (Bullet)Instantiate(BulletPrefab, transform, false);
            b.gameObject.SetActive(false);
            bullets.Add(b);
            StartCoroutine(TimeExpiration(b));
            return b;
        }
        return null;
	}

    private IEnumerator TimeExpiration(Bullet b)
    {
        yield return lifetime;
        b.gameObject.SetActive(false);
        yield break;
    }
}
