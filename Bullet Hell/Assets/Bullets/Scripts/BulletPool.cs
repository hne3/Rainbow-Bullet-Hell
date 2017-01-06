using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour {

    public bool WillGrow = false;

    [SerializeField]
    private int NumToPool = 100;

    [SerializeField]
    private Bullet BulletPrefab;

    private List<Bullet> bullets;

    private int curIndex = 0;

	private void Start ()
    {
        bullets = new List<Bullet>(NumToPool);
        for(int i = 0; i < NumToPool; i++)
        {
            Bullet b = (Bullet)Instantiate(BulletPrefab, transform, false);
            b.gameObject.SetActive(false);
            bullets.Add(b);
        }
    }
	
	public Bullet GetBullet ()
    {
        for(int i = 0; i < NumToPool; i++)
        {
            if(!bullets[i].gameObject.activeInHierarchy)
            {
                curIndex = i;
                return bullets[i];
            }
        }
        if(WillGrow)
        {
            Bullet b = (Bullet)Instantiate(BulletPrefab, transform, false);
            b.gameObject.SetActive(false);
            bullets.Add(b);
            return b;
        }
        return null;
	}
}
