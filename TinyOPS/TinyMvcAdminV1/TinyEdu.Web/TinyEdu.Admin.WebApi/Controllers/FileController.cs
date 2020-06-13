﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Util;
using TinyEdu.Util.Extension;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// 文件上传下载
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="fileModule"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        #region 上传单个文件
        [HttpPost]
        public async Task<TData<string>> UploadFile(int fileModule, IFormCollection files)
        {
            TData<string> obj = await FileHelper.UploadFile(fileModule, files.Files);
            return obj;
        }
        #endregion

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileModule"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        #region 删除单个文件
        [HttpPost]
        public TData<string> DeleteFile(int fileModule, string path)
        {
            TData<string> obj = FileHelper.DeleteFile(fileModule, path);
            return obj;
        }
        #endregion

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="delete"></param>
        #region 下载文件
        [HttpGet]
        public void DownloadFile(string fileName, int delete = 1)
        {
            fileName = fileName.ParseToString();
            string filePath = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("文件不存在：" + filePath);
            }
            fileName = Path.GetFileName(fileName);
            FileHelper.DownLoadFile(HttpContext, fileName, filePath);
            if (delete == 1)
            {
                System.IO.File.Delete(filePath);
            }
        }
        #endregion
    }
}