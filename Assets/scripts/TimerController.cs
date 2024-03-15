using UnityEngine;

public class TimerController : MonoBehaviour
{
    public RectTransform timerSprite;
    public RectTransform clockPointer;
    public float timerDuration = 60f; // Duration of the timer in seconds

    private float currentTime = 0f;
    private Color startColor = Color.black; // Initial color (black)
    private Color endColor = Color.white;
    void Update()
    {
        // Update timer
        currentTime += Time.deltaTime;
        float timerProgress = currentTime / timerDuration;

        // Update clock pointer rotation around the center of timer sprite
        float pointerAngle = Mathf.Lerp(0f, 360f, timerProgress);
        clockPointer.localPosition = GetPointerPosition(pointerAngle);

        // UpdateClockColor(timerProgress);
    }

    Vector2 GetPointerPosition(float angleDegrees)
    {
        // Calculate the position of the clock pointer around the center of the timer sprite
        float radius = timerSprite.sizeDelta.x * 0.5f * 1.2f; // Assuming timer sprite is a circle
        float angleRadians = Mathf.Deg2Rad * angleDegrees;
        float x = Mathf.Sin(angleRadians) * radius;
        float y = Mathf.Cos(angleRadians) * radius;
        return new Vector2(x, y-radius-20f);
    }

    /*void UpdateClockColor(float progress)
    {
        // Blend colors between start and end colors based on time progress
        Color blendedColor = Color.Lerp(startColor, endColor, progress);

        // Update clock sprite color and alpha
        Image clockImage = timerSprite.GetComponent<Image>();
        clockImage.color = blendedColor;

        // Update clock sprite alpha based on time progress
        float alpha = 1f - progress; // Invert progress to make clock fade out over time
        clockImage.color = new Color(clockImage.color.r, clockImage.color.g, clockImage.color.b, alpha);
    }*/
}
