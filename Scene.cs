using System;
using System.Collections.Generic;
using System.Numerics;

namespace Adventure
{
  public class Scene
  {
    private char[,]? map;
    private int dimensions = 20;
    private Generation gen;
    
    ActorModel.Actor player = new ActorModel.Actor{
          Name = "Hero",
          Health = 100,
          Location = new Vector2(10, 10)
    };

    ActorModel.Actor enemy = new ActorModel.Actor{
          Name = "Enemy",
          Health = 20,
          Location = new Vector2(1, 1)
    };
    
    public Scene()
    {
     gen = new Generation();
     map = gen.TileSet();
     MapFrame();
    }

    private void MapFrame()
    {   
      Console.Clear();
      map[(int)player.Location.X, (int)player.Location.Y] = '&';
      map[(int)enemy.Location.X, (int)enemy.Location.Y] = 'E';
      Console.WriteLine($"Player health: {player.Health}");
      for (var i = 0; i < dimensions; i++)
      {
         for (var j = 0; j < dimensions; j++)
         {
           Console.Write(map[i, j] + " ");
         }
         Console.WriteLine();
      }    
    }

    public void Turn()
    {
      while (!PlayerTurn())
      {
        if(EnemyTurn() != null)
        // {
          // if (enemy.Location == player.Location) This will be for the fight system
        // };
        MapFrame();
        
      }
    }
// TODO: Make proper collisions, preferably here, i'd guess do a bool that if its collidable then dont let the player or enemy pass.

    private bool PlayerTurn()
    {   
      bool Exit = false;
      ConsoleKeyInfo keyInfo = Console.ReadKey(true);
      map[(int)player.Location.X, (int)player.Location.Y] = ' ';

      int newRow = (int)player.Location.X;
      int newCol = (int)player.Location.Y;
      switch (keyInfo.Key)
      {
      case ConsoleKey.UpArrow:
        if (newRow > 0)
        {
          newRow -= 1;
        }
        break;
      case ConsoleKey.DownArrow:
        if (newRow < dimensions - 1)
        {
          newRow += 1;
        }
        break;
      case ConsoleKey.LeftArrow:
          if (newCol > 0)
          {
            newCol -= 1;
          }
          break;
        case ConsoleKey.RightArrow:
          if (newCol < dimensions - 1)
          {
            newCol += 1;
          }
          break;
        case ConsoleKey.Escape:
          Exit = true;
          break;
        }
        if (newRow >= 0 && newRow < dimensions && newCol >= 0 && newCol < dimensions && map[newRow, newCol] != '*')
        {
          player.Location = new Vector2(newRow, newCol);
        }  
        map[(int)player.Location.X, (int)player.Location.Y] = '&';
        return Exit;
    } 
    
    private Vector2? EnemyTurn()
    {
      var nextStep = EnemyPathFinding(enemy.Location, player.Location);
            if (nextStep != null)
            {
                map[(int)enemy.Location.X, (int)enemy.Location.Y] = ' '; // Clear previous enemy position
                enemy.Location = nextStep.Value;
                map[(int)enemy.Location.X, (int)enemy.Location.Y] = 'E'; // Update enemy position
            }
      return nextStep;
    }

    private Vector2? EnemyPathFinding(Vector2 start, Vector2 target)
    {
      var queue = new Queue<(Vector2 Position, Vector2? Parent)>();
      var visited = new bool[dimensions, dimensions];
      var directions = new List<Vector2>
      {
          new Vector2(-1, 0), // up
          new Vector2(1, 0),  // down
          new Vector2(0, -1), // left
          new Vector2(0, 1),   // right
      };

      queue.Enqueue((start, null));
      visited[(int)start.X, (int)start.Y] = true;

      var parentMap = new Dictionary<Vector2, Vector2>();
      parentMap[start] = start;
      while (queue.Count > 0)
      {
      var (current, parent) = queue.Dequeue();

        if (current == target)
        {
          var step = current;
          while (parentMap[step] != start)
          {
            step = parentMap[step];
          }
          return step;
        }

          foreach (var direction in directions)
          {
            var next = current + direction;
            int nextX = (int)next.X;
            int nextY = (int)next.Y;

            if (nextX >= 0 && nextX < dimensions && nextY >= 0 && nextY < dimensions &&
                !visited[nextX, nextY] && map[nextX, nextY] != '*')
            {
              queue.Enqueue((next, current));
              visited[nextX, nextY] = true;
              parentMap[next] = current;
            }
          }
      }
      return null;
    }
  }
}
