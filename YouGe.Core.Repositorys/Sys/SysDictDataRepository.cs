using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IRepositorys.Sys;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Interface.IDbContexts;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons.Helper;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Models.System;
using YouGe.Core.Common.Helper;
using System.Threading.Tasks;
using YouGe.Core.DbContexts;
using System.Linq.Expressions;
using YouGe.Core.Commons;
using MySql.Data.MySqlClient;
using System.Linq;

namespace YouGe.Core.Repositorys.Sys
{
    public class SysDictDataRepository : BaseRepository<SysDictData, int>, ISysDictDataRepository
    {
        private YouGeDbContextOption option { get; set; }
        public IYouGeDbContext _dataContext;
        public SysDictDataRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            _dataContext = dbContext;
            option = (YouGeDbContextOption)DbContext.Option;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictData"></param>
        /// <returns></returns>
        public List<SysDictData> selectDictDataList(SysDictData dictData)
        {
            Expression<Func<SysDictData, bool>> express = i => 1 == 1;
            if(!string.IsNullOrEmpty(dictData.DictType))
            {
                express = express.AndAlso(e => e.DictType == dictData.DictType);
            }
            if (!string.IsNullOrEmpty(dictData.DictLabel))
            {
                express = express.AndAlso(e => e.DictLabel == dictData.DictLabel);
            }
            if (!string.IsNullOrEmpty(dictData.status.ToString()))
            {
                express = express.AndAlso(e => e.status == dictData.status);
            }
            return (List<SysDictData>)this.Get(express);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictCode"></param>
        /// <returns></returns>
        public object selectDictDataById(long dictCode)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public int insertDictData(SysDictData dict)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public int updateDictData(SysDictData dict)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictCodes"></param>
        /// <returns></returns>
        public int deleteDictDataByIds(long[] dictCodes)
        {
            throw new NotImplementedException();
        }
    }
}
