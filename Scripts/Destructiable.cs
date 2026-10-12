using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Destructiable : MonoBehaviour
{
    public int HP = 100;
    private int currentHP;
    public List<Sprite> injuredSpriteList;
    private SpriteRenderer spriteRenderer;
    private GameObject boomPrefab;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = HP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boomPrefab = Resources.Load<GameObject>("Boom1");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void Dead()
    {
        PlayAudioDestroyed();
        Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        float damage = collision.relativeVelocity.magnitude * 10f;
        
      
        
        TakeDamage((int)damage);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Dead();
        }
        else
        {
            Sprite beforeSprite = spriteRenderer.sprite;
            int index = (int)((HP - currentHP) / (HP / (injuredSpriteList.Count + 1.0f))) - 1;
            if (index != -1)
            {
                spriteRenderer.sprite = injuredSpriteList[index];
            }
            if (beforeSprite != spriteRenderer.sprite)
            {
                PlayAudioCollision();
            }
        }

    }

    protected virtual void PlayAudioCollision()
    {
        AudioManager.Instance.PlayWoodCollision(transform.position);
    }
    protected virtual void PlayAudioDestroyed()
    {
        AudioManager.Instance.PlayWoodDestroyed(transform.position);
    }
    private void UpdateSprite()
    {
        if (injuredSpriteList == null || injuredSpriteList.Count == 0) return;

        // 计算受伤程度比例（0~1）
        float healthPercent = (float)currentHP / HP;
        // 映射到精灵列表索引（假设列表从轻伤到重伤）
        int index = Mathf.Clamp(
            (int)((1f - healthPercent) * injuredSpriteList.Count),
            0,
            injuredSpriteList.Count - 1
        );
        spriteRenderer.sprite = injuredSpriteList[index];
    }

    
}