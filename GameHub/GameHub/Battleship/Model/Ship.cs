namespace GameHub.Battleship.Model;

public class Ship
{
    #region Properties
    public int Size { get; set; }
    public int[,] Positions { get; set; }
    #endregion

    #region Constructor
    public Ship(int size)
    {
        Size = size;
        Positions = new int[size,2];
    }
    #endregion

}
