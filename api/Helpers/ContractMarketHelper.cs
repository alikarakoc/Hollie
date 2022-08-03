using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class ContractMarketHelper
    {
        public static void AddMarkets(int id, List<CMarketList> listMarkets, Context context)
        {
            List<Market> list = context.Markets.ToList();
            foreach (CMarketList marketFromList in listMarkets)
            {
                marketFromList.ListId = id;
                if (list.Any(p => p.Id == marketFromList.MarketId))
                {
                    context.CMarkets.Add(marketFromList);
                }
            }
        }

        public static void DeleteMarkets(List<CMarketList> listMarkets, Context context)
        {
            foreach (CMarketList marketFromList in listMarkets)
            {
                context.CMarkets.Remove(marketFromList);
            }
        }

    }

}