using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.Common.Helper;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons;
using YouGe.Core.Commons.Helper;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Interface.IRepositorys.Sys;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;

namespace YouGe.Core.Services.Sys
{
    public class SysDictDataService : ISysDictDataService
    {
        public SysDictDataService()
        {
        }

        public int deleteDictDataByIds(long[] dictCodes)
        {
            throw new NotImplementedException();
        }

        public int insertDictData(SysDictData dict)
        {
            throw new NotImplementedException();
        }

        public object selectDictDataById(long dictCode)
        {
            throw new NotImplementedException();
        }

        public List<SysDictData> selectDictDataList(SysDictData dictData)
        {
            throw new NotImplementedException();
        }

        public int updateDictData(SysDictData dict)
        {
            throw new NotImplementedException();
        }
    }
}
