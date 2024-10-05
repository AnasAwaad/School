using School.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities;
public class DepartmetSubject : LocalizableEntity
{
    public int DID { get; set; }
    public int SubID { get; set; }

    [ForeignKey("DID")]
    public Department? Department { get; set; }

    [ForeignKey("SubID")]
    public Subject? Subject { get; set; }
}