using System;
using System.Collections.Generic;

namespace Adventure
{
  public class Scene
  {
    public char[,] map;
    private int dimensions = 20;
    
    ActorModel.Actor actor = new ActorModel.Actor{
          Name = "Hero",
          Health = 100,
          Location = (10,10)
    };
    
    public Scene()
    {
     map = new char[dimensions, dimensions];
     InitializeEmptyMap();
     MapFrame();
     Character();
    }

    private void InitializeEmptyMap()
    {
       for (var i = 0; i < dimensions; i++)
       {
          for (var j = 0; j < dimensions; j++)
          {
             map[i, j] = '*'; 
          }
       }
    }
    
    public void MapFrame()
    {   
       Console.Clear();
       map[actor.Location.row, actor.Location.col] = '&';
       for (var i = 0; i < dimensions; i++)
       {
          for (var j = 0; j < dimensions; j++)
          {
            Console.Write(map[i, j] + " "); 
          }
          Console.WriteLine();
       }
    }

    private void Character()
    {
      while (true)
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        map[actor.Location.row, actor.Location.col] = '*';

        int newRow = actor.Location.row;
        int newCol = actor.Location.col;
          switch (keyInfo.Key)
          {
            case ConsoleKey.UpArrow:
              if (actor.Location.row > 0)
              {
                newRow -= 1;
              }
              break;
            case ConsoleKey.DownArrow:
              if (actor.Location.row < dimensions - 1)
              {
                newRow += 1;
              }
              break;
            case ConsoleKey.LeftArrow:
              if (actor.Location.col > 0)
              {
                newCol -= 1;
              }
              break;
            case ConsoleKey.RightArrow:
              if (actor.Location.col < dimensions - 1)
              {
                newCol += 1;
              }
              break;
            case ConsoleKey.Escape:
              return; // Exit the loop
          }
          actor.Location = (newRow, newCol);

          map[actor.Location.row, actor.Location.col] = '&';
          MapFrame();
      }
    }

  }
}
