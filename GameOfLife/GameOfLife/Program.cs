namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var gameEngine = new GameEngine())
            {
                gameEngine.Start();
            }

        }

    }
}
