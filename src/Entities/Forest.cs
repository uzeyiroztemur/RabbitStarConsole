namespace RabbitStarConsole.Entities
{
    public class Forest
    {
        public int Size { get; set; }

        /// <summary>
        /// Ormanın büyüklüğe göre koordinasyonların tutulduğu arraydir.
        /// </summary>
        public char[,] Area { get; set; }
    }
}
