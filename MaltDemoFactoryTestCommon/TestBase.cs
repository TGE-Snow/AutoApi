using Malt.Core;
using MaltDemoModelCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaltDemoFactoryTestCommon
{
    /// <summary>
    /// 测试基础类
    /// </summary>
    public abstract class TestBase : TestBase_NullToken
    {
        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns></returns>
        protected override IUserToken GetToken()
        {
            IUserToken userToken = new UserToken();
            return userToken;
        }
    }
}