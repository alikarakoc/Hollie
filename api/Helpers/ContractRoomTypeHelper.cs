using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class ContractRoomTypeHelper
    {
        public static void AddRoomTypes(int id, List<CRoomTypeList> listRoomTypes, Context context)
        {
            List<RoomType> list = context.RoomTypes.ToList();
            foreach (CRoomTypeList roomtypeFromList in listRoomTypes)
            {
                roomtypeFromList.ListId = id;
                if (list.Any(p => p.Id == roomtypeFromList.RoomTypeId))
                {
                    context.CRoomTypes.Add(roomtypeFromList);
                }
            }
        }

        public static void DeleteRoomTypes(List<CRoomTypeList> listRoomTypes, Context context)
        {
            foreach (CRoomTypeList roomtypeFromList in listRoomTypes)
            {
                context.CRoomTypes.Remove(roomtypeFromList);
            }
        }

    }
}

