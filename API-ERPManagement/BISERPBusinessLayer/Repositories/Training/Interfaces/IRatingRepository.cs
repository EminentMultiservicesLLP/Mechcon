using BISERPBusinessLayer.Entities.Training;
using System.Collections.Generic;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IRatingRepository
    {
        IEnumerable<RatingEntity> GetAllRating();
        int CreateRating(RatingEntity entity);
        bool UpdateRating(RatingEntity entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int id); 
    }
}
