namespace Adventure
{
      
  class Generation  
  {
    public char[,]? map;
    private int dimensions = 20;
    private static Random rand = new Random();
    
    public Generation()
    {
      
    }

    public char[,] TileSet()
    {
      map = new char[dimensions, dimensions];
      map = InitializeEmptyMap(map);

      for (var i = 0; i < dimensions; i++)
      {
        for (var j = 0; j < dimensions; j++)
        {
          
          var cell = map[i, j];
          
          if (cell != '*' && i != 0 && i != dimensions - 1 && j != 0 && j != dimensions - 1)
          {
            if (rand.Next(5) == 0)
            {
              map[i, j] = '*';
            }
          }
        }
      }
      return map;
    }

    private char[,] InitializeEmptyMap(char[,] map)
    {
       for (var i = 0; i < dimensions; i++)
       {
          for (var j = 0; j < dimensions; j++)
          {
            if (i == 0 || i == dimensions - 1 || j == 0 || j == dimensions - 1)
            {
             map[i, j] = '*'; 
            }
            else map[i, j] = ' ';
          }
       }
       return map;
    }

  }
}
