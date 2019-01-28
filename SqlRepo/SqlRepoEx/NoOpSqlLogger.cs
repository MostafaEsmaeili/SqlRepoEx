using SqlRepoEx.Abstractions;

namespace SqlRepoEx
{
  public class NoOpSqlLogger : ISqlLogWriter
  {
    public void Log(string sql)
    {
    }
  }
}
