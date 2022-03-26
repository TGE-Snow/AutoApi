using AiMiYun.Data;
using Malt.Core;
using Microsoft.Extensions.Logging;
using System;

namespace MaltDemoFactoryTestCommon
{
    /// <summary>
    /// 测试基础类(票据为空)
    /// </summary>
    public abstract class TestBase_NullToken
    {
        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns></returns>
        protected virtual IUserToken GetToken()
        {
            return null;
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns></returns>
        protected virtual ILogger GetLogger()
        {
            return null;
        }

        private IBusinessContext _blContext;

        /// <summary>
        /// 业务逻辑上下文
        /// </summary>
        public virtual IBusinessContext BlContext
        {
            get
            {
                if (_blContext == null)
                {
                    _blContext = new BusinessContext(GetToken(), GetLogger())
                    {
                        IsDevelopment = true
                    };
                }
                return _blContext;
            }
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public IDbContext DbContext = new MsSqlContext("");
    }
}