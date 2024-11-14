namespace Day22Puzzle02
{
    internal class CubeFace
    {
        public const int FaceSize = 50;
        private static int Counter = 0;
        public int X { get; set; }
        public int Y { get; set; }
        public List<Tile> Tiles { get; } = new List<Tile>();
        public int Id { get; } = Counter++;

        public CubeFace? FaceToTheLeft { get; set; }
        public CubeFace? FaceToTheRight { get; set; }
        public CubeFace? FaceDown { get; set; }
        public CubeFace? FaceUp { get; set; }

        public Dictionary<CubeFace, int> OrientationChanges { get; } = new Dictionary<CubeFace, int>();

        private IEnumerable<Tile> GetUpTiles() => Tiles.Where(t => t.Y == (FaceSize * Y) + 1);
        private IEnumerable<Tile> GetRightTiles() => Tiles.Where(t => t.X == FaceSize * (X + 1));
        private IEnumerable<Tile> GetDownTiles() => Tiles.Where(t => t.Y == FaceSize * (Y + 1));
        private IEnumerable<Tile> GetLeftTiles() => Tiles.Where(t => t.X == (FaceSize * X) + 1);

        public void SetFaceUp(CubeFace other, int orientationchange)
        {
            FaceUp = other;
            Tile[] myTiles = GetUpTiles().OrderBy(t => t.X).ToArray();
            Tile[]? otherTiles = null;
            Action<Tile, Tile> setTheirs = (theirs, ours) => { };
            switch (orientationchange)
            {
                case 0:
                    other.FaceDown = this;
                    otherTiles = other.GetDownTiles().OrderBy(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Down = ours; };
                    break;
                case 1:
                    other.FaceToTheLeft = this;
                    otherTiles = other.GetLeftTiles().OrderBy(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Left = ours; };
                    break;
                case 2:
                    other.FaceUp = this;
                    otherTiles = other.GetUpTiles().OrderByDescending(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Up = ours; };
                    break;
                case 3:
                    other.FaceToTheRight = this;
                    otherTiles = other.GetRightTiles().OrderByDescending(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Right = ours; };
                    break;
                default:
                    throw new InvalidOperationException();
            }
            for(int i = 0; i < FaceSize; i++)
            {
                Tile mine = myTiles[i];
                Tile theirs = otherTiles[i];
                mine.Up = theirs;
                setTheirs(theirs, mine);
            }
            OrientationChanges.Add(other, orientationchange);
            other.OrientationChanges.Add(this, (4 - orientationchange) % 4);
        }

        public void SetFaceToTheRight(CubeFace other, int orientationchange)
        {
            FaceToTheRight = other;
            Tile[] myTiles = GetRightTiles().OrderBy(t => t.Y).ToArray();
            Tile[]? otherTiles = null;
            Action<Tile, Tile> setTheirs = (theirs, ours) => { };
            switch (orientationchange)
            {
                case 0:
                    other.FaceToTheLeft = this;
                    otherTiles = other.GetLeftTiles().OrderBy(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Left = ours; };
                    break;
                case 1:
                    other.FaceUp = this;
                    otherTiles = other.GetUpTiles().OrderByDescending(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Up = ours; };
                    break;
                case 2:
                    other.FaceToTheRight = this;
                    otherTiles = other.GetRightTiles().OrderByDescending(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Right = ours; };
                    break;
                case 3:
                    other.FaceDown = this;
                    otherTiles = other.GetDownTiles().OrderBy(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Down = ours; };
                    break;
                default:
                    throw new InvalidOperationException();
            }
            for (int i = 0; i < FaceSize; i++)
            {
                Tile mine = myTiles[i];
                Tile theirs = otherTiles[i];
                mine.Right = theirs;
                setTheirs(theirs, mine);
            }
            OrientationChanges.Add(other, orientationchange);
            other.OrientationChanges.Add(this, (4 - orientationchange) % 4);
        }

        public void SetFaceDown(CubeFace other, int orientationchange)
        {
            FaceDown = other;
            Tile[] myTiles = GetDownTiles().OrderBy(t => t.Y).ToArray();
            Tile[]? otherTiles = null;
            Action<Tile, Tile> setTheirs = (theirs, ours) => { };
            switch (orientationchange)
            {
                case 0:
                    other.FaceUp = this;
                    otherTiles = other.GetUpTiles().OrderBy(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Up = ours; };
                    break;
                case 1:
                    other.FaceToTheRight = this;
                    otherTiles = other.GetRightTiles().OrderBy(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Right = ours; };
                    break;
                case 2:
                    other.FaceDown = this;
                    otherTiles = other.GetDownTiles().OrderByDescending(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Down = ours; };
                    break;
                case 3:
                    other.FaceToTheLeft = this;
                    otherTiles = other.GetLeftTiles().OrderByDescending(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Left = ours; };
                    break;
                default:
                    throw new InvalidOperationException();
            }
            for (int i = 0; i < FaceSize; i++)
            {
                Tile mine = myTiles[i];
                Tile theirs = otherTiles[i];
                mine.Down = theirs;
                setTheirs(theirs, mine);
            }
            OrientationChanges.Add(other, orientationchange);
            other.OrientationChanges.Add(this, (4 - orientationchange) % 4);
        }

        public void SetFaceToTheLeft(CubeFace other, int orientationchange)
        {
            FaceToTheLeft = other;
            Tile[] myTiles = GetLeftTiles().OrderBy(t => t.Y).ToArray();
            Tile[]? otherTiles = null;
            Action<Tile, Tile> setTheirs = (theirs, ours) => { };
            switch (orientationchange)
            {
                case 0:
                    other.FaceToTheRight = this;
                    otherTiles = other.GetRightTiles().OrderBy(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Right = ours; };
                    break;
                case 1:
                    other.FaceDown = this;
                    otherTiles = other.GetDownTiles().OrderByDescending(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Down = ours; };
                    break;
                case 2:
                    other.FaceToTheLeft = this;
                    otherTiles = other.GetLeftTiles().OrderByDescending(t => t.Y).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Left = ours; };
                    break;
                case 3:
                    other.FaceUp = this;
                    otherTiles = other.GetUpTiles().OrderBy(t => t.X).ToArray();
                    setTheirs = (theirs, ours) => { theirs.Up = ours; };
                    break;
                default:
                    throw new InvalidOperationException();
            }
            for (int i = 0; i < FaceSize; i++)
            {
                Tile mine = myTiles[i];
                Tile theirs = otherTiles[i];
                mine.Left = theirs;
                setTheirs(theirs, mine);
            }
            OrientationChanges.Add(other, orientationchange);
            other.OrientationChanges.Add(this, (4 - orientationchange) % 4);
        }
    }
}
