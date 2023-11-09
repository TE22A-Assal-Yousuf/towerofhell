using Raylib_cs;


Raylib.InitWindow(800, 600, "game");
Raylib.SetTargetFPS(60);

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();

  Raylib.ClearBackground(new Color(152,0,1,100));

  Raylib.DrawRectangle( 500, 200, 32, 32, Color.BLACK);


  Raylib.EndDrawing();
}
