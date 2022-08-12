using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class AgencyMarketHelper
    {
        public static void AddMarkets(int id, List<MarketListA> listMarkets, Context context)
        {
            List<Market> list = context.Markets.ToList();
            if (listMarkets != null)
            {
                foreach (MarketListA marketFromList in listMarkets)
                {
                    marketFromList.ListId = id;
                    if (list.Any(p => p.Id == marketFromList.MarketId))
                    {
                        context.AMarkets.Add(marketFromList);
                    }
                }

            }
        }

        public static void DeleteMarkets(List<MarketListA> listMarkets, Context context)
        {
            foreach (MarketListA marketFromList in listMarkets)
            {
                context.AMarkets.Remove(marketFromList);
            }
        }

    }

}