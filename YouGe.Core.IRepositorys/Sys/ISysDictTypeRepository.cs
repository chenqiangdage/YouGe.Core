using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
    public interface ISysDictTypeRepository : IRepository<SysDictType, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        object selectDictDataByType(string dictType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        List<SysDictType> selectDictTypeList(SysDictType dictType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        object selectDictTypeById(long dictId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        object checkDictTypeUnique(SysDictType dict);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        int insertDictType(SysDictType dict);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        int updateDictType(SysDictType dict);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictIds"></param>
        /// <returns></returns>
        int deleteDictTypeByIds(long[] dictIds);
        /// <summary>
        /// 
        /// </summary>
        void clearCache();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SysDictType> selectDictTypeAll();
    }
}
