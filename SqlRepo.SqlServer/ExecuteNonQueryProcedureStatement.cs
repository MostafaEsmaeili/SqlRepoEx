using System.Linq;
using System.Threading.Tasks;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core;
using SqlRepoEx.Core.Abstractions;

namespace SqlRepoEx.MsSqlServer
{
    public class ExecuteNonQueryProcedureStatement : ExecuteProcedureStatement<int>, IExecuteNonQueryProcedureStatement,
        IExecuteProcedureStatement<int>
    {
        protected string SchemaName { get; private set; } = "dbo";

        public ExecuteNonQueryProcedureStatement(IStatementExecutor statementExecutor)
            : base(statementExecutor)
        {
        }

        public override int Go()
        {
            if (string.IsNullOrWhiteSpace(ProcedureName))
                throw new MissingProcedureNameException();
            var name = "[" + SchemaName + "].[" + ProcedureName + "]";
            return ParameterDefinitions.Any()
                ? StatementExecutor.ExecuteNonQueryStoredProcedure(name,
                    ParameterDefinitions.ToArray())
                : StatementExecutor.ExecuteNonQueryStoredProcedure(name);
        }

        public override async Task<int> GoAsync()
        {
            if (string.IsNullOrWhiteSpace(ProcedureName))
                throw new MissingProcedureNameException();
            var procedureName = "[" + SchemaName + "].[" + ProcedureName + "]";
            int num;
            if (ParameterDefinitions.Any())
                num = await StatementExecutor.ExecuteNonQueryStoredProcedureAsync(procedureName,
                    ParameterDefinitions.ToArray());
            else
                num = await StatementExecutor.ExecuteNonQueryStoredProcedureAsync(procedureName);
            return num;
        }

        public IExecuteProcedureStatement<int> WithSchema(string schemaName)
        {
            SchemaName = schemaName;
            return this;
        }
    }
}
