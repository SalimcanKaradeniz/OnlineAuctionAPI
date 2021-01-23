using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Core.Extensions
{
    public static class AutoMapperHelper
    {
        public static TDest MapTo<TDest>(this object src)
        {
            return (TDest)AutoMapper.Mapper.Map(src, src.GetType(), typeof(TDest));
        }
    }
}
