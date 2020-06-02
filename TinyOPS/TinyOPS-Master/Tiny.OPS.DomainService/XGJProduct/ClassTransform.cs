using Newtonsoft.Json;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Contract.XGJ;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class ClassTransform : ITransform
    {
        private IProductClassDomainService ProductClassDomainService => IoC.Resolve<IProductClassDomainService>();
        private ITeachTimeDomainService TeachTimeDomainService => IoC.Resolve<ITeachTimeDomainService>();

        public void ExecuteTrans(T_EXT_SyncHistory entity)
        {
            var _responses = JsonConvert.DeserializeObject<ReturnResponse<ClassResponse>>(entity.DataJson);

            //优化查询方法,100条查询一次
            List<T_EXT_Class> productClassList = new List<T_EXT_Class>();
            if (_responses.Data.List != null && _responses.Data.List.Count > 0)
            {
                var count = Math.Ceiling(Convert.ToDecimal(_responses.Data.List.Count) / 100);
                for (int i = 0; i < count; i++)
                {
                    string[] classIDs = _responses.Data.List.Select(x => x.ID.ToString()).Skip(i * 100).Take(100).ToArray();
                    productClassList.AddRange(ProductClassDomainService.GetInfoByGuids(entity.FromSystem, classIDs));
                }
            }
            foreach (var response in _responses.Data.List)
            {
                var _class = productClassList.Where(x => x.FromSystem == entity.FromSystem && Guid.Parse(x.ClassID) == response.ID).FirstOrDefault();
                if (_class == null)
                    _class = new T_EXT_Class();
                _class.FromSystem = entity.FromSystem;
                _class.TeachLevelOneOrgID = response.OrgID;
                _class.TeachLevelOneOrgName = response.OrgName;
                _class.CampusID = response.CampusID.ToString();
                _class.TeachNetOrgID = response.AngLiCampusID;
                _class.TeachNetOrgName = response.CampusName;
                _class.TeachOrgFinaUnitEOSID = int.Parse(string.IsNullOrEmpty(response.EOSID) ? "0" : response.EOSID);
                _class.ClassID = response.ID.ToString();
                _class.ClassName = response.Name;
                _class.CourseID = response.ShiftID.ToString();
                _class.ClassYear = response.Year;
                _class.TermID = response.TermID.ToString();
                _class.TermName = response.TermName;
                _class.OpenDate = response.OpenDate;
                _class.CloseDate = response.CloseDate;
                _class.MinStudentAmoun = 0;
                _class.MaxStudentAmoun = response.MaxStudentAmount;
                _class.TotalClassHour = response.CourseTimes;
                _class.TotalClassHourName = response.UnitName;
                _class.ClassMasterUserID = response.AngLiMasterID.ToString();
                _class.ClassMasterUserName = response.MasterUserName;
                _class.ClassStatus = response.Status;
                _class.ProductCreatedDate = response.CreateTime;
                _class.ProductUpdateDate = response.UpdateTime;
                _class.Describe = response.Describe;
                _class.CourseStartTime = response.CourseStartTime;
                _class.CourseEndTime = response.CourseEndTime;
                _class.POCSource = entity.POCSource;
                _class.History = entity;


                if (_class.Id == 0)
                    ProductClassDomainService.Add(_class);
                else
                    ProductClassDomainService.Save(_class);

                TeachTimeDomainService.DeleteTeachTime(_class.Id);

                foreach (var time in response.ClassTimeList)
                {
                    var _teach = new T_EXT_ClassTeachTime();
                    _teach.ProductClass = _class;
                    _teach.ClassID = time.ClassID.ToString();
                    _teach.WeekDay = time.WeekDay;
                    _teach.StartTime = time.StartTime;
                    _teach.EndTime = time.EndTime;
                    _teach.TeacherUserID = time.AngLiTeacherID.HasValue ? time.AngLiTeacherID.Value : Guid.Empty;
                    _teach.TeacherUserName = time.TeacherName;
                    _teach.ClassroomID = time.ClassroomID.ToString();
                    _teach.ClassroomName = time.ClassroomName;
                    _teach.POCSource = entity.POCSource;
                    _teach.History = entity;
                    TeachTimeDomainService.Add(_teach);
                }
            }
        }
    }
}
