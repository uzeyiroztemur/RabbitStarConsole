using RabbitStarConsole.Entities;
using RabbitStarConsole.Enums;

namespace RabbitStarConsole.Abstract
{
    public interface IForest
    {
        /// <summary>
        /// Belirtilen forest objesi ilgili lokasyonda işaretlenir.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="forestObject"></param>
        void SetObject(Location location, ForestObject forestObject);

        /// <summary>
        /// Belirtilen lokasyondaki forest nesnesi kaldırılır.
        /// </summary>
        /// <param name="location"></param>
        void ClearObject(Location location);

        /// <summary>
        /// Engeller random bir şekilde dağıtılır.
        /// </summary>
        void GenerateObstacle();

        int GetSize();
        char[,] GetArea();

        /// <summary>
        /// Console ekranında simulasyonu görüntülemek için kullanılan fonksiyondur.
        /// </summary>
        void ShowArea();
    }
}
