using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.Application.Queries.QueriesTypeHandlers
{
    public class StringCollectionTypeHandler : SqlMapper.TypeHandler<IList<string>>
    {
        public override IList<string> Parse(object value)
        {
            return JsonConvert.DeserializeObject<IList<string>>(value.ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public override void SetValue(IDbDataParameter parameter, IList<string> value)
        {
            parameter.Value = JsonConvert.SerializeObject(value, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}