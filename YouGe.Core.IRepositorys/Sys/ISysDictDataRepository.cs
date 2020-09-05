using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
    public interface ISysDictDataRepository : IRepository<SysDictData, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictData"></param>
        /// <returns></returns>
        List<SysDictData> selectDictDataList(SysDictData dictData);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictCode"></param>
        /// <returns></returns>
        object selectDictDataById(long dictCode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        int insertDictData(SysDictData dict);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        int updateDictData(SysDictData dict);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictCodes"></param>
        /// <returns></returns>
        int deleteDictDataByIds(long[] dictCodes);
    }
}
