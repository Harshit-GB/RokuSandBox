using UnityEngine;

public class BirdController
{
    private float yVelocity;
    private float gravity = -9.8f;
    private float jumpForce = 1f;
    public Vector2 Position { get; private set; }

    public void Initialize()
    {
        Position = new Vector2(0.2f, 0.5f);
        yVelocity = 0f;
    }

    public void SpawnBird()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = jumpForce;
        }

        yVelocity += (gravity * Time.deltaTime)/4;
        Position += new Vector2(0, yVelocity * Time.deltaTime);

        if (Position.y < 0) Position = new Vector2(Position.x, 0); 
        if (Position.y > 1) Position = new Vector2(Position.x, 1); 
    }
}