using ANTIL.Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace ANTIL.Domain.Mapping
{
    public class CommitMap : ClassMap<Commit>
    {
        public CommitMap()
        {
            Id(m => m.Id);
            Map(m => m.Name);
            References(m => m.ParentCommit).Column("ParentCommitId");
            References(m => m.Project).Column("ProjectId");
            HasMany(m => m.Files).Inverse().Cascade.All();
        }
    }
}
