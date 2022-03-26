using AiMiYun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaltDemoBusinessCommon
{
    public class DbContextBase : MsSqlContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public DbContextBase(string connectionString) : base(connectionString) { }
    }
}
