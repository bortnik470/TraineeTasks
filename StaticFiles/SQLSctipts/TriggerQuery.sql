use University;
go;

create or alter trigger logTrigger on Students
for INSERT, Update, Delete
as
begin
	if exists (select 1 from inserted) and not exists(select 1 from deleted)
	begin
		insert into StudentLog 
		select *, 'Insert', GETDATE() from inserted;
	end
	else if not exists (select 1 from inserted) and exists(select 1 from deleted)
	begin
		insert into StudentLog 
		select *, 'Delete', GETDATE() from deleted;
	end
	else if exists (select 1 from inserted) and exists(select 1 from deleted)
	begin
		insert into StudentLog 
		select *, 'UpdateOld', GETDATE() from deleted;

		insert into StudentLog 
		select *, 'UpdateNew', GETDATE() from inserted;
	end
end

select * from Students;

select * from StudentLog;

update Students set 
firstName = 'Lary'
where studentId = 27

insert into Students
values ('Denis', 'Kin', '4561327', '3G');

delete from Students
where studentId = IDENT_CURRENT('Students');
go;