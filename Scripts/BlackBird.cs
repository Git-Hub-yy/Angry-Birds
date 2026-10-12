using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    // Start is called before the first frame update
    protected override void FullTimeSkill()
    {
        // 获取指定半径的圆形范围
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f);
        foreach (Collider2D collider in colliders)
        { // 遍历范围内所有可摧毁对象
            Destructiable des = collider.GetComponent<Destructiable>();
            if (des != null)
            {
                des.TakeDamage(Int32.MaxValue); // 伤害值999999或一个比较大的值
            }
        }
        birdState = BirdState.WaitToDie; // 发动技能后自身也要立即销毁，进入等待死亡状态
        LoadNextBird(); // 找到Bird中销毁小鸟的方法，直接调用
    }

}
