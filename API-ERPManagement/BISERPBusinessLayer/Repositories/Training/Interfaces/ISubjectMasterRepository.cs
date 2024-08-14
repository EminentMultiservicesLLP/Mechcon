using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface ISubjectMasterRepository
    {
        IEnumerable<SubjectEntity> GetAllSubject();
        int CreateSubject(SubjectEntity Entity);
        bool UpdateSubject(SubjectEntity Entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int Id);
    }
}
