using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class ContractAgencyHelper
    {
        public static void AddAgencies(int id, List<CAgencyList> listAgencies, Context context)
        {
            List<Agency> list = context.Agencies.ToList();
            foreach (CAgencyList agencyFromList in listAgencies)
            {
   
                agencyFromList.ListId = id;
                if (list.Any(p => p.Id == agencyFromList.AgencyId))
                {
                    context.CAgencies.Add(agencyFromList);
                }
            }
        }

        public static void DeleteAgencies(List<CAgencyList> listAgencies, Context context)
        {
            foreach (CAgencyList agencyFromList in listAgencies)
            {
                context.CAgencies.Remove(agencyFromList);
            }
        }

    }


}





