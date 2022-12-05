
/* TableNameVariable */

declare @tableName nvarchar(max) = '[' + @schema + '].[' + @tablePrefix + N'SubscriberPolicy]';
declare @tableNameWithoutSchema nvarchar(max) = @tablePrefix + N'SubscriberPolicy';


/* Initialize */

/* CreateTable */

if not exists
(
    select *
    from sys.objects
    where
        object_id = object_id(@tableName) and
        type in ('U')
)
begin
declare @createTable nvarchar(max);
set @createTable = '
    create table ' + @tableName + '(
        Id uniqueidentifier not null primary key,
        Metadata nvarchar(max) not null,
        Data nvarchar(max) not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null
    )
';
exec(@createTable);
end

/* AddProperty MeasureID */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_MeasureID' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_MeasureID nvarchar(max);
  set @createColumn_MeasureID = '
  alter table ' + @tableName + N'
    add Correlation_MeasureID bigint;';
  exec(@createColumn_MeasureID);
end

/* VerifyColumnType Int */

declare @dataType_MeasureID nvarchar(max);
set @dataType_MeasureID = (
  select data_type
  from INFORMATION_SCHEMA.COLUMNS
  where
    table_name = @tableNameWithoutSchema and
    table_schema = @schema and
    column_name = 'Correlation_MeasureID'
);
if (@dataType_MeasureID <> 'bigint')
  begin
    declare @error_MeasureID nvarchar(max) = N'Incorrect data type for Correlation_MeasureID. Expected bigint got ' + @dataType_MeasureID + '.';
    throw 50000, @error_MeasureID, 0
  end

/* WriteCreateIndex MeasureID */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_MeasureID' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_MeasureID nvarchar(max);
  set @createIndex_MeasureID = N'
  create unique index Index_Correlation_MeasureID
  on ' + @tableName + N'(Correlation_MeasureID)
  where Correlation_MeasureID is not null;';
  exec(@createIndex_MeasureID);
end

/* PurgeObsoleteIndex */

declare @dropIndexQuery nvarchar(max);
select @dropIndexQuery =
(
    select 'drop index ' + name + ' on ' + @tableName + ';'
    from sysindexes
    where
        Id = object_id(@tableName) and
        Name is not null and
        Name like 'Index_Correlation_%' and
        Name <> N'Index_Correlation_MeasureID'
);
exec sp_executesql @dropIndexQuery

/* PurgeObsoleteProperties */

declare @dropPropertiesQuery nvarchar(max);
select @dropPropertiesQuery =
(
    select 'alter table ' + @tableName + ' drop column ' + column_name + ';'
    from INFORMATION_SCHEMA.COLUMNS
    where
        table_name = @tableNameWithoutSchema and
        table_schema = @schema and
        column_name like 'Correlation_%' and
        column_name <> N'Correlation_MeasureID'
);
exec sp_executesql @dropPropertiesQuery

/* CompleteSagaScript */
