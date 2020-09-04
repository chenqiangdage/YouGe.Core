using System;
using System.Collections.Generic;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    public interface ISysDictTypeService
    {
        object selectDictDataByType(string dictType);
        List<SysDictType> selectDictTypeList(SysDictType dictType);
        object selectDictTypeById(long dictId);
        object checkDictTypeUnique(SysDictType dict);
        int insertDictType(SysDictType dict);
        int updateDictType(SysDictType dict);
        int deleteDictTypeByIds(long[] dictIds);
        void clearCache();
        List<SysDictType> selectDictTypeAll();
    }
}
