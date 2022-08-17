using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class ContractRoomTypeHelper
    {
        public static void AddRoomTypes(int id, List<CRoomTypeList> listRoomType, Context context)
        {
            List<RoomType> list = context.RoomTypes.ToList();
            foreach (CRoomTypeList roomTypeFromList in listRoomType)
            {
                roomTypeFromList.ListId = id;
                if (list.Any(p => p.Id == roomTypeFromList.RoomTypeId))
                {
                    context.CRoomTypes.Add(roomTypeFromList);
                }
            }
        }

        public static void DeleteRoomTypes(List<CRoomTypeList> listRoomType, Context context)
        {
            foreach (CRoomTypeList roomTypeFromList in listRoomType)
            {
                context.CRoomTypes.Remove(roomTypeFromList);
            }
        }

    }
}

