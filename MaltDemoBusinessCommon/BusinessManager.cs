using AiMiYun.Data;
using Malt.Core;
using System;

namespace MaltDemoBusinessCommon
{
    /// <summary>
    /// 业务管理器
    /// </summary>
    public abstract class BusinessManager : BusinessManagerBase
    {
        private IDbContext _dbContext;

        public BusinessManager(IBusinessContext blContext) : base(blContext)
        {
        }

        public long AppKey { get; set; } = 32815620;

        public string AppSecret { get; set; } = "5ee729e079f83042e4bd02656a386fe9";

        public override void MainLogic()
        {
        }

        protected override IDbContext GetDbContext()
        {
            //return new DbContextBase("Data Source=.;Initial Catalog=MaltDemoServer;Integrated Security=True");
            return _dbContext ??= new MsSqlContext("data source = 8.140.112.33,6001;initial catalog = TGE; user id = sa; password=Thegodemperor1;MultipleActiveResultSets=True;Max Pool Size=30000;");
        }

        public decimal ToDecimal(decimal value)
        {
            return (decimal)((int)(value * 100) / 100.00);
        }
    }
}