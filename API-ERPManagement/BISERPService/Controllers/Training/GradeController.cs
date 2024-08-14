using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Entities.Training;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/grade")]
    public class GradeController : ApiController
    {
        IGradeRepository _Grades;
        private static readonly ILogger _loggger = Logger.Register(typeof(GradeController));

        public GradeController(IGradeRepository Grades)
        {
            _Grades = Grades;
        }

        [Route("getallgrades")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllGrades()
        {
            List<GradeEntity> grade = new List<GradeEntity>();
            TryCatch.Run(() =>
            {
                var list = _Grades.GetAllGrades();
                if (list != null && list.Count() > 0)
                    grade = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllGrades method of GradeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grade.Any())
                return Ok(grade);
            else
                return Ok(grade);
        }
    }
}
