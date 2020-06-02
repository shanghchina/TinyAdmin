using AutoMapper;
using Tiny.OPS.Domain;
using Tiny.OPS.Domain.XGJProduct;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceProfiles : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceProfiles()
        {
            CreateMap<T_POC_Product, VM_POC_Product>();
            CreateMap<T_EXT_Course, VM_EXT_Course>();
            
            //CreateMap<SurveyFeedBackData, TMC_SurveyFeedback>();

            //CreateMap<TMC_SurveyFeedbackUser, SurveyFeedbackUserData>();
            //CreateMap<SurveyFeedbackUserData, TMC_SurveyFeedbackUser>();

            //CreateMap<TMC_SurveyFeedbackResponse, TMC_SurveyFeedback>();
            //CreateMap<TMC_SurveyFeedback, TMC_SurveyFeedbackResponse>();
        }
    }
}
