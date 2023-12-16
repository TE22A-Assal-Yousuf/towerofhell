﻿using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

int windowWidth = 1280;
int windowHeight = 800;
int fps = 60;

Raylib.InitWindow(windowWidth, windowHeight, "Battletower");
Raylib.SetTargetFPS(fps);

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Vector2 movement = new Vector2(0.1f, 0.1f);

float speed = 5;

string scene = "start";

int playerPositionX = 576;
int playerPositionY = 528;

int enemySpawnX = windowWidth / 2 - 128;
int enemySpawnY = windowHeight / 2 - 128;

int swordPosX = playerPositionX + 38;
int swordPosY = playerPositionY - 62;

int swordWidth = 20;
int swordHeight = 64;

int points = 0;
int healthPoints = 100;


//Stats --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/* NOTES!!!!!!!!

int streangth
- increase attach damage and raise the number probability
- if streangth is high it = higher chance of miss but not to high
- 

int defence
- reduce damage lol
- slower attack speed
- idk

int attackspeed
-
-
-

int evasion
- how often you could dodge attacks / negate damage "gwen is imune =D" % wise
- not to high but if you only build it it becomes verry funny becaus no hit possible
- funny haha make one boss that has extra high evasion so its impossible
- but ur def stat will be absolutely tanked so that if you do get hit you will be fucked

int accuracy
- with 100% accuracy comes great reduction to damage so you dont get some bs hits
-


*/

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Stuff
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Texture2D swordTexture = Raylib.LoadTexture(@"swordtexture.png");
Rectangle swordRect = new Rectangle(swordPosX , swordPosY, swordWidth, swordHeight);

Rectangle playerRect = new Rectangle(playerPositionX, playerPositionY, 64, 64);
Rectangle enemyRect = new Rectangle(enemySpawnX, enemySpawnY, 64, 64);

Rectangle bgrect = new Rectangle(0, 43, windowWidth, windowHeight);
Texture2D bgTexture = Raylib.LoadTexture(@"bgTowerImg.png");

List<Rectangle> walls = new();
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//stuff +
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

enemyRect.Width = 128;
enemyRect.Height = 128;


bgrect.Width = bgTexture.Width;
bgrect.Height = bgTexture.Height;


swordRect.Width = swordTexture.Width;
swordRect.Height = swordTexture.Height;

walls.Add(new Rectangle(32, 32, 32, 128));
walls.Add(new Rectangle(64, 32, 128, 32));
walls.Add(new Rectangle(192, 32, 32, 128));
walls.Add(new Rectangle(256, 32, 32, 128));

Rectangle doorRect = new Rectangle(760, 460, 32, 32);

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

            playerPositionX = 576;
            playerPositionY = 528;
            healthPoints = 100;

            scene = "game";
        }
    }
    else if (scene == "game")
    {
        Raylib.DrawRectangleRec(enemyRect, Color.SKYBLUE);
        Raylib.DrawRectangleRec(playerRect, Color.RED);

        //Raylib.DrawTextureEx(swordtexture, , 45, 1, Color.BLUE);
        /*
        [Better map layout and stuff]

        make the map a grid that everything is placed on 
        ↓

        */

        //pause

        Image Pimg = Raylib.LoadImageFromScreen();
        Raylib.GetMousePosition();

         if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "pause";
        }

        //button
        /*

        - check where mouse is
        - if mouse is over the buttons pos
        - then if mouse is clicked over this pos 
        - the button exe whatever ist supposed to 
        
        */

        //fighting

        

        /*

        - write simillar code to the fighting game
        - add some cool shit / more options 
        - idk but add more
        
        */

        //enemy

        /*

        - make if toutch 
        - popup window when i collide with enemy
        - ask if i want to fight
        - sometimes ask but force me no matter what 
        
        */

        //collision

        /*

        - check if i have collided 
        - if true execute whatever i need
        
        */

        bool isInAWall = CheckIfWall(playerRect, walls);

        if (isInAWall == true)
        {
            playerRect.X -= movement.X;
        }
    
        isInAWall = CheckIfWall(playerRect, walls);

        if (isInAWall == true)
        {
            playerRect.Y -= movement.Y;
        }

        if (Raylib.CheckCollisionRecs(playerRect, doorRect))
        {
            points++;
        }

        if (Raylib.CheckCollisionRecs(playerRect, enemyRect))
        {
            healthPoints--; 
        }

        if(healthPoints <= 0){
            scene = "death";
        }

        //walk

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

        //run

        if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT) == true){
            speed = 10;
            
        }
        else if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT_SHIFT)){
            speed = 5;
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
    else if (scene == "pause"){
        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){

            scene = "game";

        }
    }
    else if (scene == "death"){

        

        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){

            scene = "start";

        }
    }
    
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //OMG THAT LOOKS NICE
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    if(scene == "start"){

/*
        Raylib.DrawLine( 0, 35, windowWidth, 35, Color.RED);
        Raylib.DrawLine( 0, 765, windowWidth, 765, Color.RED);
*/
    //-------------------------------------------------------------------------------

        Raylib.ClearBackground(Color.BLANK);
        Raylib.DrawTexture(bgTexture, (int) bgrect.X, (int) bgrect.Y, Color.WHITE);
        Raylib.DrawText("press space to start", 100, 500, 100, Color.BLACK);

    }
    else if(scene == "game"){
        Raylib.ClearBackground(Color.MAROON);

        Raylib.DrawRectangleRec(swordRect, Color.GRAY);
        Raylib.DrawTexture(swordTexture, (int) swordRect.X, (int) swordRect.Y, Color.WHITE);
        
     

        Raylib.DrawText("-Press space to pause!", 1025, 760, 20, Color.WHITE);

        Raylib.DrawRectangleRec(doorRect, Color.BROWN);

    foreach (Rectangle wall in walls)
    {
      Raylib.DrawRectangleRec(wall, Color.BLACK);
    }

    Raylib.DrawText($"Points: {points}", 10, 42, 32, Color.WHITE);
    Raylib.DrawText($"HP: {healthPoints}", 10, 10, 32, Color.WHITE);
        

        
        

    }
    else if(scene == "death"){

        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("PAUSED", 434, 0, 100, Color.WHITE);
        Raylib.DrawText("Press space to resume!", 390, 700, 40, Color.WHITE);

  
    }

    else if (scene == "pause"){
//Draw lineup
       /*
        Raylib.DrawLine( 640, 0, 640, 800, Color.RED);
        Raylib.DrawLine( 436, 0, 436, 800, Color.RED);
        Raylib.DrawLine( 844, 0, 844, 800, Color.RED);
        */
//actual

        
        Raylib.DrawText("PAUSED", 434, 0, 100, Color.WHITE);
        Raylib.DrawText("Press space to resume!", 390, 700, 40, Color.WHITE);



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

    static bool CheckIfWall(Rectangle playerRect, List<Rectangle> walls)
{
  foreach (Rectangle wall in walls)
  {
    if (Raylib.CheckCollisionRecs(playerRect, wall))
    {
      return true;
    }
  }
  return false;
}
}