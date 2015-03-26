create View [FileInfo] as
select f.Name as FileName, c.Name as CommitName, p.Name as ProjectNAme, u.UserName as UserName
 From Files as f 
left join [Commit] as c on f.CommitId = c.Id
left join [Projects] as p on p.Id = c.ProjectId
left join [Users] as u on u.Id = p.UserId