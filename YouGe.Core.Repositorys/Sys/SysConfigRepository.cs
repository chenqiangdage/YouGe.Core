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

namespace YouGe.Core.Repositorys.Sys
{
    public class SysConfigRepository : BaseRepository<SysConfig, int>, ISysConfigRepository
    {
        private YouGeDbContextOption option { get; set; }
        public SysConfigRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            option = (YouGeDbContextOption)DbContext.Option;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public SysConfig selectConfigById(long configId)
        {
           return  this.GetSingle((int)configId); 
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public SysConfig selectConfigByKey(string configKey)
        {
            return this.GetSingleOrDefault(u => u.ConfigKey.Contains(configKey));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public List<SysConfig> selectConfigList(SysConfig config)
        {
            Expression<Func<SysConfig, bool>> express = i => 1 == 1;
            if(!string.IsNullOrEmpty(config.ConfigName))
            {
               express =  express.AndAlso(e => e.ConfigName.Contains(config.ConfigName));
            }
            if (!string.IsNullOrEmpty(config.ConfigType))
            {
                express = express.AndAlso(e => e.ConfigType == config.ConfigType);
            }
            if (!string.IsNullOrEmpty(config.ConfigKey))
            {
                express = express.AndAlso(e => e.ConfigKey == config.ConfigKey);
            }
            if (config.beginTime.HasValue)
            {
                express = express.AndAlso(e => e.CreateTime >= config.beginTime);
            }
            if (config.endTime.HasValue)
            {
                express = express.AndAlso(e => e.CreateTime <= config.endTime);
            }
            return (List<SysConfig>)this.Get(express);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int insertConfig(SysConfig config)
        {
            int row = this.Add(config);            
            if (row > 0)
            {
                YouGeRedisHelper.Set(config.ConfigKey, config.ConfigValue);            
            }
            return row;
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int updateConfig(SysConfig config)
        {
            int row = this.Edit(config);
            
            if (row > 0)
            {
                YouGeRedisHelper.Set(config.ConfigKey, config.ConfigValue);
            }
            return row;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configIds"></param>
        /// <returns></returns>
        public int deleteConfigByIds(long[] configIds)
        {
            int row = 0,count=0;
            string[] keys = new string[configIds.Length]; 
            for(int i = 0;i<configIds.Length;i++)
            {
              row =   this.Delete((int)configIds[i]);
                count = count + row;
                keys[i] = configIds[i].ToString();
            }
            
           
            if (count > 0)
            {
                YouGeRedisHelper.Del(keys);
              //  List<String> keys = YouGeRedisHelper.get(YouGeSystemConst.SYS_CONFIG_KEY + "*");
               // redisCache.deleteObject(keys);
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        public void clearCache()
        {
            string keys =  YouGeSystemConst.SYS_CONFIG_KEY + "*";
            YouGeRedisHelper.Del(keys);
           // throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public string checkConfigKeyUnique(SysConfig config)
        {
            long configId = config.Id;

            SysConfig info = this.GetSingleOrDefault(u => u.ConfigKey == config.ConfigKey);            
            if (info!=null && info.Id != configId)
            {
                return YouGeUserConstants.NOT_UNIQUE;
            }
            return YouGeUserConstants.UNIQUE;
            throw new NotImplementedException();
        }

    }
}
