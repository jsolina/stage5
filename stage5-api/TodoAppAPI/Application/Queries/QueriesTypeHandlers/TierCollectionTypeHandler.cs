using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries.QueriesTypeHandlers
{
    public class TierCollectionTypeHandler
    {
    }
        /*
        public class TierCollectionTypeHandler : SqlMapper.TypeHandler<IList<TierViewModel>>
        {

            public override IList<TierViewModel> Parse(object value)
            {
                return JsonConvert.DeserializeObject<IList<TierViewModel>>(value.ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }

            public override void SetValue(IDbDataParameter parameter, IList<TierViewModel> value)
            {
                parameter.Value = JsonConvert.SerializeObject(value, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }
         */
    }
