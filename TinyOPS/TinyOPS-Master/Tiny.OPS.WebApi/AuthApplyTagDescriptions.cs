using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Tiny.OPS.WebApi
{
    /// <summary>
    /// swagger中controller的描述
    /// </summary>
    public class AuthApplyTagDescriptions : IDocumentFilter
    {
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            //反射获取Api项目下的Controller信息绑定到Swagger
            var assembly = Assembly.Load("Tiny.OPS.WebApi");
            List<Type> li_controllerTypes = new List<Type>();
            List<Tag> li_tags = new List<Tag>();
            li_controllerTypes.AddRange(assembly.GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)));

            foreach (var controller in li_controllerTypes)
            {
                Tag tag = new Tag { Name = controller.Name.Replace("Controller",""), Description = (controller.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute) == null ? "" : (controller.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute).Description};
                li_tags.Add(tag);
            }
            swaggerDoc.Tags = li_tags;
        }
    }
}
