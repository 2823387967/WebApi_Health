using Common.MemCache.EnyimCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class MemCacheHelper : SingleTon<MemCacheHelper>
    {
        public ICacheWriterService writer;/* = CacheBuilder.GetWriterService();//writer 使用memcached默认过期时间*/
        public ICacheReaderService reader;/* = CacheBuilder.GetReaderService();//reader*/
        public MemCacheHelper()
        {    
            writer = CacheBuilder.GetWriterService();//writer 使用memcached默认过期时间
            reader = CacheBuilder.GetReaderService();//reader
            writer.TimeOut = 60;
        }
    }
}
