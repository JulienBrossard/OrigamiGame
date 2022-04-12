using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public new Animation animation;
    public float minCloudProbability;
    public float playerSpeed;

    private void Start()
    {
        animation = GetComponent<Animation>();
        animation[animation.clip.name].speed = 1f / 60f;
    }
}