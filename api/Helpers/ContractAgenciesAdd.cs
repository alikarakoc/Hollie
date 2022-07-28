using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class ContractAgenciesAdd
    {
        public static void AddAgencies(int id, List<CAgencyList> listAgencies, Context context)
        {
            List<Agency> list = context.Agencies.ToList();
            foreach (CAgencyList agencyFromList in listAgencies)
            {
                var c = agencyFromList;
                agencyFromList.ListId = id;
                if (list.Any(p => p.Id == agencyFromList.AgencyId))
                {
                    context.CAgencies.Add(agencyFromList);
                }
            }
        }

    }


}






/*
  //private void AddContractAgencies(int id, List<CAgencyList> list)
        //{
        //    foreach (CAgencyList agencyFromList in list)
        //    {
        //        agencyFromList.ListId = id;
        //        if (list.Any(p => p.Id == agencyFromList.AgencyId))
        //        {
        //            _context.CAgencies.Add(agencyFromList);

        //        }
        //    }
        //    _context.SaveChanges();
        //}
 */
