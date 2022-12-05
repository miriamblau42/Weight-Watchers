
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'SubscriberPolicy`');
set @tableNameNonQuoted = concat(@tablePrefix, 'SubscriberPolicy');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;
