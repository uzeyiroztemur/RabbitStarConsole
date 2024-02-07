using RabbitStarConsole.Enums;

namespace RabbitStarConsole.Abstract
{
    public interface IRabbitManager
    {
        /// <summary>
        /// Belirtilen senaryo için tavşanın hareket etmesini sağlar.
        /// </summary>
        /// <param name="scenario"></param>
        /// <returns></returns>
        Result ExecuteOperation(string scenario);        
    }
}