﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Business.OrganizationManage;
using TinyEdu.Entity.OrganizationManage;
using TinyEdu.Model.Param;
using TinyEdu.Model.Param.OrganizationManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// 新闻
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class NewsController : ControllerBase
    {
        private NewsBLL newsBLL = new NewsBLL();

        #region 获取数据
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TData<List<NewsEntity>>> GetPageList([FromQuery] NewsListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<NewsEntity>> obj = await newsBLL.GetPageList(param, pagination);
            return obj;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TData<List<NewsEntity>>> GetPageContentList([FromQuery]NewsListParam param, [FromQuery]Pagination pagination)
        {
            TData<List<NewsEntity>> obj = await newsBLL.GetPageContentList(param, pagination);
            return obj;
        }

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TData<NewsEntity>> GetForm([FromQuery]long id)
        {
            TData<NewsEntity> obj = await newsBLL.GetEntity(id);
            return obj;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<string>> SaveForm([FromBody]NewsEntity entity)
        {
            TData<string> obj = await newsBLL.SaveForm(entity);
            return obj;
        }

        /// <summary>
        /// SaveViewTimes
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<string>> SaveViewTimes([FromBody]IdParam param)
        {
            TData<string> obj = null;
            TData<NewsEntity> objNews = await newsBLL.GetEntity(param.Id.Value);
            NewsEntity newsEntity = new NewsEntity();
            if (objNews.Result != null)
            {
                newsEntity.Id = param.Id.Value;
                newsEntity.ViewTimes = objNews.Result.ViewTimes;
                newsEntity.ViewTimes++;
                obj = await newsBLL.SaveForm(newsEntity);
            }
            else
            {
                obj = new TData<string>();
                obj.Message = "文章不存在";
            }
            return obj;
        }
        #endregion
    }
}