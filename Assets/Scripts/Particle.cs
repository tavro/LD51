using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float timer;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AnimationCurve opacityCurve;

    [SerializeField] private AnimationCurve speedCurve;
    private float moveAngle;
    private float origSpriteAngle;

    public void SetAngle(float newAngle)
    {
        origSpriteAngle = spriteRenderer.transform.rotation.eulerAngles.z;
        moveAngle = newAngle;
        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, moveAngle + origSpriteAngle);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
                Destroy(gameObject);
            
            float lifeTimePercent = timer / lifeTime;
            Color color = spriteRenderer.color;
            color.a = opacityCurve.Evaluate(lifeTimePercent);
            spriteRenderer.color = color;

            Vector2 moveDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * moveAngle), Mathf.Sin(Mathf.Deg2Rad * moveAngle));
            transform.Translate(moveDir * speedCurve.Evaluate(lifeTimePercent) * Time.deltaTime, Space.World);
        }
    }
}
