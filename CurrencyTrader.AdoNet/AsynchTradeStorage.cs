using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTrader.Contracts;

namespace CurrencyTrader.AdoNet
{
    // used to apply the decorator pattern by using an asynchronous decorator.
    public class AsynchTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;
        private ITradeStorage SynchTradeStorage;

        public AsynchTradeStorage(ILogger logger)
        {
            this.logger = logger;
            SynchTradeStorage = new AdoNetTradeStorage(logger);
        }

        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Starting synch trade storage.");
            //SynchTradeStorage.Persist(trades);
            Task.Run(() => SynchTradeStorage.Persist(trades)); // this will run the trade storage on a different thread.
        }
    }
}
