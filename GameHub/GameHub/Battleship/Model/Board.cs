using GameHub.Battleship.Util;

namespace GameHub.Battleship.Model;

public class Board
{
    #region Properties ans Attributes
    private int _boardSize = Constraints.BoardSize;
    public Ship[,] Table { get; set; }
    public bool[,] DiscoveredPositions { get; set; }
    public string[] Rows { get; set; }
    public string[] Columns { get; set; }
    public int ShipsQuantity { get; set; }
    public List<Ship> Ships { get; set; }

    public int[] shipSizes = new int[] { 5, 4, 3, 3, 2, 1, 1 };
    #endregion

    #region Constructor
    public Board()
    {
        Table = new Ship[_boardSize, _boardSize];
        DiscoveredPositions = new bool[_boardSize, _boardSize];
        Ships = new List<Ship>();
        Rows = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        Columns = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        ShipsQuantity = shipSizes.Length;

        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                Table[i, j] = new Ship(0);
                DiscoveredPositions[i, j] = false;
            }
        }

        foreach (int shipSize in shipSizes)
        {
            bool isPlacementSuccess = false;
            while (!isPlacementSuccess)
            {
                isPlacementSuccess = PlaceShipRandomly(shipSize);
            }
        }
    }
    #endregion

    #region Methods
    private bool PlaceShipRandomly(int shipSize)
    {
        Random random = new Random();

        int row = random.Next(0, _boardSize);
        int column = random.Next(0, _boardSize);
        int orientation = random.Next(0, Constraints.MaximumOrientationNumber);

        if (orientation == Constraints.HorizontalOrientationNumber)
        {
            if (column + shipSize > _boardSize)
            {
                return false;
            }

            for (int j = column; j < column + shipSize; j++)
            {
                if (Table[row, j].Size != 0)
                {
                    return false;
                }
            }

            Ship ship = new Ship(shipSize);

            for (int j = column, r = 0; j < column + shipSize; j++, r++)
            {
                Table[row, j] = ship;
                ship.Positions[r, 0] = row;
                ship.Positions[r, 1] = j;
            }

            Ships.Add(ship);
        }
        else
        {
            if (row + shipSize > _boardSize)
            {
                return false;
            }

            for (int i = row; i < row + shipSize; i++)
            {
                if (Table[i, column].Size != 0)
                {
                    return false;
                }
            }

            Ship ship = new Ship(shipSize);

            for (int i = row, r = 0; i < row + shipSize; i++, r++)
            {
                Table[i, column] = ship;
                ship.Positions[r, 0] = i;
                ship.Positions[r, 1] = column;
            }

            Ships.Add(ship);
        }
        return true;
    }
    #endregion
}
