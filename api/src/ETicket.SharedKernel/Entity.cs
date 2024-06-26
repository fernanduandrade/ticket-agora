using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.SharedKernel;

public abstract class Entity : HasDomainEvents
{
    public long Id { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("created_by")]
    public string? CreatedBy { get; set; }
    [Column("last_modified_at")]
    public DateTime LastModifiedAt { get; set; }
    [Column("last_modified_by")]
    public string? LastModifiedBy { get; set; }
}