using System;
using System.Collections.Generic;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    public interface ISysConfigService
    {
     

        /// <summary>
        /// 查询参数配置信息
        /// </summary>
        /// <param name="configId">参数配置ID</param>
        /// <returns>参数配置信息</returns>
        public SysConfig selectConfigById(long configId);

       
        /// <summary>
        /// 根据键名查询参数配置信息
        /// </summary>
        /// <param name="configKey">参数键名</param>
        /// <returns>参数键值</returns>
        public string selectConfigByKey(string configKey);

        /// <summary>
        /// 查询参数配置列表
        /// </summary>
        /// <param name="config">参数配置信息</param>
        /// <returns>参数配置集合</returns>
        public List<SysConfig> selectConfigList(SysConfig config);
       
        /// <summary>
        /// 新增参数配置
        /// </summary>
        /// <param name="config">参数配置信息</param>
        /// <returns></returns>
        public int insertConfig(SysConfig config);
        
        /// <summary>
        /// 修改参数配置
        /// </summary>
        /// <param name="config">参数配置信息</param>
        /// <returns></returns>
        public int updateConfig(SysConfig config);

        /// <summary>
        /// 批量删除参数信息
        /// </summary>
        /// <param name="configIds">需要删除的参数ID</param>
        /// <returns></returns>
        public int deleteConfigByIds(long[] configIds);

        /// <summary>
        /// 清空缓存数据
        /// </summary>
        public void clearCache();
    
        /// <summary>
        /// 校验参数键名是否唯一
        /// </summary>
        /// <param name="config">参数信息</param>
        /// <returns></returns>
        public string checkConfigKeyUnique(SysConfig config);
    }
}
