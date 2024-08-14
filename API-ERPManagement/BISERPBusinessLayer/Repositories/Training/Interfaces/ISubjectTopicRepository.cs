using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ISubjectTopicRepository
    {
        IEnumerable<SubjectTopicEntity> GetAllSubjectTopic();
        int CreateSubjectTopic(SubjectTopicEntity Entity);
        bool UpdateSubjectTopic(SubjectTopicEntity Entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
