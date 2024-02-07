using RabbitStarConsole.Entities;
using RabbitStarConsole.Enums;

namespace RabbitStarConsole.Abstract
{
    public interface IRabbit
    {
        /// <summary>
        /// Lokasyon kontrolünü yapan yardımcı fonksiyondur.
        /// </summary>
        /// <returns></returns>
        bool IsOutOfBorder();

        /// <summary>
        /// Verilen lokasyon bilgisi için dikenli tel kontrolünü yapan fonksiyondur.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        bool IsWireBarbed(Location location);

        /// <summary>
        /// Verilen lokasyon bilgisi için tel çit kontrolünü yapan fonksiyondur.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        bool IsWireFence(Location location);

        /// <summary>
        /// Tavşan öldümü bilgisini döner
        /// </summary>
        /// <returns></returns>
        bool IsDead();

        /// <summary>
        /// Tavşan deliğe girdimi bilgisini döner
        /// </summary>
        /// <returns></returns>
        bool IsHole();

        Location GetLocation();
        Direction GetDirection();

        void SetLocation(Location location, Direction direction = Direction.North);
        void SetDirection(Direction direction);

        void ShowLocationInfo();        
    }
}
