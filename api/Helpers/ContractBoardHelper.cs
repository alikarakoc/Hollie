using Application.Concrete;
using DataAccess.Concrate;
using System.Collections.Generic;
using System.Linq;



namespace api.Helpers
{
    public class ContractBoardHelper
    {
        public static void AddBoards(int id , List<CBoardList> listBoards,Context context)
        {
            List<Board> list = context.Boards.ToList();
            foreach (CBoardList boardFromList in listBoards)
            {
                boardFromList.ListId = id;
                if (list.Any(p => p.Id == boardFromList.BoardId))
                {
                    context.CBoards.Add(boardFromList);
                }
            }
        }

        public static void DeleteBoards ( List<CBoardList> listBoards , Context context)
        {
            foreach (CBoardList boardFromList in listBoards)
            {
                context.CBoards.Remove(boardFromList);
            }

        }
    }
}
