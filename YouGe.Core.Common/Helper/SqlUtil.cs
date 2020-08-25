using System;
using System.Text.RegularExpressions;

namespace YouGe.Core.Common.Helper
{
    public class SqlUtil
    {
        /// <summary>
        /// 仅支持字母、数字、下划线、空格、逗号（支持多个字段排序）
        /// </summary>
        public static string SQL_PATTERN = "[a-zA-Z0-9_\\ \\,]+";

        /// <summary>
        /// 检查字符，防止注入绕过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string escapeOrderBySql(string value)
        {
            if (!string.IsNullOrEmpty(value) && !isValidOrderBySql(value))
            {
                return string.Empty;
            }
            return value;
        }

        /**
         * 验证 order by 语法是否符合规范
         */
        public static bool isValidOrderBySql(string value)
        {
            Regex r = new Regex(SQL_PATTERN, RegexOptions.IgnoreCase);
            Match m = r.Match(value);
            return m.Success;
        }

        public SqlUtil()
        {
        }
    }
}
