using Raylib_cs;
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

string scene = "clickgame";

int spawnX = 1280 / 2;
int spawnY = 800 / 2;

int playerPositionX = 500;
int playerPositionY = 500;

int enemySpawnX = windowWidth / 2 - 128;
int enemySpawnY = windowHeight / 2 - 128;

int swordPosX = playerPositionX + 38;
int swordPosY = playerPositionY - 62;

int swordWidth = 20;
int swordHeight = 64;

int coinSpawnX = 100;
int coinSpawnY = 100;
int coinWorth = 20;

int points = 0;
int healthPoints = 100;
int bossMaxHealth = 999999999;
int bossHealth = 999999999;
int bossDMG = 1000;

string heroSword;
int heroSwordDMG = 1;

string currentSword;
int currentSwordDMG;

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// Stuff
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Texture2D swordTexture = Raylib.LoadTexture(@"swordtexture.png");
Rectangle swordRect = new Rectangle(swordPosX, swordPosY, swordWidth, swordHeight);

Rectangle playerRect = new Rectangle(playerPositionX, playerPositionY, 64, 64);
Rectangle enemyRect = new Rectangle(enemySpawnX, enemySpawnY, 64, 64);

Rectangle bgrect = new Rectangle(0, 43, windowWidth, windowHeight);
Texture2D bgTexture = Raylib.LoadTexture(@"bgTowerImg.png");

Rectangle coinRect = new Rectangle(coinSpawnX, coinSpawnY, 32, 32);

List<Rectangle> walls = new();
List<Rectangle> enemies = new();

Raylib.GetMousePosition();



//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//stuff +
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

enemyRect.Width = 128;
enemyRect.Height = 128;

bgrect.Width = bgTexture.Width;
bgrect.Height = bgTexture.Height;

swordRect.Width = swordTexture.Width;
swordRect.Height = swordTexture.Height;

walls.Add(new Rectangle(0, 0, 1280, 32));
walls.Add(new Rectangle(0, 0, 32, 800));
walls.Add(new Rectangle(1248, 0, 32, 800));
walls.Add(new Rectangle(0, 768, 1280, 32));

enemies.Add(new Rectangle(200, 200, 100,20));

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
            scene = "pregame";
        }           
        
        
    }
    else if (scene == "pregame"){

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "game";
        }
    }
    else if (scene == "game")
    {
        
        
        Raylib.DrawRectangleRec(enemyRect, Color.SKYBLUE);
        Raylib.DrawRectangleRec(playerRect, Color.RED);


         if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "pause";
        }

        currentSword = "heroSword";
        currentSwordDMG = heroSwordDMG;

        bool isInAWall = CheckIfWall(swordRect, playerRect, walls);

        if (isInAWall == true)
        {
            playerRect.X -= movement.X;
        }

        if (isInAWall == true)
        {
            swordRect.X -= movement.X;
        }

        isInAWall = CheckIfWall(swordRect, playerRect, walls);

        if (isInAWall == true)
        {
            playerRect.Y -= movement.Y;
        }

        if (isInAWall == true)
        {
            swordRect.Y -= movement.Y;
        }

        if (Raylib.CheckCollisionRecs(playerRect, enemyRect))
        {
            healthPoints -= bossDMG; 
        }
        if (Raylib.CheckCollisionRecs(swordRect, enemyRect))
        {
            bossHealth -= currentSwordDMG;  
        }

        if(healthPoints <= 0){
            scene = "death";
        }

        if(bossHealth <= bossMaxHealth - 1){
            enemySpawnY -= 10;
            healthPoints = 0;
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
    else if (scene == "pause2"){
        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){

            scene = "clickgame";

        }
    }
    else if (scene == "death"){

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){

            scene = "cutscene";

        }
        
    }
    else if (scene == "cutscene"){

        if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){

            scene = "clickgame";

        }
    }

    else if (scene == "clickgame"){
        
        
        Vector2 mpousePos = Raylib.GetMousePosition();
        swordRect.X = mpousePos.X - 10;
        swordRect.Y = mpousePos.Y - 50;

        if(Raylib.CheckCollisionPointRec(mpousePos, coinRect)){

            points += coinWorth;


        }


       // if(coinRect.Y >= mpousePos.Y && coinRect.X <= mpousePos.X && coinRect.Width <= mpousePos.X && coinRect.Height <= mpousePos.Y && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)){
            
       points += coinWorth;

        //}
    

        

        Raylib.DrawRectangleRec(coinRect, Color.GOLD);


        Raylib.DrawRectangle(0, 768, 1280, 32, Color.DARKBROWN);
        Raylib.DrawRectangle(0, 0, 1280, 32, Color.DARKBROWN);
        Raylib.DrawRectangle(0, 0, 32, 800, Color.DARKBROWN);
        Raylib.DrawRectangle(1000, 0, 500, 800, Color.DARKBROWN);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            scene = "pause2";
        }

    }

    static bool CheckIfWall(Rectangle swordRect, Rectangle playerRect, List<Rectangle> walls)
{
  foreach (Rectangle wall in walls)
  {
    if (Raylib.CheckCollisionRecs(playerRect, wall))
    {
      return true;
    }
    if (Raylib.CheckCollisionRecs(swordRect, wall))
    {
      return true;
    }

  }
  return false;
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

        Raylib.ClearBackground(Color.GRAY);
        //Raylib.DrawTexture(bgTexture, (int) bgrect.X, (int) bgrect.Y, Color.WHITE);
        Raylib.DrawText("press space to start", 100, 500, 100, Color.BLACK);

    }
    else if(scene == "pregame"){

        Raylib.DrawText("STORY:", 570, 50, 30, Color.WHITE);
        Raylib.DrawText("Hello traveler", 480, 100, 40, Color.WHITE);
        Raylib.DrawText("You have died and been reincarnated", 230, 180, 40, Color.WHITE);
        Raylib.DrawText("and transported to a different world", 230, 260, 40, Color.WHITE);
        Raylib.DrawText("Your mission is to defeat the demon lord", 214, 340, 40, Color.WHITE);
        Raylib.DrawText("Good luck!", 520, 420, 40, Color.WHITE);
        Raylib.DrawText("-press space to continue", 150, 560, 70, Color.WHITE);

        Raylib.ClearBackground(Color.BLACK);

    }
    else if(scene == "game"){
        Raylib.ClearBackground(Color.MAROON);

        Raylib.DrawRectangleRec(swordRect, Color.GRAY);
        Raylib.DrawTexture(swordTexture, (int) swordRect.X, (int) swordRect.Y, Color.WHITE);
        
     

        Raylib.DrawText("-Press space to pause!", 1025, 750, 20, Color.WHITE);


    foreach (Rectangle wall in walls)
    {
      Raylib.DrawRectangleRec(wall, Color.BLACK);
    }

    Raylib.DrawText($"HP: {healthPoints}", 10, 10, 32, Color.WHITE);
    Raylib.DrawText($"BOSS HEALTH: {bossHealth}", enemySpawnX - 120, enemySpawnY - 80, 32, Color.WHITE);
    
    }
    else if(scene == "death"){

        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Wait what you died??", 370, 50, 50, Color.WHITE);
        Raylib.DrawText("How could this happen", 350, 120, 50, Color.WHITE);
        Raylib.DrawText("You even had the hero's sword", 250, 190, 50, Color.WHITE);
        Raylib.DrawText("Okay i guess you still need some training", 150, 260, 50, Color.WHITE);
        Raylib.DrawText("Even though you are the chosen one...", 180, 330, 50, Color.WHITE);
        Raylib.DrawText("-Press space to continue!", 390, 500, 40, Color.WHITE);

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
    else if (scene == "pause2"){
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
    else if (scene == "cutscene"){
        Raylib.DrawText("You are sent to the slime fields...", 180, 330, 50, Color.BLACK); 
        Raylib.DrawText("-Press space to continue!", 390, 500, 40, Color.BLACK);
        Raylib.ClearBackground(Color.GRAY);
        
    }
    else if (scene == "clickgame"){
        Raylib.DrawText($"Points: {points}", 1080, 30, 32, Color.WHITE);

        Raylib.ClearBackground(Color.GREEN);


        Raylib.DrawTexture(swordTexture, (int) swordRect.X, (int) swordRect.Y, Color.WHITE);


    }

    Raylib.EndDrawing();

    
}