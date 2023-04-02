

using System;

namespace TurnBaseStrategy.Grid
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int X;
        public int Z;

        public GridPosition(int x,int z)
        {
            X = x;
            Z = z;
        }

        public override string ToString() =>  $"x: {X}; z: {Z}";
        

        public static bool operator ==(GridPosition a,GridPosition b) => a.X == b.X && a.Z == b.Z;

        public static bool operator !=(GridPosition a, GridPosition b) => !(a == b);
        

        public override bool Equals(object obj) => obj is GridPosition position &&
                                                                X == position.X &&
                                                                Z == position.Z;
        
        public override int GetHashCode() => HashCode.Combine(X, Z);

        // IEquatable
        public bool Equals(GridPosition other) => this == other;

        public static GridPosition operator +(GridPosition a, GridPosition b) => new GridPosition(a.X + b.X, a.Z + b.Z);
        public static GridPosition operator -(GridPosition a, GridPosition b) => new GridPosition(a.X - b.X, a.Z - b.Z);
    }
}