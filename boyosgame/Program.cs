using Raylib_cs;


Raylib.InitWindow(800, 600, "game");
Raylib.SetTargetFPS(60);
Rectangle hdjhd = new Rectangle(10, 10, 200, 200);

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

    int characterPositionx = 400;
    int characterPositiony = 300;

    Raylib.ClearBackground(new Color(152, 0, 1, 100));

    //   Raylib.DrawRectangle( characterPositionx, characterPositiony, 32, 32, Color.BLACK);
    Raylib.DrawRectangleRec(hdjhd, Color.GREEN);





    Raylib.EndDrawing();
}
