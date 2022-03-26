using Malt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaltDemo
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BusinessController : BusinessControllerBase
    {
        /// <summary>
        /// 测试环境
        /// </summary>
        public override bool IsDevelopment => Startup.IsDevelopment;

        /// <summary>
        /// 在业务逻辑上下文创建完成后
        /// </summary>
        /// <param name="businessContext">业务上下文</param>
        protected override void OnBusinessContextCreated(IBusinessContext businessContext)
        {
            base.OnBusinessContextCreated(businessContext);
        }

        protected void ValidationUserToken()
        {
            //检查票据
            if (BlContext.UserToken == null)
            {
                throw new DataValidationException("没有操作权限", BlContext.Logger);
            }
        }
    }
}