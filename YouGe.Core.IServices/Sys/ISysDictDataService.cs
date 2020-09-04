using System;
using System.Collections.Generic;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    public interface ISysDictDataService
    {
        List<SysDictData> selectDictDataList(SysDictData dictData);
        object selectDictDataById(long dictCode);
        int insertDictData(SysDictData dict);
        int updateDictData(SysDictData dict);
        int deleteDictDataByIds(long[] dictCodes);
    }
}
