﻿using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;


Raylib.InitWindow(1280, 800, "game");
Raylib.SetTargetFPS(60);

Vector2 movement = new Vector2(0.1f, 0.1f);

float speed = 5;

string scene = "start";

int playerpositionx = 20;
int playerpositiony = 30;

Texture2D swordtexture = Raylib.LoadTexture("pixil-frame-0.png");
Rectangle playerRect = new Rectangle(playerpositionx, playerpositiony, 64, 64);
Rectangle swordRect = new Rectangle(playerpositionx + 38 , playerpositiony - 62, 20, 64);




//--------------------------------------------------------------------------------------------------------------------------------------------------------------
//Game be like
//--------------------------------------------------------------------------------------------------------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

    if (scene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }
    else if (scene == "game")
    {
       
        Raylib.DrawRectangleRec(swordRect, Color.GRAY);
        Raylib.DrawRectangleRec(playerRect, Color.RED);

        Raylib.DrawTextureEx(swordtexture, swordRect, 45, 1, Color.BLUE);
        

        movement = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
        movement.X = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
        movement.X = 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
        movement.Y = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)|| Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
        movement.Y = 1;
        }

        if (movement.Length() > 0)
        {
        movement = Vector2.Normalize(movement) * speed;
        }

        playerRect.X += movement.X;
        playerRect.Y += movement.Y;
        swordRect.X += movement.X;
        swordRect.Y += movement.Y;
    }
    
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //OMG THAT LOOKS NICE
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    if(scene == "start"){

        Raylib.ClearBackground(Color.BLUE);
        Raylib.DrawText("press space to start", 100, 500, 100, Color.BLACK);

    }
    else if(scene == "game"){
        Raylib.ClearBackground(Color.MAROON);
    }

   




    Raylib.EndDrawing();
}