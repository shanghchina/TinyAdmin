using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Tiny.Common.FileOperations
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileHelper
    {
        #region 

        #region 操作指定目录下的文件类
        private String StartPath;
        private ArrayList AllFiles;
        private ArrayList AllPaths;
        /// <summary>
        /// 操作指定目录下的文件类
        /// </summary>
        /// <param name="startPathStr">目录</param>
        public FileHelper(String startPathStr)
        {
            StartPath = startPathStr;
            AllPaths = new ArrayList();
            GetAllPaths(new string[] { startPathStr });
            AllPaths.Add(startPathStr);
            AllFiles = GetAllFiles(AllPaths);
        }
        #endregion 操作指定目录下的文件类

        #region 将字符串保存为文件
        /// <summary>
        /// 将字符串保存为文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="txt">字符串</param>
        /// <param name="encoding">编码</param>
        static public void SaveFile(String path, String txt, String encoding = "GB2312")
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path: Path.GetDirectoryName(path));
            }
            using (StreamWriter mStreamWriter = new StreamWriter(path, true, Encoding.GetEncoding(encoding)))
            {
                mStreamWriter.WriteLine(txt);
                mStreamWriter.Close();
            }

        }
        #endregion 将字符串保存为文件

        #region 读取指定路径的文件
        /// <summary>
        /// 读取指定路径的文件
        /// </summary>
        /// <param name="fromPath">路径</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        static public string ReadFile(String fromPath, String encoding = "GB2312")
        {
            if (File.Exists(fromPath))
            {
                String fileStr = String.Empty;
                using (StreamReader reader = new StreamReader(fromPath, Encoding.GetEncoding(encoding)))
                {
                    fileStr = reader.ReadToEnd();
                    reader.Close();
                }
                return fileStr;
            }
            else
                return String.Empty;
        }
        #endregion 读取指定路径的文件

        #region 查找指定目录下所有为此名称的文件
        /// <summary>
        /// 查找指定目录下所有为/包含此名称的文件,找不到返回空字符串
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>路径,找不到返回空字符串</returns>
        public String FintFilePath(String fileName)
        {
            foreach (String path in AllFiles)
            {
                if (Path.GetFileName(path) == fileName)
                {
                    return path;
                }
            }
            return String.Empty;
        }
        /// <summary>
        /// 查找指定目录下所有包含此名称的文件,找不到返回空字符串
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="extension">文件的扩展名如,要加点</param>
        /// <returns>路径,找不到返回空字符串</returns>
        public ArrayList FintFilePath(String fileName, String extension)
        {
            ArrayList paths = new ArrayList();
            if (fileName.Length > 0)
            {
                foreach (String path in AllFiles)
                {
                    var name = Path.GetFileName(path);
                    if (name != null && (name.ToLower().IndexOf(fileName.ToLower(), StringComparison.Ordinal) >= 0 && string.Compare(Path.GetExtension(path), extension, StringComparison.OrdinalIgnoreCase) == 0
                                                           && !paths.Contains(path)))
                    {
                        paths.Add(path);
                    }
                }
            }
            else
            {
                foreach (String path1 in AllFiles)
                {
                    if (String.Compare(Path.GetExtension(path1), extension, StringComparison.OrdinalIgnoreCase) == 0 && !paths.Contains(path1))
                    {
                        paths.Add(path1);
                    }
                }
            }
            return paths;
        }
        #endregion 查找指定目录下所有为此名称的文件

        #region 得到所有子目录/得到目录下的所有文件
        /// <summary>
        /// 得到所有子目录
        /// </summary>
        /// <param name="dirs">目录集</param>
        void GetAllPaths(string[] dirs)
        {
            foreach (String dir in dirs)
            {
                if (dir.EndsWith("_svn"))
                {
                    continue;//排除代码管理器的目录
                }
                String[] inDirs = Directory.GetDirectories(dir);
                GetAllPaths(inDirs);
                AllPaths.Add(dir);
            }
        }
        /// <summary>
         /// 得到目录下的所有文件
         /// </summary>
         /// <param name="allpaths">目录集</param>
         /// <returns>路径</returns>
        ArrayList GetAllFiles(ArrayList allpaths)
        {
            ArrayList allFiels = new ArrayList();
            foreach (String path in allpaths)
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                    allFiels.Add(file);
            }
            return allFiels;
        }
        #endregion 得到所有子目录/得到目录下的所有文件

        #region 获取Dll的运行路径
        /// <summary>
        /// 获取Dll的运行路径
        /// </summary>
        /// <returns></returns>
        static public String GetAssemblyPath()
        {
            String codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            codeBase = codeBase.Substring(8, codeBase.Length - 8);    // 8是 file:// 的长度
            String[] arrSection = codeBase.Split(new char[] { '/' });
            String folderPath = String.Empty;
            for (Int32 i = 0; i < arrSection.Length - 2; i++)
            {
                folderPath += arrSection[i] + "/";
            }
            return folderPath;
        }
        #endregion 获取Dll的运行路径

        #region 得到父目录
        /// <summary>
        /// 得到父目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="cdCount">cd的级数</param>
        /// <returns>父目录</returns>
        static public String pathCD(string path, int cdCount)
        {
            path = path.Replace('\\', '/');
            path = path.TrimEnd('/');
            String[] arrSection = path.Split(new char[] { '/' });
            if (arrSection.Length - cdCount == 0)
            {
                return arrSection[0];
            }
            String folderPath = String.Empty;
            for (Int32 i = 0; i < arrSection.Length - cdCount; i++)
            {
                folderPath += arrSection[i] + "/";
            }
            return folderPath;
        }
        #endregion 得到父目录

        #region 得到两个目录的级数差
        /// <summary>
        /// 得到两个目录的级数差
        /// </summary>
        /// <param name="path1">目录1</param>
        /// <param name="path2">目录2</param>
        /// <returns>../ 的组合</returns>
        static public String PathSeriesDifference(string path1, string path2)
        {
            path1 = path1.Replace('\\', '/');
            path2 = path2.Replace('\\', '/');
            path1 = path1.TrimEnd('/');
            path2 = path2.TrimEnd('/');
            String[] path1S = path1.Split('/');
            String[] path2S = path2.Split('/');
            Int32 difference = Math.Abs(path1S.Length - path2S.Length);
            String differenceStr = String.Empty;
            for (Int32 i = 0; i < difference; i++)
            {
                differenceStr += "../";
            }
            return differenceStr;
        }
        #endregion 得到两个目录的级数差

        /// <summary>
        /// 得到初始化的路径
        /// </summary>
        public string path => StartPath;
        #endregion

        #region 检测指定路径是否存在
        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        /// <returns></returns>
        public static bool IsExist(string path, bool isDirectory)
        {
            return isDirectory ? Directory.Exists(path) : File.Exists(path);
        }
        #endregion 检测指定路径是否存在

        #region 创建目录或文件
        /// <summary>
        /// 创建目录或文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        public static void CreateFiles(string path, bool isDirectory)
        {
            try
            {
                if (!IsExist(path, isDirectory))
                {
                    if (isDirectory)
                        Directory.CreateDirectory(path);
                    else
                    {
                        FileInfo file = new FileInfo(path);
                        FileStream fs = file.Create();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 创建目录或文件

        #region 删除目录或文件
        /// <summary>
        /// 删除目录或文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        public static void DeleteFiles(string path, bool isDirectory)
        {
            try
            {
                if (!IsExist(path, isDirectory))
                {
                    if (isDirectory)
                        Directory.Delete(path);
                    else
                        File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 删除目录或文件

        #region 获取文件名和扩展名
        /// <summary>
        /// 获取文件名和扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 获取文件名不带扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string GetFileNameWithOutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string GetFileExtension(string path)
        {
            return Path.GetExtension(path);
        }
        #endregion 获取文件名和扩展名

    }
}
