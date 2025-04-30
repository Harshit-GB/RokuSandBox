using UnityEngine;
using System.Collections.Generic;

public class PipeManager
{
    private List<Pipe> pipes = new List<Pipe>();
    private float spawnTimer;
    private float spawnInterval = 1f;
    private float moveSpeed = 0.7f;

    public void Initialize()
    {
        pipes.Clear();
        spawnTimer = 0f;
    }

    public void Spawn()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0f;
            float halfGap = Pipe.gapHeight / 2f;
            float pipeHeight = 0.5f; 
            float margin = pipeHeight + 0.05f; 

            float minY = halfGap + margin;
            float maxY = 1f - halfGap - margin;

            pipes.Add(new Pipe { x = 1f, gapY = Random.Range(minY, maxY) });

            //pipes.Add(new Pipe { x = 1.2f, gapY = Random.Range(0.3f, 0.7f) });
        }

        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            pipes[i].x -= moveSpeed * Time.deltaTime;
            if (pipes[i].x < -1f) 
            {
                pipes.RemoveAt(i);
            }
        }
    }

    public bool CheckCollision(Vector2 birdPos)
    {
        foreach (var pipe in pipes)
        {
            if (Mathf.Abs(pipe.x - birdPos.x) < Pipe.width / 2f)
            {
                if (birdPos.y > pipe.gapY + Pipe.gapHeight / 2f ||
                    birdPos.y < pipe.gapY - Pipe.gapHeight / 2f)
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public bool PassedPipe(Vector2 birdPos)
    {
        foreach (var pipe in pipes)
        {
            if (!pipe.passed && pipe.x < birdPos.x)
            {
                pipe.passed = true;
                return true;
            }
        }
        return false;
    }


    public List<Pipe> GetPipes() => pipes;
}