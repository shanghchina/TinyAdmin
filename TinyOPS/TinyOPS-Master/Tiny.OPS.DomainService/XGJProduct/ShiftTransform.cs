using Newtonsoft.Json;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Contract.XGJ;
using Tiny.OPS.Domain.XGJProduct;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class ShiftTransform : ITransform
    {
        private IProductCourseDomainService ProductCourseDomainService => IoC.Resolve<IProductCourseDomainService>();
        private ICampusRangeDomainService CampusRangeDomainService => IoC.Resolve<ICampusRangeDomainService>();

        public void ExecuteTrans(T_EXT_SyncHistory entity)
        {
            var _responses = JsonConvert.DeserializeObject<ReturnResponse<ShiftResponse>>(entity.DataJson);
            foreach (var response in _responses.Data.List)
            {
                var _course = ProductCourseDomainService.GetInfoByGuid(entity.FromSystem, response.ID.ToString());
                if (_course == null)
                    _course = new T_EXT_Course();
                _course.FromSystem = entity.FromSystem;
                _course.TeachLevelOneOrgID = response.OrgID;
                _course.TeachLevelOneOrgName = response.OrgName;
                _course.CourseID = response.ID.ToString();
                _course.CourseName = response.Name;
                _course.ProductTypeOneID = response.ProductTypeID.Split('|').FirstOrDefault();
                _course.ProductTypeTwoID = response.ProductTypeID.Split('|').LastOrDefault();
                _course.CourseYear = response.Year;
                _course.TermID = response.TermID.ToString();
                _course.TermName = response.TermName;
                _course.GradeID = response.GradeID.ToString();
                _course.GradeName = response.GradeName;
                _course.ClassTypeID = response.ClassType.ToString();
                _course.ClassTypeName = response.ClassTypeName;
                _course.FlagID = response.Flag.ToString();
                _course.FlagName = response.FlagName;
                _course.SubjectID = response.SubjectID.ToString();
                _course.SubjectName = response.SubjectName;
                _course.CategoryID = response.CategoryID.ToString();
                _course.CategoryName = response.CategoryName;
                _course.TotalClassHour = response.CourseTimes;
                _course.TotalClassHourName = response.UnitName;
                _course.FeeUnitPrice = response.UnitPrice;
                _course.FeeUnitPriceName = response.UnitPriceName;
                _course.CourseStatus = response.Status;
                _course.ProductCreatedDate = response.CreateTime;
                _course.ProductUpdateDate = response.UpdateTime;
                _course.Describe = response.Describe;
                _course.POCSource = entity.POCSource;
                _course.History = entity;

                if (_course.Id == 0)
                {
                    _course.ExtractStatus = 1000;
                    ProductCourseDomainService.Add(_course);
                }
                else
                {
                    ProductCourseDomainService.Save(_course);
                }

                CampusRangeDomainService.DeleteCampusRange(_course.Id);

                foreach (var permit in response.PermitList)
                {
                    var _range = new T_EXT_CourseRange();
                    _range.ProductCourse = _course;
                    _range.CourseID = permit.ShiftID.ToString();
                    _range.CampusID = permit.CampusID.ToString();
                    _range.TeachNetOrgID = permit.AngLiCampusID;
                    _range.TeachNetOrgName = permit.CampusName;
                    _range.TeachOrgFinaUnitEOSID = int.Parse(string.IsNullOrEmpty(permit.ESOID) ? "0" : permit.ESOID);
                    _range.FeeUnitPrice = permit.UnitPrice;
                    _range.FeePriceName = response.UnitPriceName;
                    _range.POCSource = entity.POCSource;
                    _range.History = entity;
                    CampusRangeDomainService.Add(_range);
                }
            }
        }
    }
}
