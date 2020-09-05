using System;
using System.Collections.Generic;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    public interface ISysDictDataService
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
