﻿using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

int windowWidth = 1280;
int windowHeight = 800;
int fps = 60;

Raylib.InitWindow(windowWidth, windowHeight, "Battletower");
Raylib.SetTargetFPS(fps);

Vector2 movement = new Vector2(0.1f, 0.1f);

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

float speed = 5;

string scene = "start";

int playerPositionX = windowWidth / 2 - 64;
int playerPositionY = windowHeight / 2 - 64;

int swordPosX = playerPositionX + 38;
int swordPosY = playerPositionY - 62;

int swordWidth = 20;
int swordHeight = 64;


//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Stuff
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Texture2D background = Raylib.LoadTexture(@"");
Texture2D swordTexture = Raylib.LoadTexture(@"swordtexture.png");
Rectangle playerRect = new Rectangle(playerPositionX, playerPositionY, 64, 64);
Rectangle swordRect = new Rectangle(swordPosX , swordPosY, swordWidth, swordHeight);



//--------------------------------------------------------------------------------------------------------------------------------------------------------------
//Game be like
//--------------------------------------------------------------------------------------------------------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (scene == "start")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }
    else if (scene == "game")
    {
        Raylib.DrawRectangleRec(playerRect, Color.RED);

        //Raylib.DrawTextureEx(swordtexture, , 45, 1, Color.BLUE);
        /*
        [Better map layout and stuff]

        make the map a grid that everything is placed on 
        ↓

        */

        Raylib.GetMousePosition();

         if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "pause";
        }

        
    

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F)) {

            swordWidth = 40;
            swordHeight = swordHeight * 2;

        }

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

        /*
        [Make character movement better?]

        if hit button once change direction 
        ↓
        else if button is held walk one tile att a time 

        */
    }
    else if (scene == "pause"){
        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){

            scene = "game";

        }
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

        Raylib.DrawRectangleRec(swordRect, Color.GRAY);
        Raylib.DrawTexture(swordTexture, swordPosX, swordPosY, Color.WHITE);
        

    }
    else if (scene == "pause"){
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Paused", 320, 100, 100, Color.GRAY);

    }

    /*
    [SWORD ANIMATION AND HITBOX]

    if mouse button gets clicked 
    ↓
    make the hitbox of the sword bigger / as big as the sword animation/look
    ↓
    play an animation that shows the sword swinging relly fast but in reality the hit box is just expaning 



    */
   




    Raylib.EndDrawing();
}