using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<RoomEntity> GetAllRoom();
        int CreateRoom(RoomEntity Entity);
        bool UpdateRoom(RoomEntity Entity);
        IEnumerable<RoomEntity> GetActiveRoom(int UserId);
        IEnumerable<RoomEntity> GetFloorRoom(int FloorId);
    }
}
