using UnityEngine;
using System.Collections.Generic;

public class Renderer2D : MonoBehaviour
{
    private Material mat;
    private GameManager gm;

    void Start()
    {
        mat = new Material(Shader.Find("Hidden/Internal-Colored"));
        gm = FindObjectOfType<GameManager>();
    }

    void OnPostRender()
    {
        if (gm == null || gm.bird == null || gm.pipeManager == null) return;

        GL.PushMatrix();
        GL.LoadOrtho();
        mat.SetPass(0);

        DrawBird(gm.bird.Position);
        foreach (var pipe in gm.pipeManager.GetPipes())
        {
            DrawPipe(pipe);
        }

        GL.PopMatrix();
    }

    void DrawBird(Vector2 pos)
    {
        DrawRect(pos.x, pos.y, 0.05f, 0.05f, Color.green);
    }

   
    
    void DrawPipe(Pipe pipe)
    {
        float halfGap = Pipe.gapHeight / 2f;
        float pipeHeight = 1f; 

        // Bottom pipe
        float bottomPipeCenterY = (pipe.gapY - halfGap) / 2f;
        float bottomPipeHeight = pipe.gapY - halfGap;
        DrawRect(pipe.x, bottomPipeCenterY, Pipe.width, bottomPipeHeight, Color.white);

        // Top pipe
        float topPipeHeight = 1f - (pipe.gapY + halfGap);
        float topPipeCenterY = pipe.gapY + halfGap + topPipeHeight / 2f;
        DrawRect(pipe.x, topPipeCenterY, Pipe.width, topPipeHeight, Color.white);
    }


    void DrawRect(float cx, float cy, float w, float h, Color color)
    {
        GL.Begin(GL.QUADS);
        GL.Color(color);
        GL.Vertex3(cx - w / 2, cy - h / 2, 0);
        GL.Vertex3(cx + w / 2, cy - h / 2, 0);
        GL.Vertex3(cx + w / 2, cy + h / 2, 0);
        GL.Vertex3(cx - w / 2, cy + h / 2, 0);
        GL.End();
    }
}