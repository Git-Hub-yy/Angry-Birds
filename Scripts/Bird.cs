using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public enum BirdState
{
    Waiting,
    BeforeShoot,
    AfterShoot,
    WaitToDie

}
public class Bird : MonoBehaviour
{

    public BirdState birdState = BirdState.BeforeShoot;
    private bool isMouseDown=false;
    public float maxDistance = 2.4f;
   // public static Slingshot Instance { get; private set; }
    public float flySpeed = 12;
    protected Rigidbody2D rgd;
    public bool isFlying = true;
    public bool isHaveUsedSkill = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        rgd.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        switch(birdState)
        {
            case BirdState.Waiting:
                break;
            case BirdState.BeforeShoot:
                MoveControl();
                break;
            case BirdState.AfterShoot:
                StopControl();
                SkillControl();
                break;
            case BirdState.WaitToDie:
                break;
            default:
                break;
        }
    }
    private void SkillControl()
    {
        if(isHaveUsedSkill)
        {
            return;
        }
        if(isFlying==true&&Input.GetMouseButtonDown(0))
        {
            isHaveUsedSkill = true;
            FlyingSkill();
        }
        if(Input.GetMouseButtonDown(0))
        {
            isHaveUsedSkill = true;
            FullTimeSkill();
        }
    }
    protected virtual void FlyingSkill()
    {

    }

    protected virtual void FullTimeSkill()
    {

    }
    private void StopControl()
    {
        if(rgd.velocity.magnitude==0)
        {
            birdState = BirdState.WaitToDie;
            Invoke("LoadNextBird", 0.1f);
        }
    }
    protected void LoadNextBird()
    {
        Destroy(gameObject);
        GameObject.Instantiate(Resources.Load("Boom1"), transform.position, Quaternion.identity);
        GameManager.Instance.LoadNextBird();
    }

    private void OnMouseDown()
    {
        if(birdState==BirdState.BeforeShoot&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            isMouseDown = true;
            Slingshot.Instance.StartDraw(transform, GetComponent<CircleCollider2D>().radius);
            AudioManager.Instance.PlayBirdSelect(transform.position);
        }
        
    }
    private void OnMouseUp()
    {
        if(birdState==BirdState.BeforeShoot&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            isMouseDown = false;
            Slingshot.Instance.EndDraw();
            Fly();
        }
        
    }

    private void Fly()
    {
        rgd.bodyType = RigidbodyType2D.Dynamic;
        rgd.velocity = (Slingshot.Instance.getCenterPosition() - transform.position).normalized * flySpeed;
        birdState = BirdState.AfterShoot;
        AudioManager.Instance.PlayBirdFlying(transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(birdState==BirdState.AfterShoot)
        {
            isFlying = false;
            if (collision.relativeVelocity.magnitude > 5)
            {
                AudioManager.Instance.PlayBirdCollision(transform.position);
            }
        }
        
    }
    private void MoveControl()
    {

       if(isMouseDown)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 centerPosition = Slingshot.Instance.getCenterPosition();
            Vector3 mouseDir = mousePosition - centerPosition;
            float distance = mouseDir.magnitude;
            if(distance>maxDistance)
            {
                mousePosition = centerPosition + mouseDir.normalized * maxDistance;
            }
            transform.position = mousePosition;
        }
    }

    public void GoStage(Vector3 position)
    {
        birdState = BirdState.BeforeShoot;
        transform.position = position;
    }
}
