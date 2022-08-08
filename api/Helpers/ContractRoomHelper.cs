using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;

namespace api.Helpers
{
    public class ContractRoomHelper
    {
        public static void AddRooms(int id, List<CRoomList> listRoom, Context context)
        {
            List<Room> list = context.Rooms.ToList();
            foreach (CRoomList roomFromList in listRoom)
            {
                roomFromList.ListId = id;
                if (list.Any(p => p.Id == roomFromList.RoomId))
                {
                    context.CRooms.Add(roomFromList);
                }
            }
        }

        public static void DeleteRooms(List<CRoomList> listRoom, Context context)
        {
            foreach (CRoomList roomFromList in listRoom)
            {
                context.CRooms.Remove(roomFromList);
            }
        }

    }
}

