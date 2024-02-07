namespace RabbitStarConsole.Entities
{
    public class Location
    {
        #region Constructor

        public Location(int pointX, int pointY)
        {
            PointX = pointX;
            PointY = pointY;
        }

        #endregion

        #region Properties

        public int PointX { get; set; }
        public int PointY { get; set; }

        #endregion
    }
}
