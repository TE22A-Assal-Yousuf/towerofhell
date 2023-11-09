using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1280, 800, "game");
Raylib.SetTargetFPS(60);

Vector2 movement = new Vector2(0.1f, 0.1f);

float speed = 5;


Rectangle playerRect = new Rectangle(400, 300, 60, 100);

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

    int characterPositionx = 400;
    int characterPositiony = 300;

    Raylib.ClearBackground(Color.GRAY);

    Raylib.DrawRectangleRec(playerRect, Color.RED);

    movement = Vector2.Zero;

     if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
    {
      movement.X = -1;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
    {
      movement.X = 1;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
    {
      movement.Y = -1;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
    {
      movement.Y = 1;
    }

    if (movement.Length() > 0)
    {
      movement = Vector2.Normalize(movement) * speed;
    }

    playerRect.X += movement.X;
    playerRect.Y += movement.Y;








    Raylib.EndDrawing();
}
