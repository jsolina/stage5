using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppAPI.LogHelper
{
    public class LogMessageTemplate
    {
        public string CreatedMessage()
        {
            return "Command Name: {CommandName}. {@NewValue}. Modified By: {UserName}. Action Date: {DateModified}. LogType: {LogType}.";
        }
        public string UpdatedMessage()
        {
            return "Command Name: {CommandName}. From: {@PreviousValue}. To: {@NewValue}. Modified By: {UserName}. Action Date: {DateModified}. LogType: {LogType}.";
        }
        public string DeletedMessage()
        {
            return "Command Name: {CommandName}. {@DeletedValue}. Modified By: {UserName}. Action Date: {DateModified}. LogType: {LogType}.";
        }
    }
}
