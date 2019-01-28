using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core.Abstractions;

namespace SqlRepoEx.Core
{
  public abstract class GroupByClauseBaseBuilder : ClauseBuilder, IGroupByClauseBuilder, IClauseBuilder
  {
    protected readonly IList<GroupBySpecificationBase> groupBySpecifications = new List<GroupBySpecificationBase>();
    protected readonly IList<HavingSpecificationBase> havingSpecifications = new List<HavingSpecificationBase>();
    private const string ClauseTemplate = "GROUP BY {0}";
    private const string HavlingClauseTemplate = "\nHAVING {0}";

    public IGroupByClauseBuilder By<TEntity>(Expression<Func<TEntity, object>> selector, string alias = null, string tableName = null, string tableSchema = null, params Expression<Func<TEntity, object>>[] additionalSelectors)
    {
      AddGroupBySpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(selector));
      foreach (Expression<Func<TEntity, object>> additionalSelector in additionalSelectors)
        AddGroupBySpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(additionalSelector));
      IsClean = false;
      return this;
    }

    public IGroupByClauseBuilder HavingAvg<TEntity>(Expression<Func<TEntity, object>> selector, Comparison comparison, int value, string alias = null, string tableName = null, string tableSchema = null)
    {
      AddHavingSpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(selector), Aggregation.Avg, comparison, value);
      return this;
    }

    public IGroupByClauseBuilder HavingCount<TEntity>(Expression<Func<TEntity, object>> selector, Comparison comparison, int value, string alias = null, string tableName = null, string tableSchema = null)
    {
      AddHavingSpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(selector), Aggregation.Count, comparison, value);
      return this;
    }

    public IGroupByClauseBuilder HavingCountAll<TEntity>(Comparison comparison, int value)
    {
      AddHavingSpecification<TEntity>(null, null, null, "*", Aggregation.Count, comparison, value);
      return this;
    }

    public IGroupByClauseBuilder HavingMax<TEntity>(Expression<Func<TEntity, object>> selector, Comparison comparison, int value, string alias = null, string tableName = null, string tableSchema = null)
    {
      AddHavingSpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(selector), Aggregation.Max, comparison, value);
      return this;
    }

    public IGroupByClauseBuilder HavingMin<TEntity>(Expression<Func<TEntity, object>> selector, Comparison comparison, int value, string alias = null, string tableName = null, string tableSchema = null)
    {
      AddHavingSpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(selector), Aggregation.Min, comparison, value);
      return this;
    }

    public IGroupByClauseBuilder HavingSum<TEntity>(Expression<Func<TEntity, object>> selector, Comparison comparison, int value, string alias = null, string tableName = null, string tableSchema = null)
    {
      AddHavingSpecification<TEntity>(alias, tableName, tableSchema, GetMemberName(selector), Aggregation.Sum, comparison, value);
      return this;
    }

    public override string Sql()
    {
      if (!groupBySpecifications.Any())
        return string.Empty;
      string str = string.Format("GROUP BY {0}", string.Join(", ", groupBySpecifications));
      if (havingSpecifications.Any())
        str += string.Format("\nHAVING {0}", string.Join(", ", havingSpecifications));
      return str;
    }

    protected abstract void AddGroupBySpecification<TEntity>(string alias, string tableName, string tableSchema, string name);

    protected abstract void AddHavingSpecification<TEntity>(string alias, string tableName, string tableSchema, string name, Aggregation aggregation, Comparison comparison, object value);
  }
}
