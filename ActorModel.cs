using System;

namespace Adventure
{
  class ActorModel
  {
      public struct Actor
      {
        public string Name { get; set; }
        public uint Health { get; set; }
        public (int row, int col) Location { get; set; }
      }
  }
}
