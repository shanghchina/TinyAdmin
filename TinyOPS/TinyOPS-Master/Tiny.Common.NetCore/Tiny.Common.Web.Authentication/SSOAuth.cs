using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.Common.Web.Authentication
{
    /// <summary>
    /// 认证凭据内容
    /// </summary>
    [Serializable]
    public class SSOAuth
    {
        private Guid _LoginId;
        private Dictionary<string, string> _userDictionary;
        private int _ttl;
        private DateTime _expires;

        public Guid LoginId
        {
            get
            {
                return _LoginId;

            }
            set { _LoginId = value; }
        }
        /// <summary>
        /// 用户信息
        /// </summary>
        public Dictionary<string, string> UserDictionary
        {
            get
            {
                return _userDictionary;
            }
            set { _userDictionary = value; }
        }
        /// <summary>
        /// 有效时长
        /// </summary>
        public int Ttl
        {
            get { return _ttl; }
            set { _ttl = value; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires
        {
            get { return _expires; }
            set { _expires = value; }
        }
        //
    }
}
