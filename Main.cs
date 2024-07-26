using System;

namespace Adventure
{
  class Control
  {
    public Scene currentScene;
    private Control()
    {
      currentScene = new Scene();
    }

    static void Main()
    {
      Control ctr = new Control();
      ctr.Game();
    }

    private void Game()
    {
      currentScene.Turn();
    }
  }
}
